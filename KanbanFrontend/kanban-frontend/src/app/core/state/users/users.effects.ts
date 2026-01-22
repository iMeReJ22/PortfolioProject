import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { UsersApiService } from '../../services/api/users';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { catchError, concatMap, map, mergeMap, of, switchMap } from 'rxjs';
import { UsersActions } from './users.actions';
import { selectUserById } from './users.selector';
import { concatLatestFrom } from '@ngrx/operators';
import { UserDto } from '../../models/DTOs/user.model';

@Injectable()
export class UsersEffects {
    constructor(
        private actions$: Actions,
        private userService: UsersApiService,
        private store: Store<AppState>,
    ) {}

    createUser$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.createUser),
            concatMap(({ create }) =>
                this.userService.createUser(create).pipe(
                    map((createdUser) => UsersActions.createUserSuccess({ createdUser })),
                    catchError((error) =>
                        of(UsersActions.createUserFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    login$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.login),
            switchMap(({ login }) =>
                this.userService.login(login).pipe(
                    map((result) => UsersActions.loginSuccess({ result })),
                    catchError((error) => of(UsersActions.loginFailure({ error: error.message }))),
                ),
            ),
        );
    });

    getUserById$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.getUserById),
            switchMap(({ userId }) =>
                this.userService.getUserById(userId).pipe(
                    map((user) => UsersActions.getUserByIdSuccess({ user })),
                    catchError((error) =>
                        of(UsersActions.getUserByIdFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    getUsers$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.getUsers),
            switchMap(({}) =>
                this.userService.getUsers().pipe(
                    map((users) => UsersActions.getUsersSuccess({ users })),
                    catchError((error) =>
                        of(UsersActions.getUsersFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    updateUser$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.updateUser),
            concatLatestFrom(({ userId }) => this.store.select(selectUserById(userId))),
            concatMap(([{ userId, update }, oldUser]) =>
                this.userService.updateUser(userId, update).pipe(
                    map((updatedUser) => UsersActions.updateUserSuccess({ updatedUser })),
                    catchError((error) =>
                        of(
                            UsersActions.updateUserFailure({
                                error: error.message,
                                oldUser: oldUser ?? ({} as UserDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    deleteUser$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.deleteUser),
            concatLatestFrom(({ userId }) => this.store.select(selectUserById(userId))),
            mergeMap(([{ userId }, deletedUser]) =>
                this.userService.deleteUser(userId).pipe(
                    map(() => UsersActions.deleteUserSuccess()),
                    catchError((error) =>
                        of(
                            UsersActions.deleteUserFailure({
                                error: error.message,
                                deletedUser: deletedUser ?? ({} as UserDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    getUsersByBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(UsersActions.getUsersByBoard),
            switchMap(({ boardId }) =>
                this.userService.getUsersByBoard(boardId).pipe(
                    map((users) => UsersActions.getUsersByBoardSuccess({ users })),
                    catchError((error) =>
                        of(UsersActions.getUsersByBoardFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });
}
