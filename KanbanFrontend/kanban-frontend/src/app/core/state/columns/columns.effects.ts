import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { ColumnsApiService } from '../../services/api/columns';
import { catchError, concatMap, map, of, switchMap } from 'rxjs';
import { ColumnsActions } from './columns.actions';
import { concatLatestFrom } from '@ngrx/operators';
import { selectAllColumns, selectColumnById } from './columns.selector';
import { ColumnDto } from '../../models/DTOs/column.model';

export class ColumnsEffects {
    private actions$ = inject(Actions);
    private store = inject(Store<AppState>);
    private columnsService = inject(ColumnsApiService);

    createColumn$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(ColumnsActions.createColumn),
            concatMap(({ tempId, request }) =>
                this.columnsService.createColumn(request).pipe(
                    map((created) => ColumnsActions.createColumnSuccess({ created, tempId })),
                    catchError((error) =>
                        of(ColumnsActions.createColumnFailure({ error: error.message, tempId })),
                    ),
                ),
            ),
        );
    });

    getColumnsForBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(ColumnsActions.getColumnsForBoard),
            switchMap(({ boardId }) =>
                this.columnsService.getColumnsForBoard(boardId).pipe(
                    map((columns) => ColumnsActions.getColumnsForBoardSuccess({ columns })),
                    catchError((error) =>
                        of(ColumnsActions.getColumnsForBoardFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    updateColumn$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(ColumnsActions.updateColumn),
            concatLatestFrom(({ columnId }) => this.store.select(selectColumnById(columnId))),
            concatMap(([{ columnId, request }, columnBefore]) =>
                this.columnsService.updateColumn(columnId, request).pipe(
                    map((updatedColumn) => ColumnsActions.updateColumnSuccess({ updatedColumn })),
                    catchError((error) =>
                        of(
                            ColumnsActions.updateColumnFailure({
                                error: error.message,
                                columnBefore: columnBefore ?? ({} as ColumnDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    deleteColumn$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(ColumnsActions.deleteColumn),
            concatLatestFrom(({ columnId }) => this.store.select(selectColumnById(columnId))),
            concatMap(([{ columnId }, deletedColumn]) =>
                this.columnsService.deleteColumn(columnId).pipe(
                    map(() => ColumnsActions.deleteColumnSuccess()),
                    catchError((error) =>
                        of(
                            ColumnsActions.deleteColumnFailure({
                                error: error.message,
                                deletedColumn: deletedColumn ?? ({} as ColumnDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    reorderColumns$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(ColumnsActions.reorderColumns),
            concatLatestFrom(() => this.store.select(selectAllColumns)),
            switchMap(([{ boardId, request }, columnsBefore]) =>
                this.columnsService.reorderColumns(boardId, request).pipe(
                    map(() => ColumnsActions.reorderColumnsSuccess()),
                    catchError((error) =>
                        of(
                            ColumnsActions.reorderColumnsFailure({
                                error: error.message,
                                columnsBefore,
                            }),
                        ),
                    ),
                ),
            ),
        );
    });
}
