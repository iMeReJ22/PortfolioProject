import { TaskCommentDto } from '../../models/DTOs/task-comment.model';

export interface CommentState {
    comments: TaskCommentDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialCommentState = {
    comments: [],
    error: null,
    status: 'null',
};
