import { createReducer, on } from '@ngrx/store';
import { BoardDto } from '../../models/DTOs/board.model';
import { BoardMemberDto, compareRoles } from '../../models/DTOs/board-member.models';
import { BoardsActions } from './boards.actions';
import { map } from 'rxjs';
import { TaskTypeDto } from '../../models/DTOs/task-type.model';

export interface BoardState {
    boards: BoardDto[];
    boardMembers: BoardMemberDto[];
    currentBoard: BoardDto | null;
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialBoardsState: BoardState = {
    boards: [],
    boardMembers: [],
    currentBoard: null,
    error: null,
    status: 'idle',
};

export const boardReducer = createReducer(
    initialBoardsState,
    on(BoardsActions.createBoard, (state, { create, tempId }) => {
        const optimisticBoard: BoardDto = {
            id: tempId as any,
            name: create.name,
            description: create.description,
            createdAt: new Date(Date.now()),
            ownerId: create.ownerId,
        };
        const optimisticMember: BoardMemberDto = {
            userId: create.ownerId,
            boardId: tempId,
            role: 'owner',
        };
        return {
            ...state,
            status: 'creating',
            boards: [...state.boards, optimisticBoard],
            boardMembers: [...state.boardMembers, optimisticMember],
        };
    }),
    on(BoardsActions.createBoardSuccess, (state, { created, tempId }) => ({
        ...state,
        status: 'success',
        boards: state.boards.map((b) => (b.id === tempId ? created : b)),
        boardMembers: state.boardMembers.map((bm) => {
            const replacement: BoardMemberDto = {
                userId: created.ownerId,
                boardId: created.id,
                role: 'owner',
            };

            return bm.userId === created.ownerId && bm.boardId === tempId ? replacement : bm;
        }),
    })),
    on(BoardsActions.createBoardFailure, (state, { error, tempId }) => ({
        ...state,
        status: 'error',
        error,
        boards: state.boards.filter((b) => b.id !== tempId),
        boardMembers: state.boardMembers.filter((bm) => bm.boardId !== tempId),
    })),

    on(BoardsActions.getBoardById, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(BoardsActions.getBoardByIdSuccess, (state, { board }) => ({
        ...state,
        status: 'success',
        boards: state.boards.map((b) => (b.id === board.id ? board : b)),
    })),
    on(BoardsActions.getBoardByIdFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error: error,
    })),

    on(BoardsActions.getBoardsForUser, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(BoardsActions.getBoardsForUserSuccess, (state, { boards }) => ({
        ...state,
        status: 'success',
        boards: boards,
    })),
    on(BoardsActions.getBoardsForUserFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(BoardsActions.updateBoard, (state, { update }) => ({
        ...state,
        status: 'updating',
        boards: state.boards.map((b) => (b.id === update.boardId ? { ...b, ...update } : b)),
    })),
    on(BoardsActions.updateBoardSuccess, (state, { board }) => ({
        ...state,
        status: 'success',
        boards: state.boards.map((b) => (b.id === board.id ? board : b)),
    })),
    on(BoardsActions.updateBoardFailure, (state, { error, boardBefore }) => ({
        ...state,
        status: 'error',
        error,
        boards: state.boards.map((b) => (b.id === boardBefore.id ? boardBefore : b)),
    })),

    on(BoardsActions.deleteBoard, (state, { boardId }) => ({
        ...state,
        status: 'deleting',
        boards: state.boards.filter((b) => b.id !== boardId),
    })),
    on(BoardsActions.deleteBoardSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(BoardsActions.deleteBoardFailure, (state, { deletedBoard }) => ({
        ...state,
        status: 'error',
        boards: [...state.boards, deletedBoard],
    })),

    on(BoardsActions.addMemberToBoard, (state, { request }) => {
        const newBoardMember: BoardMemberDto = {
            userId: request.userId,
            boardId: request.boardId,
            role: request.role,
        };

        return {
            ...state,
            status: 'creating',
            boardMembers: [...state.boardMembers, newBoardMember].sort((a, b) =>
                compareRoles(a, b),
            ),
        };
    }),
    on(BoardsActions.addMemberToBoardSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(BoardsActions.addMemberToBoardFailure, (state, { boardId, userId }) => ({
        ...state,
        status: 'error',
        boardMembers: state.boardMembers.filter(
            (bm) => bm.userId !== userId && bm.boardId !== boardId,
        ),
    })),

    on(BoardsActions.removeMemberFromBoard, (state, { boardId, userId }) => ({
        ...state,
        status: 'deleting',
        boardMembers: state.boardMembers.filter(
            (bm) => bm.userId !== userId && bm.boardId !== boardId,
        ),
    })),
    on(BoardsActions.removeMemberFromBoardSuccess, (state) => ({
        ...state,
        status: 'success',
    })),
    on(BoardsActions.removeMemberFromBoardFailure, (state, { error, removedBoardMember }) => ({
        ...state,
        status: 'error',
        error,
        boardMembers: [...state.boardMembers, removedBoardMember].sort((a, b) =>
            compareRoles(a, b),
        ),
    })),

    on(BoardsActions.getDashboardBoardTiles, (state, {}) => ({
        ...state,
        status: 'loading',
    })),
    on(BoardsActions.getDashboardBoardTilesSuccess, (state, { tiles }) => ({
        ...state,
        status: 'success',
        error: null,
        boards: tiles.map((t) => {
            const board: BoardDto = { ...t, ownerId: t.owner.id };
            return board;
        }),
    })),
    on(BoardsActions.getDashboardBoardTilesFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(BoardsActions.upsertBoardMembers, (state, { members }) => {
        return {
            ...state,
            boardMembers: mergeBoardMembers(state.boardMembers, members),
        };
    }),

    on(BoardsActions.getDetailedBoardById, (state, { boardId }) => ({
        ...state,
        status: 'loading',
        boards: state.boards,
    })),
    on(BoardsActions.getDetailedBoardByIdSuccess, (state, { board }) => ({
        ...state,
        status: 'success',
        error: null,
        currentBoard: board,
        boards: mergeBoards(state.boards, [board]),
    })),
    on(BoardsActions.getDetailedBoardByIdFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
        boards: state.boards,
    })),
);

function mergeBoardMembers(membersLeft: BoardMemberDto[], membersRight: BoardMemberDto[]) {
    const map = new Map<string, BoardMemberDto>();
    [...membersLeft, ...membersRight].forEach((item) => {
        const key = `${item.boardId}_${item.userId}`;
        map.set(key, { ...map.get(key), ...item });
    });
    return Array.from(map.values());
}

interface boardMemberKey {
    boardId: number;
    userId: number;
}

function mergeBoards(left: BoardDto[], right: BoardDto[]) {
    const map = new Map<number, BoardDto>();
    [...left, ...right].forEach((item) => {
        const key = item.id;
        map.set(key, { ...map.get(key), ...item });
    });
    return Array.from(map.values());
}
