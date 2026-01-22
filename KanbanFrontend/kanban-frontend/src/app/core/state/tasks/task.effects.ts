import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TasksApiService } from '../../services/api/tasks';
import { Store } from '@ngrx/store';
import { TasksActions } from './tasks.actions';
import { catchError, concatMap, map, merge, mergeMap, of, switchMap } from 'rxjs';
import { selectAllTasks, selectTaskById, selectTasksByColumnId } from './task.selectors';
import { concatLatestFrom } from '@ngrx/operators';
import { AppState } from '../app.state';
import { TaskDto } from '../../models/DTOs/task.model';
import { TagDto } from '../../models/DTOs/tag.model';

@Injectable()
export class TasksEffects {
    constructor(
        private actions$: Actions,
        private taskService: TasksApiService,
        private store: Store<AppState>,
    ) {}

    getTasksForBoard$ = createEffect(() =>
        this.actions$.pipe(
            ofType(TasksActions.getTasksForBoard),
            switchMap(({ boardId }) =>
                this.taskService.getTasksForBoard(boardId).pipe(
                    map((tasks) => TasksActions.getTasksForBoardSuccess({ tasks })),
                    catchError((error) =>
                        of(TasksActions.getTasksForBoardFailure({ error: error.message })),
                    ),
                ),
            ),
        ),
    );

    updateTask$ = createEffect(() =>
        this.actions$.pipe(
            ofType(TasksActions.updateTask),
            concatLatestFrom(({ update }) => this.store.select(selectTaskById(update.taskId))),
            mergeMap(([{ update }, existingTask]) =>
                this.taskService.updateTask(update.taskId, update).pipe(
                    map((updatedTask) => TasksActions.updateTaskSuccess({ task: updatedTask })),
                    catchError((error) =>
                        of(
                            TasksActions.updateTaskFailure({
                                error: error.message,
                                task: existingTask ?? ({} as TaskDto),
                            }),
                        ),
                    ),
                ),
            ),
        ),
    );

    createTask$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.createTask),
            concatMap(({ create, tempId }) =>
                this.taskService.createTask(create).pipe(
                    map((task) => TasksActions.createTaskSuccess({ task, tempId })),
                    catchError((error) =>
                        of(
                            TasksActions.createTaskFailure({
                                error: error.message,
                                tempId: tempId,
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    deleteTask$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.deleteTask),
            concatLatestFrom(({ taskId }) => this.store.select(selectTaskById(taskId))),
            concatMap(([{ taskId }, deletedTask]) =>
                this.taskService.deleteTask(taskId).pipe(
                    map(() => TasksActions.deleteTaskSuccess()),
                    catchError((error) =>
                        of(
                            TasksActions.deleteTaskFailure({
                                error: error.message,
                                deletedTask: deletedTask ?? ({} as TaskDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    getTasksForColumn$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.getTasksForColumn),
            concatLatestFrom(() => this.store.select(selectAllTasks)),
            switchMap(([{ columnId }, unloadedTasks]) =>
                this.taskService.getTasksForColumn(columnId).pipe(
                    map((tasks) => TasksActions.getTasksForColumnSuccess({ tasks })),
                    catchError((error) =>
                        of(
                            TasksActions.getTasksForColumnFailure({
                                error: error.message,
                                unloadedTasks: unloadedTasks,
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    reorderTasks$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.reorderTasks),
            concatLatestFrom(({ reorder }) =>
                this.store.select(selectTasksByColumnId(reorder.columnId)),
            ),
            switchMap(([{ reorder }, unorderedTasks]) =>
                this.taskService.reorderTasks(reorder).pipe(
                    map(() => TasksActions.reorderTasksSuccess()),
                    catchError((error) =>
                        of(
                            TasksActions.reorderTasksFailure({
                                error: error.message,
                                unorderedTasks,
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    moveTask$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.moveTask),
            concatLatestFrom(({}) => this.store.select(selectAllTasks)),
            concatMap(([{ move }, unmovedTasks]) =>
                this.taskService.moveTask(move).pipe(
                    map(() => TasksActions.moveTaskSuccess()),
                    catchError((error) =>
                        of(TasksActions.moveTaskFailure({ error: error.message, unmovedTasks })),
                    ),
                ),
            ),
        );
    });

    assignTagToTask$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.assignTagToTask),
            concatLatestFrom(({ taskId }) => this.store.select(selectTaskById(taskId))),
            mergeMap(([{ taskId, assign }, untaggedTask]) =>
                this.taskService.assignTagToTask(taskId, assign).pipe(
                    map(() => TasksActions.assignTagToTaskSuccess()),
                    catchError((error) =>
                        of(
                            TasksActions.assignTagToTaskFailure({
                                error: error.message,
                                untaggedTask: untaggedTask ?? ({} as TaskDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    removeTagFromTask$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TasksActions.removeTagFromTask),
            concatLatestFrom(({ taskId }) => this.store.select(selectTaskById(taskId))),
            mergeMap(([{ taskId, tagId }, taggedTask]) =>
                this.taskService.removeTagFromTask(taskId, tagId).pipe(
                    map(() => TasksActions.removeTagFromTaskSuccess()),
                    catchError((error) =>
                        of(
                            TasksActions.removeTagFromTaskFailure({
                                error: error.message,
                                taggedTask: taggedTask ?? ({} as TaskDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });
}
