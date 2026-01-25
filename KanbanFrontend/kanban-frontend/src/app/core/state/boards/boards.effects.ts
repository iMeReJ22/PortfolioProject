import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { BoardsApiService } from '../../services/api/boards';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { catchError, concatMap, map, mergeMap, of, switchMap } from 'rxjs';
import { BoardsActions } from './boards.actions';
import { concatLatestFrom } from '@ngrx/operators';
import { selectBoardById, selectBoardMemberByIds } from './boards.selector';
import { BoardDto } from '../../models/DTOs/board.model';
import { BoardMemberDto } from '../../models/DTOs/board-member.models';
import { UsersActions } from '../users/users.actions';
import { selectLoggedUser } from '../users/users.selector';
import { ToastService } from '../../services/toast/toast.service';

@Injectable()
export class BoardsEffects {
    private actions$ = inject(Actions);
    private store = inject(Store<AppState>);
    private boardsService = inject(BoardsApiService);
    private toast = inject(ToastService);

    createBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.createBoard),
            concatMap(({ create, tempId }) =>
                this.boardsService.createBoard(create).pipe(
                    map((created) => {
                        this.toast.success(
                            'Board Created!',
                            `Successfully created ${create.name}.`,
                        );
                        return BoardsActions.createBoardSuccess({ created, tempId });
                    }),
                    catchError((error) => {
                        this.toast.error(
                            'Error creating.',
                            `Something went wrong while creating your board.`,
                        );
                        return of(
                            BoardsActions.createBoardFailure({ error: error.message, tempId }),
                        );
                    }),
                ),
            ),
        );
    });

    getBoardById$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.getBoardById),
            switchMap(({ boardId }) =>
                this.boardsService.getBoardById(boardId).pipe(
                    map((board) => BoardsActions.getBoardByIdSuccess({ board })),
                    catchError((error) =>
                        of(BoardsActions.getBoardByIdFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    getBoardsForUser$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.getBoardsForUser),
            switchMap(({ userId }) =>
                this.boardsService.getBoardsForUser(userId).pipe(
                    map((boards) => BoardsActions.getBoardsForUserSuccess({ boards })),
                    catchError((error) =>
                        of(BoardsActions.getBoardsForUserFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    updateBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.updateBoard),
            concatLatestFrom(({ update }) => this.store.select(selectBoardById(update.boardId))),
            mergeMap(([{ boardId, update }, boardBefore]) =>
                this.boardsService.updateBoard(boardId, update).pipe(
                    map((board) => BoardsActions.updateBoardSuccess({ board })),
                    catchError((error) =>
                        of(
                            BoardsActions.updateBoardFailure({
                                error: error.message,
                                boardBefore: boardBefore ?? ({} as BoardDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    deleteBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.deleteBoard),
            concatLatestFrom(({ boardId }) => this.store.select(selectBoardById(boardId))),
            concatMap(([{ boardId }, deletedBoard]) =>
                this.boardsService.deleteBoard(boardId).pipe(
                    map(() => {
                        this.toast.success('Board Deleted!', 'Successfully deleted the board.');
                        return BoardsActions.deleteBoardSuccess();
                    }),
                    catchError((error) => {
                        this.toast.error(
                            'Error Deleting!',
                            'Something went wrong while deleting your board.',
                        );
                        return of(
                            BoardsActions.deleteBoardFailure({
                                error: error.message,
                                deletedBoard: deletedBoard ?? ({} as BoardDto),
                            }),
                        );
                    }),
                ),
            ),
        );
    });

    addMemberToBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.addMemberToBoard),
            concatMap(({ boardId, request }) =>
                this.boardsService.addMemberToBoard(boardId, request).pipe(
                    map(() => BoardsActions.addMemberToBoardSuccess()),
                    catchError((error) =>
                        of(
                            BoardsActions.addMemberToBoardFailure({
                                error: error.message,
                                boardId: request.boardId,
                                userId: request.userId,
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    removeMemberFromBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.removeMemberFromBoard),
            concatLatestFrom(({ boardId, userId }) =>
                this.store.select(selectBoardMemberByIds(boardId, userId)),
            ),
            concatMap(([{ boardId, userId }, removedBoardMember]) =>
                this.boardsService.removeMemberFromBoard(boardId, userId).pipe(
                    map(() => BoardsActions.removeMemberFromBoardSuccess()),
                    catchError((error) =>
                        of(
                            BoardsActions.removeMemberFromBoardFailure({
                                error: error.message,
                                removedBoardMember: removedBoardMember ?? ({} as BoardMemberDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    getDashboardBoardTiles$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.getDashboardBoardTiles),
            concatLatestFrom(({}) => this.store.select(selectLoggedUser)),
            switchMap(([{}, user]) =>
                this.boardsService.getDashboardBoardTiles(user?.id ?? 0).pipe(
                    switchMap((tiles) => {
                        const usersToUpsert = tiles.map((t) => t.owner);
                        const membersToUpsert = tiles.flatMap((t) => t.boardMembers ?? []);

                        return [
                            UsersActions.upsertUsers({
                                users: usersToUpsert,
                            }),
                            BoardsActions.upsertBoardMembers({
                                members: membersToUpsert,
                            }),
                            BoardsActions.getDashboardBoardTilesSuccess({ tiles }),
                        ];
                    }),
                    catchError((error) =>
                        of(BoardsActions.getDashboardBoardTilesFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });
}
