import { Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';
import { TagsApiService } from '../../services/api/tags';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';

@Injectable()
export class TagsEffects {
    constructor(
        private actions$: Actions,
        private tagsService: TagsApiService,
        private store: Store<AppState>,
    ) {}
}
