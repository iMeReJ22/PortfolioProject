import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { TaskDto } from '../../models/DTOs/task.model';
import {
    AssignTagToTaskRequest,
    CreateTaskRequest,
    UpdateTaskRequest,
    ReorderTasksRequest,
    MoveTaskRequest,
} from '../../models/Requests/task-requests.models';
import { UserDto } from '../../models/DTOs/user.model';

export const TasksActions = createActionGroup({
    source: 'Tasks API',
    events: {
        'Get Tasks For Board': props<{ boardId: number }>(),
        'Get Tasks For Board Success': props<{ tasks: TaskDto[] }>(),
        'Get Tasks For Board Failure': props<{ error: string }>(),

        'Update Task': props<{ update: UpdateTaskRequest }>(),
        'Update Task Success': props<{ task: TaskDto }>(),
        'Update Task Failure': props<{ error: string; taskBefore: TaskDto }>(),

        'Create Task': props<{ create: CreateTaskRequest; tempId: number }>(),
        'Create Task Success': props<{ task: TaskDto; tempId: number }>(),
        'Create Task Failure': props<{ error: string; tempId: number }>(),

        'Delete Task': props<{ taskId: number }>(),
        'Delete Task Success': emptyProps(),
        'Delete Task Failure': props<{ error: string; deletedTask: TaskDto }>(),

        'Get Tasks For Column': props<{ columnId: number }>(),
        'Get Tasks For Column Success': props<{ tasks: TaskDto[] }>(),
        'Get Tasks For Column Failure': props<{ error: string; unloadedTasks?: TaskDto[] }>(),

        'Reorder Tasks': props<{ reorder: ReorderTasksRequest }>(),
        'Reorder Tasks Success': emptyProps(),
        'Reorder Tasks Failure': props<{ error: string; unorderedTasks: TaskDto[] }>(),

        'Move Task': props<{ move: MoveTaskRequest }>(),
        'Move Task Success': emptyProps(),
        'Move Task Failure': props<{ error: string; unmovedTasks: TaskDto[] }>(),

        'Assign Tag To Task': props<{ taskId: number; assign: AssignTagToTaskRequest }>(),
        'Assign Tag To Task Success': emptyProps(),
        'Assign Tag To Task Failure': props<{ error: string; untaggedTask: TaskDto }>(),

        'Remove Tag From Task': props<{ taskId: number; tagId: number }>(),
        'Remove Tag From Task Success': emptyProps(),
        'Remove Tag From Task Failure': props<{ error: string; taggedTask: TaskDto }>(),
    },
});
