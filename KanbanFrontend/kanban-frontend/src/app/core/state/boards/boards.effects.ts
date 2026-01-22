import { Injectable } from '@angular/core';
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

@Injectable()
export class BoardsEffects {
    constructor(
        private actions$: Actions,
        private boardsService: BoardsApiService,
        private store: Store<AppState>,
    ) {}

    createBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.createBoard),
            concatMap(({ create, tempId }) =>
                this.boardsService.createBoard(create).pipe(
                    map((created) => BoardsActions.createBoardSuccess({ created, tempId })),
                    catchError((error) =>
                        of(BoardsActions.createBoardFailure({ error: error.message, tempId })),
                    ),
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
                    map(() => BoardsActions.deleteBoardSuccess()),
                    catchError((error) =>
                        of(
                            BoardsActions.deleteBoardFailure({
                                error: error.message,
                                deletedBoard: deletedBoard ?? ({} as BoardDto),
                            }),
                        ),
                    ),
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

    getBoardMember$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(BoardsActions.getBoardMembers),
            switchMap(({ boardId }) =>
                this.boardsService.getBoardMembers(boardId).pipe(
                    map((users) => BoardsActions.getBoardMembersSuccess({ users })),
                    catchError((error) =>
                        of(BoardsActions.getBoardMembersFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });
}
