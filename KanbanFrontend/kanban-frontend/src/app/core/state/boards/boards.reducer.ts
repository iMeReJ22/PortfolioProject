import { createReducer, on } from '@ngrx/store';
import { BoardDto } from '../../models/DTOs/board.model';
import { BoardMemberDto, compareRoles } from '../../models/DTOs/board-member.models';
import { BoardsActions } from './boards.actions';

export interface BoardState {
    boards: BoardDto[];
    boardMembers: BoardMemberDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialBoardsState: BoardState = {
    boards: [],
    boardMembers: [],
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
        return {
            ...state,
            status: 'creating',
            boards: [...state.boards, optimisticBoard],
        };
    }),
    on(BoardsActions.createBoardSuccess, (state, { created, tempId }) => ({
        ...state,
        status: 'success',
        boards: state.boards.map((b) => (b.id === (tempId as any) ? created : b)),
    })),
    on(BoardsActions.createBoardFailure, (state, { error, tempId }) => ({
        ...state,
        status: 'error',
        error,
        boards: state.boards.filter((b) => b.id !== (tempId as any)),
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
        boards: [...state.boards, deletedBoard].sort(
            (a, b) => a.createdAt.getTime() - b.createdAt.getTime(),
        ),
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
);
