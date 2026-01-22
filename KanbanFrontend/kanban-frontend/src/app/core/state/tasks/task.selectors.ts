import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { TaskState } from './tasks.reducer';

export const selectTasksState = (state: AppState) => state.tasksState;

export const selectAllTasks = createSelector(selectTasksState, (state: TaskState) => state.tasks);

export const selectTaskById = (taskId: number) =>
    createSelector(selectAllTasks, (tasks) => tasks.find((task) => task.id === taskId));

export const selectTasksByColumnId = (columnId: number) =>
    createSelector(selectAllTasks, (tasks) =>
        tasks
            .filter((task) => task.columnId === columnId)
            .sort((a, b) => a.orderIndex - b.orderIndex),
    );
