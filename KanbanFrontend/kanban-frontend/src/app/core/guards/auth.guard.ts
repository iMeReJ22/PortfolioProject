import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn, selectUsersState } from '../state/users/users.selector';
import { filter, map, take } from 'rxjs';

export const authGuard: CanActivateFn = () => {
    const store = inject(Store);
    const router = inject(Router);

    return store.select(selectUsersState).pipe(
        filter((state) => state.status !== 'loading' && state.status !== 'error'),
        take(1),
        map((state) => {
            if (state.loggedUser) return true;
            return router.createUrlTree(['/login']);
        }),
    );
};
