import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { CommentsApiService } from '../../services/api/comments';
import { AppState } from '../app.state';
import { Store } from '@ngrx/store';
import { catchError, concatMap, map, of, switchMap } from 'rxjs';
import { CommentsActions } from './comments.actions';
import { concatLatestFrom } from '@ngrx/operators';
import { selectCommentById } from './comments.selector';
import { TaskCommentDto } from '../../models/DTOs/task-comment.model';

@Injectable()
export class CommentsEffects {
    constructor(
        private actions$: Actions,
        private commentService: CommentsApiService,
        private store: Store<AppState>,
    ) {}

    createComment$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(CommentsActions.createComment),
            concatMap(({ create, tempId }) =>
                this.commentService.createComment(create).pipe(
                    map((createdComment) =>
                        CommentsActions.createCommentSuccess({ createdComment, tempId }),
                    ),
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
                this.commentService.getCommentsForTask(taskId).pipe(
                    map((comments) => CommentsActions.getCommentsForTaskSuccess({ comments })),
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
                this.commentService.updateComment(commentId, update).pipe(
                    map((updatedComment) =>
                        CommentsActions.updateCommentSuccess({ updatedComment }),
                    ),
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
                this.commentService.deleteComment(commentId).pipe(
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
