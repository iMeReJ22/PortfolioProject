import { createReducer, on } from '@ngrx/store';
import { TasksActions } from './tasks.actions';
import { TaskDto } from '../../models/DTOs/task.model';

export interface TaskState {
    tasks: TaskDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialTaskState: TaskState = {
    tasks: [],
    error: null,
    status: 'idle',
};

export const taskReducer = createReducer(
    initialTaskState,
    on(TasksActions.getTasksForBoard, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(TasksActions.getTasksForBoardSuccess, (state, { tasks }) => ({
        ...state,
        tasks: tasks,
        status: 'success',
        error: null,
    })),
    on(TasksActions.getTasksForBoardFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error: error,
    })),

    on(TasksActions.updateTask, (state, { update }) => ({
        ...state,
        tasks: state.tasks.map((t) => (t.id === update.taskId ? { ...t, ...update } : t)),
        status: 'updating',
    })),
    on(TasksActions.updateTaskSuccess, (state, { task }) => ({
        ...state,
        status: 'success',
        tasks: state.tasks.map((t) => (t.id === task.id ? task : t)),
    })),
    on(TasksActions.updateTaskFailure, (state, { error, taskBefore }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: state.tasks.map((t) => (t.id === taskBefore.id ? taskBefore : t)),
    })),

    on(TasksActions.createTask, (state, { create, tempId }) => {
        const optimisticTask: TaskDto = {
            id: tempId as any,
            title: create.title,
            description: create.description || '',
            columnId: create.columnId,
            tags: [],
            comments: [],
            createdByUserId: create.createdByUserId,
            createdAt: new Date(Date.now()),
            orderIndex: 9999,
            taskTypeId: create.taskTypeId,
        };

        return {
            ...state,
            status: 'creating',
            tasks: [...state.tasks, optimisticTask],
        };
    }),
    on(TasksActions.createTaskSuccess, (state, { task, tempId }) => ({
        ...state,
        tasks: state.tasks.map((t) => (t.id === (tempId as any) ? task : t)),
        status: 'success',
    })),
    on(TasksActions.createTaskFailure, (state, { error, tempId }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: state.tasks.filter((t) => t.id !== (tempId as any)),
    })),

    on(TasksActions.deleteTask, (state, { taskId }) => ({
        ...state,
        status: 'deleting',
        tasks: state.tasks.filter((t) => t.id !== taskId),
    })),
    on(TasksActions.deleteTaskSuccess, (state) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(TasksActions.deleteTaskFailure, (state, { error, deletedTask }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: [...state.tasks, deletedTask].sort((a, b) => a.orderIndex - b.orderIndex),
    })),

    on(TasksActions.getTasksForColumn, (state, { columnId }) => ({
        ...state,
        status: 'loading',
        tasks: state.tasks.filter((t) => t.columnId === columnId),
    })),
    on(TasksActions.getTasksForColumnSuccess, (state, { tasks }) => ({
        ...state,
        tasks: tasks,
        status: 'success',
        error: null,
    })),
    on(TasksActions.getTasksForColumnFailure, (state, { error, unloadedTasks }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: unloadedTasks ? { ...state.tasks, unloadedTasks } : state.tasks,
    })),

    on(TasksActions.reorderTasks, (state, { reorder }) => ({
        ...state,
        status: 'loading',
        tasks: state.tasks.map((task) => {
            const matchingTask = reorder.tasks.find((t) => t.taskId === task.id);
            if (matchingTask) {
                task.orderIndex = matchingTask.orderIndex;
            }
            return task;
        }),
    })),
    on(TasksActions.reorderTasksSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(TasksActions.reorderTasksFailure, (state, { error, unorderedTasks }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: unorderedTasks,
    })),

    on(TasksActions.moveTask, (state, { move }) => {
        const task = state.tasks.find((t) => t.id === move.taskId);
        if (!task) {
            return state;
        }

        if (task.columnId != move.targetColumnId) {
            const oldColumnTasks = state.tasks
                .filter((t) => t.columnId == task.columnId && t.id !== move.taskId)
                .sort((a, b) => a.orderIndex - b.orderIndex);
            reorderTasks(oldColumnTasks);
        }

        const newColumnTasks = state.tasks
            .filter((t) => t.columnId == move.targetColumnId && t.id !== move.taskId)
            .sort((a, b) => a.orderIndex - b.orderIndex);

        if (task.columnId != move.targetColumnId) task.columnId = move.targetColumnId;

        newColumnTasks.splice(move.newOrderIndex - 1, 0, task);
        reorderTasks(newColumnTasks);

        return {
            ...state,
            status: 'updating',
            tasks: newColumnTasks,
        };
    }),
    on(TasksActions.moveTaskSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(TasksActions.moveTaskFailure, (state, { error, unmovedTasks }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: unmovedTasks,
    })),

    on(TasksActions.assignTagToTask, (state, { taskId, assign }) => {
        const task = state.tasks.find((t) => t.id === taskId);
        if (!task) {
            return state;
        }
        //TODO: when tags state is done, push tagDto instead of just increasing length
        // also add to tags state
        // task.tags.push();
        return {
            ...state,
            status: 'updating',
            tasks: state.tasks.map((t) => (t.id === taskId ? task : t)),
        };
    }),
    on(TasksActions.assignTagToTaskSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(TasksActions.assignTagToTaskFailure, (state, { error, untaggedTask }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: state.tasks.map((t) => (t.id === untaggedTask.id ? untaggedTask : t)),
    })),

    on(TasksActions.removeTagFromTask, (state, { taskId, tagId }) => {
        const task = state.tasks.find((t) => t.id === taskId);
        if (!task) {
            return state;
        }
        //TODO: when tags state is done, remove also from tags.
        task.tags = task.tags.filter((tag) => tag.id !== tagId);
        return {
            ...state,
            status: 'deleting',
            tasks: state.tasks.map((t) => (t.id === taskId ? task : t)),
        };
    }),
    on(TasksActions.removeTagFromTaskSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(TasksActions.removeTagFromTaskFailure, (state, { error, taggedTask }) => ({
        ...state,
        status: 'error',
        error: error,
        tasks: state.tasks.map((t) => (t.id === taggedTask.id ? taggedTask : t)),
    })),
);

function reorderTasks(tasks: TaskDto[]) {
    for (let i = 0; i < tasks.length; i++) {
        tasks[i].orderIndex = i + 1;
    }
}
