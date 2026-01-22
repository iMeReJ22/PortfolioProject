import { Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { ColumnsApiService } from '../../services/api/columns';

@Injectable()
export class ColumnsEffects {
    constructor(
        private actions$: Actions,
        private boardsService: ColumnsApiService,
        private store: Store<AppState>,
    ) {}
}
