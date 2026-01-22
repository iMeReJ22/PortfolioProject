import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { ColumnState } from './columns.reducer';

export const selectColumnsState = (state: AppState) => state.columnsState;

export const selectAllColumns = createSelector(
    selectColumnsState,
    (state: ColumnState) => state.columns,
);

export const selectColumnById = (columnId: number) =>
    createSelector(selectAllColumns, (columns) => columns.find((c) => c.id === columnId));
