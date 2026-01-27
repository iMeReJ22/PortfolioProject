import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { catchError, throwError } from 'rxjs';
import { UsersActions } from '../state/users/users.actions';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const store = inject(Store);
    return next(req).pipe(
        catchError((error) => {
            if (error.status === 404) {
                console.error('Resource not found:', error);
            } else if (error.status === 500) {
                console.error('Internal server error:', error);
            } else if (error.status === 401) {
                store.dispatch(UsersActions.logout());
                console.error('Your session ended, please login.');
            } else if (error.status === 0) {
                store.dispatch(UsersActions.logout());
                console.error('Your session ended, please login.');
            }

            return throwError(() => error);
        }),
    );
};
