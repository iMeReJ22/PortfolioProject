import { createReducer } from '@ngrx/store';
import { BoardDto } from '../../models/DTOs/board.model';
import { BoardMemberDto } from '../../models/DTOs/board-member.models';

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

export const boardReducer = createReducer(initialBoardsState);
