import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CommentState } from './comments.reducer';

export const selectCommentsState = createFeatureSelector<CommentState>('comments');

export const selectAllComments = createSelector(
    selectCommentsState,
    (state: CommentState) => state.comments,
);

export const selectAllCommentsForTask = (taskId: number) =>
    createSelector(selectAllComments, (comments) =>
        comments
            .filter((c) => c.taskId === taskId)
            .sort((a, b) => a.createdAt.getTime() - b.createdAt.getTime()),
    );

export const selectCommentById = (commentId: number) =>
    createSelector(selectAllComments, (comments) => comments.find((c) => c.id === commentId));

export const selectCommentsError = createSelector(
    selectCommentsState,
    (state: CommentState) => state.error,
);
export const selectCommentsStatus = createSelector(
    selectCommentsState,
    (state: CommentState) => state.status,
);
