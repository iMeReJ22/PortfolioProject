import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { UserState } from './users.reducer';

export const selectUsersState = (state: AppState) => state.usersState;

export const selectAllUsers = createSelector(selectUsersState, (state: UserState) => state.users);

export const selectUserById = (userId: number) =>
    createSelector(selectAllUsers, (users) => users.find((u) => u.id === userId));
