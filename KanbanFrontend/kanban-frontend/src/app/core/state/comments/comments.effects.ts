import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { CommentsApiService } from '../../services/api/comments';
import { Store } from '@ngrx/store';
import { catchError, concatMap, map, of, switchMap } from 'rxjs';
import { CommentsActions } from './comments.actions';
import { concatLatestFrom } from '@ngrx/operators';
import { selectCommentById } from './comments.selector';
import { TaskCommentDto } from '../../models/DTOs/task-comment.model';

export class CommentsEffects {
    private actions$ = inject(Actions);
    private store = inject(Store);
    private commentsService = inject(CommentsApiService);

    createComment$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(CommentsActions.createComment),
            concatMap(({ create, tempId }) =>
                this.commentsService.createComment(create).pipe(
                    map((createdComment) => {
                        createdComment = {
                            ...createdComment,
                            createdAt: new Date(createdComment.createdAt + 'Z'),
                        };
                        return CommentsActions.createCommentSuccess({ createdComment, tempId });
                    }),
                    catchError((error) =>
                        of(CommentsActions.createCommentFailure({ error: error.message, tempId })),
                    ),
                ),
            ),
        );
    });

    getCommentsForTask$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(CommentsActions.getCommentsForTask),
            switchMap(({ taskId }) =>
                this.commentsService.getCommentsForTask(taskId).pipe(
                    map((comments) => {
                        comments = comments.map((c) => ({
                            ...c,
                            createdAt: new Date(c.createdAt + 'Z'),
                        }));
                        return CommentsActions.getCommentsForTaskSuccess({ comments });
                    }),
                    catchError((error) =>
                        of(CommentsActions.getCommentsForTaskFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    updateComment$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(CommentsActions.updateComment),
            concatLatestFrom(({ commentId }) => this.store.select(selectCommentById(commentId))),
            concatMap(([{ commentId, update }, commentBefore]) =>
                this.commentsService.updateComment(commentId, update).pipe(
                    map((updatedComment) => {
                        updatedComment.createdAt = new Date(updatedComment.createdAt + 'Z');
                        return CommentsActions.updateCommentSuccess({ updatedComment });
                    }),
                    catchError((error) =>
                        of(
                            CommentsActions.updateCommentFailure({
                                error: error.message,
                                commentBefore: commentBefore ?? ({} as TaskCommentDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    deleteComment$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(CommentsActions.deleteComment),
            concatLatestFrom(({ commentId }) => this.store.select(selectCommentById(commentId))),
            concatMap(([{ commentId }, deletedComment]) =>
                this.commentsService.deleteComment(commentId).pipe(
                    map(() => CommentsActions.deleteCommentSuccess()),
                    catchError((error) =>
                        of(
                            CommentsActions.deleteCommentFailure({
                                error: error.message,
                                deletedComment: deletedComment ?? ({} as TaskCommentDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });
}
