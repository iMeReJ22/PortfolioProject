import { createReducer } from '@ngrx/store';
import { UserDto } from '../../models/DTOs/user.model';

export interface UserState {
    users: UserDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialUserState: UserState = {
    users: [],
    error: null,
    status: 'idle',
};

export const userReducer = createReducer(initialUserState);
