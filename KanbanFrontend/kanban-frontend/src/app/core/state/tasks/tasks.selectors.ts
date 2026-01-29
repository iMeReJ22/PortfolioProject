import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TaskState } from './tasks.reducer';
import { FullyDetailedTaskDto, TaskForColumnDto } from '../../models/DTOs/task.model';
import { selectAllTags } from '../tags/tags.selector';
import { selectAllComments } from '../comments/comments.selector';

export const selectTasksState = createFeatureSelector<TaskState>('tasks');

export const selectAllTasks = createSelector(selectTasksState, (state: TaskState) => state.tasks);

export const selectTaskTypesDict = createSelector(
    selectTasksState,
    (state: TaskState) => state.typesDict,
);

export const selectTaskById = (taskId: number) =>
    createSelector(selectAllTasks, (tasks) => tasks.find((task) => task.id === taskId));

export const selectTasksByColumnId = (columnId: number) =>
    createSelector(selectAllTasks, (tasks) =>
        tasks
            .filter((task) => task.columnId === columnId)
            .sort((a, b) => a.orderIndex - b.orderIndex),
    );

export const selectTaskIdsByColumnId = (columnId: number) =>
    createSelector(selectAllTasks, (tasks) =>
        tasks.filter((t) => t.columnId === columnId).map((t) => t.id),
    );

export const selectTasksError = createSelector(selectTasksState, (state: TaskState) => state.error);

export const selectTasksStatus = createSelector(
    selectTasksState,
    (state: TaskState) => state.status,
);

export const selectTasksForColumn = (columnId: number) =>
    createSelector(selectTasksState, selectAllTags, (tasksState, tags) => {
        const tasksForColumn = tasksState.tasks
            .filter((t) => t.columnId === columnId)
            .map((t) => {
                const taskWithTag: TaskForColumnDto = {
                    ...t,
                    tags: t.tagIds ? t.tagIds.map((id) => tags.find((tag) => tag.id === id)!) : [],
                    type: tasksState.typesDict[t.taskTypeId]!,
                };
                return taskWithTag;
            });
        return tasksForColumn.sort((a, b) => a.orderIndex - b.orderIndex);
    });

export const selectDetailedTask = (taskId: number) =>
    createSelector(
        selectTasksState,
        selectAllTags,
        selectAllComments,
        (tasksState, tags, comments) => {
            const task = tasksState.tasks.find((t) => t.id === taskId);
            const fullyDetailedTask: FullyDetailedTaskDto = {
                ...task!,
                type: tasksState.typesDict[task?.taskTypeId!],
                tags: task?.tagIds
                    ? task.tagIds.map((id) => tags.find((tag) => tag.id === id)!)
                    : [],
                comments: task?.commentIds
                    ? task.commentIds.map((id) => comments.find((comment) => comment.id === id)!)
                    : [],
            };
            return fullyDetailedTask;
        },
    );
