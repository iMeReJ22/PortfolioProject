import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { BoardState } from './boards.reducer';

export const selectBoardState = (state: AppState) => state.boardsState;

export const selectAllBoardMembers = createSelector(
    selectBoardState,
    (state: BoardState) => state.boardMembers,
);

export const selectBoardMemberByIds = (boardId: number, userId: number) =>
    createSelector(selectAllBoardMembers, (bms) =>
        bms.find((bm) => bm.boardId === boardId && bm.userId === userId),
    );

export const selectAllBoards = createSelector(
    selectBoardState,
    (state: BoardState) => state.boards,
);

export const selectBoardById = (boardId: number) =>
    createSelector(selectAllBoards, (boards) => boards.find((board) => board.id === boardId));
