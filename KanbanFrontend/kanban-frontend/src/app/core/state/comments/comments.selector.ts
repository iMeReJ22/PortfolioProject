import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { CommentState } from './comments.reducer';

export const selectCommentsState = (state: AppState) => state.commentsState;

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
