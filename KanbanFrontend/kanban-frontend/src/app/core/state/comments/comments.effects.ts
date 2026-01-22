import { Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';
import { CommentsApiService } from '../../services/api/comments';
import { AppState } from '../app.state';
import { Store } from '@ngrx/store';

@Injectable()
export class CommentsEffects {
    constructor(
        private actions$: Actions,
        private commentService: CommentsApiService,
        private store: Store<AppState>,
    ) {}
}
