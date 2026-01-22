import { Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';
import { UsersApiService } from '../../services/api/users';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';

@Injectable()
export class UsersEffects {
    constructor(
        private actions$: Actions,
        private userService: UsersApiService,
        private store: Store<AppState>,
    ) {}
}
