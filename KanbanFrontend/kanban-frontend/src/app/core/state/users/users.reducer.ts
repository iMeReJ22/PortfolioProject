import { createReducer, on } from '@ngrx/store';
import { UserDto } from '../../models/DTOs/user.model';
import { UsersActions } from './users.actions';
import { LoginResultDto } from '../../models/DTOs/login-result.models';

export interface UserState {
    users: UserDto[];
    loggedUser: LoginResultDto | null;
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

const token = localStorage.getItem('token');
const userJson = localStorage.getItem('user');

export const initialUserState: UserState = {
    users: [],
    loggedUser: token && userJson ? { token: token, user: JSON.parse(userJson) } : null,
    error: null,
    status: 'idle',
};

export const userReducer = createReducer(
    initialUserState,
    on(UsersActions.createUser, (state) => ({
        ...state,
        status: 'creating',
    })),
    on(UsersActions.createUserSuccess, (state) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(UsersActions.createUserFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(UsersActions.login, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(UsersActions.loginSuccess, (state, { result }) => {
        return {
            ...state,
            status: 'success',
            error: null,
            loggedUser: result,
        };
    }),
    on(UsersActions.loginFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(UsersActions.getUserById, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(UsersActions.getUserByIdSuccess, (state) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(UsersActions.getUserByIdFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(UsersActions.updateUser, (state, { userId, update }) => ({
        ...state,
        status: 'updating',
        users: state.users.map((u) => (u.id === userId ? { ...u, ...update } : u)),
    })),
    on(UsersActions.updateUserSuccess, (state, { updatedUser }) => ({
        ...state,
        status: 'success',
        error: null,
        users: state.users.map((u) => (u.id === updatedUser.id ? updatedUser : u)),
    })),
    on(UsersActions.updateUserFailure, (state, { error, oldUser }) => ({
        ...state,
        status: 'error',
        error,
        users: state.users.map((u) => (u.id === oldUser.id ? oldUser : u)),
    })),

    on(UsersActions.deleteUser, (state, { userId }) => ({
        ...state,
        status: 'deleting',
        users: state.users.filter((u) => u.id !== userId),
    })),
    on(UsersActions.deleteUserSuccess, (state, {}) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(UsersActions.deleteUserFailure, (state, { error, deletedUser }) => ({
        ...state,
        status: 'error',
        error,
        users: [...state.users, deletedUser],
    })),

    on(UsersActions.getUsersByBoard, (state, {}) => ({
        ...state,
        status: 'loading',
    })),
    on(UsersActions.getUsersByBoardSuccess, (state, { users }) => ({
        ...state,
        status: 'success',
        error: null,
        users: users,
    })),
    on(UsersActions.getUsersByBoardFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(UsersActions.logout, (state) => {
        return {
            ...initialUserState,
        };
    }),
);
