import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TaskState } from './tasks.reducer';
import { TaskForColumnDto } from '../../models/DTOs/task.model';
import { selectAllTags } from '../tags/tags.selector';

export const selectTasksState = createFeatureSelector<TaskState>('tasks');

export const selectAllTasks = createSelector(selectTasksState, (state: TaskState) => state.tasks);

export const selectTaskById = (taskId: number) =>
    createSelector(selectAllTasks, (tasks) => tasks.find((task) => task.id === taskId));

export const selectTasksByColumnId = (columnId: number) =>
    createSelector(selectAllTasks, (tasks) =>
        tasks
            .filter((task) => task.columnId === columnId)
            .sort((a, b) => a.orderIndex - b.orderIndex),
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
                    tags: t.tagIds.map((id) => tags.find((tag) => tag.id === id)!),
                    type: tasksState.typesMap.get(t.taskTypeId)!,
                };
                return taskWithTag;
            });
        return tasksForColumn;
    });
