import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ActivityLogApiService } from '../../services/api/activity-log';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { LogsActions } from './activity-log.actions';
import { catchError, map, mergeMap, of } from 'rxjs';

@Injectable()
export class ActivityLogEffects {
    private actions$ = inject(Actions);
    private store = inject(Store<AppState>);
    private logsService = inject(ActivityLogApiService);

    getLogsForBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(LogsActions.getActivityForBoard),
            mergeMap(({ boardId }) =>
                this.logsService.getActivityForBoard(boardId).pipe(
                    map((logs) => LogsActions.getActivityForBoardSuccess({ logs })),
                    catchError((error) =>
                        of(LogsActions.getActivityForBoardFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });
}
