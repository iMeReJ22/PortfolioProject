import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { UserState } from './users.reducer';
import { LoginResultDto } from '../../models/DTOs/login-result.models';

export const selectUsersState = (state: AppState) => state.usersState;

export const selectAllUsers = createSelector(selectUsersState, (state: UserState) => state.users);

export const selectUserById = (userId: number) =>
    createSelector(selectAllUsers, (users) => users.find((u) => u.id === userId));

export const selectLoggedData = createSelector(
    selectUsersState,
    (state: UserState) => state.loggedUser,
);

export const selectIsLoggedIn = createSelector(selectUsersState, (state: UserState) =>
    state.loggedUser ? true : false,
);
