import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ColumnState } from './columns.reducer';

export const selectColumnsState = createFeatureSelector<ColumnState>('columns');

export const selectAllColumns = createSelector(
    selectColumnsState,
    (state: ColumnState) => state.columns,
);

export const selectColumnById = (columnId: number) =>
    createSelector(selectAllColumns, (columns) => columns.find((c) => c.id === columnId));

export const selectColumnsError = createSelector(
    selectColumnsState,
    (state: ColumnState) => state.error,
);
export const selectColumnsStatus = createSelector(
    selectColumnsState,
    (state: ColumnState) => state.status,
);
export const selectColumnsByBoardId = (boardId: number) =>
    createSelector(selectColumnsState, (state: ColumnState) =>
        state.columns.filter((c) => c.boardId === boardId),
    );
