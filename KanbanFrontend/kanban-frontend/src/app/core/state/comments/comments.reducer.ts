import { createReducer, on } from '@ngrx/store';
import { TaskCommentDto } from '../../models/DTOs/task-comment.model';
import { CommentsActions } from './comments.actions';

export interface CommentState {
    comments: TaskCommentDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialCommentState: CommentState = {
    comments: [],
    error: null,
    status: 'idle',
};

export const commentReducer = createReducer(
    initialCommentState,
    on(CommentsActions.createComment, (state, { create, tempId }) => {
        const optimisticComment: TaskCommentDto = {
            id: tempId as any,
            content: create.content,
            createdAt: new Date(Date.now()),
            taskId: create.taskId,
            authorId: create.authorId,
        };

        return {
            ...state,
            status: 'creating',
            comments: [...state.comments, optimisticComment],
        };
    }),
    on(CommentsActions.createCommentSuccess, (state, { createdComment, tempId }) => ({
        ...state,
        status: 'success',
        error: null,
        comments: state.comments.map((c) => (c.id === tempId ? createdComment : c)),
    })),
    on(CommentsActions.createCommentFailure, (state, { error, tempId }) => ({
        ...state,
        status: 'error',
        error: error,
        comments: state.comments.filter((c) => c.id !== tempId),
    })),

    on(CommentsActions.getCommentsForTask, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(CommentsActions.getCommentsForTaskSuccess, (state, { comments }) => ({
        ...state,
        status: 'success',
        error: null,
        comments: comments,
    })),
    on(CommentsActions.getCommentsForTaskFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(CommentsActions.updateComment, (state, { commentId, update }) => ({
        ...state,
        status: 'updating',
        comments: state.comments.map((c) => (c.id === commentId ? { ...c, ...update } : c)),
    })),
    on(CommentsActions.updateCommentSuccess, (state, { updatedComment }) => ({
        ...state,
        status: 'success',
        error: null,
        comments: state.comments.map((c) => (c.id === updatedComment.id ? updatedComment : c)),
    })),
    on(CommentsActions.updateCommentFailure, (state, { error, commentBefore }) => ({
        ...state,
        status: 'error',
        error,
        comments: state.comments.map((c) => (c.id === commentBefore.id ? commentBefore : c)),
    })),

    on(CommentsActions.deleteComment, (state, { commentId }) => ({
        ...state,
        status: 'deleting',
        comments: state.comments.filter((c) => c.id !== commentId),
    })),
    on(CommentsActions.deleteCommentSuccess, (state, {}) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(CommentsActions.deleteCommentFailure, (state, { error, deletedComment }) => ({
        ...state,
        status: 'error',
        error,
        comments: [...state.comments, deletedComment].sort(
            (a, b) => a.createdAt.getTime() - b.createdAt.getTime(),
        ),
    })),

    on(CommentsActions.upsertComments, (state, { comments }) => {
        return {
            ...state,
            comments: mergeComments(state.comments, comments),
        };
    }),
);
function mergeComments(left: TaskCommentDto[], right: TaskCommentDto[]) {
    const map = new Map<number, TaskCommentDto>();
    [...left, ...right].forEach((item) => {
        const key = item.id;
        map.set(key, { ...map.get(key), ...item });
    });
    return Array.from(map.values());
}
