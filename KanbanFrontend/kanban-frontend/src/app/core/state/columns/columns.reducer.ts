import { createReducer, on } from '@ngrx/store';
import { ColumnDto } from '../../models/DTOs/column.model';
import { BoardsActions } from '../boards/boards.actions';
import { ColumnsActions } from './columns.actions';

export interface ColumnState {
    columns: ColumnDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialColumnState: ColumnState = {
    columns: [],
    error: null,
    status: 'idle',
};

export const ColumnReducer = createReducer(
    initialColumnState,
    on(ColumnsActions.createColumn, (state, { request, tempId }) => {
        const optimisticColumn: ColumnDto = {
            id: tempId as any,
            name: request.name,
            orderIndex: 9999,
            boardId: request.boardId,
            tasks: [],
        };

        return {
            ...state,
            status: 'creating',
            columns: [...state.columns, optimisticColumn],
        };
    }),
    on(ColumnsActions.createColumnSuccess, (state, { created, tempId }) => ({
        ...state,
        status: 'success',
        error: null,
        column: state.columns.map((c) => (c.id === tempId ? created : c)),
    })),
    on(ColumnsActions.createColumnFailure, (state, { error, tempId }) => ({
        ...state,
        status: 'error',
        error,
        columns: state.columns.filter((c) => c.id !== tempId),
    })),

    on(ColumnsActions.getColumnsForBoard, (state, { boardId }) => ({
        ...state,
        status: 'loading',
    })),
    on(ColumnsActions.getColumnsForBoardSuccess, (state, { columns }) => ({
        ...state,
        status: 'success',
        columns: columns.sort((a, b) => a.orderIndex - b.orderIndex),
        error: null,
    })),
    on(ColumnsActions.getColumnsForBoardFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(ColumnsActions.updateColumn, (state, { columnId, request }) => ({
        ...state,
        columns: state.columns.map((c) => (c.id === request.columnId ? { ...c, ...request } : c)),
        status: 'updating',
    })),
    on(ColumnsActions.updateColumnSuccess, (state, { updatedColumn }) => ({
        ...state,
        status: 'success',
        error: null,
        columns: state.columns.map((c) => (c.id === updatedColumn.id ? updatedColumn : c)),
    })),
    on(ColumnsActions.updateColumnFailure, (state, { error, columnBefore }) => ({
        ...state,
        status: 'error',
        error,
        columns: state.columns.map((c) => (c.id === columnBefore.id ? columnBefore : c)),
    })),

    on(ColumnsActions.deleteColumn, (state, { columnId }) => ({
        ...state,
        status: 'deleting',
        columns: state.columns.filter((c) => c.id !== columnId),
    })),
    on(ColumnsActions.deleteColumnSuccess, (state) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(ColumnsActions.deleteColumnFailure, (state, { error, deletedColumn }) => ({
        ...state,
        status: 'error',
        error,
        columns: [...state.columns, deletedColumn].sort((a, b) => a.orderIndex - b.orderIndex),
    })),

    on(ColumnsActions.reorderColumns, (state, { boardId, request }) => ({
        ...state,
        status: 'updating',
        columns: state.columns.map((c) => {
            const match = request.columns.find((rq) => rq.columnId === c.id);
            if (match) c.orderIndex = match.orderIndex;
            return c;
        }),
    })),
    on(ColumnsActions.reorderColumnsSuccess, (state) => ({
        ...state,
        status: 'success',
        error: null,
    })),
    on(ColumnsActions.reorderColumnsFailure, (state, { error, columnsBefore }) => ({
        ...state,
        status: 'error',
        error,
        columns: columnsBefore,
    })),
);
