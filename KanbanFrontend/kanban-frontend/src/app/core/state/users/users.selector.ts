import { createFeatureSelector, createSelector } from '@ngrx/store';
import { UserState } from './users.reducer';

export const selectUsersState = createFeatureSelector<UserState>('users');

export const selectAllUsers = createSelector(selectUsersState, (state: UserState) => state.users);

export const selectUserById = (userId: number) =>
    createSelector(selectAllUsers, (users) => users.find((u) => u.id === userId));

export const selectLoggedData = createSelector(
    selectUsersState,
    (state: UserState) => state.loggedUser,
);

export const selectIsLoggedIn = createSelector(selectUsersState, (state: UserState) =>
    state?.loggedUser ? true : false,
);

export const selectUsersError = createSelector(
    selectUsersState,
    (state: UserState) => state?.error,
);
export const selectUsersStatus = createSelector(
    selectUsersState,
    (state: UserState) => state?.status,
);
