import { createActionGroup, emptyProps, props } from '@ngrx/store';
import {
    CreateCommentRequest,
    UpdateCommentRequest,
} from '../../models/Requests/comment-requests.models';
import { TaskCommentDto } from '../../models/DTOs/task-comment.model';

export const CommentsActions = createActionGroup({
    source: 'Comments API',
    events: {
        'Create Comment': props<{ create: CreateCommentRequest; tempId: number }>(),
        'Create Comment Success': props<{ createdComment: TaskCommentDto; tempId: number }>(),
        'Create Comment Failure': props<{ error: string; tempId: number }>(),

        'Get Comments For Task': props<{ taskId: number }>(),
        'Get Comments For Task Success': props<{ comments: TaskCommentDto[] }>(),
        'Get Comments For Task Failure': props<{ error: string }>(),

        'Update Comment': props<{ commentId: number; update: UpdateCommentRequest }>(),
        'Update Comment Success': props<{ updatedComment: TaskCommentDto }>(),
        'Update Comment Failure': props<{ error: string; commentBefore: TaskCommentDto }>(),

        'Delete Comment': props<{ commentId: number }>(),
        'Delete Comment Success': emptyProps(),
        'Delete Comment Failure': props<{ error: string; deletedComment: TaskCommentDto }>(),

        'Set Comments': props<{ comments: TaskCommentDto[] }>(),
        'Upsert Comments': props<{ comments: TaskCommentDto[] }>(),
    },
});
