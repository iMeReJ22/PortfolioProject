import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BoardState } from './boards.reducer';

export const selectBoardState = createFeatureSelector<BoardState>('boards');

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

export const selectBoardsError = createSelector(
    selectBoardState,
    (state: BoardState) => state.error,
);
export const selectBoardsStatus = createSelector(
    selectBoardState,
    (state: BoardState) => state.status,
);
