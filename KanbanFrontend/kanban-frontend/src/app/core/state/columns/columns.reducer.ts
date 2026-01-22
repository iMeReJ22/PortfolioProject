import { createReducer } from '@ngrx/store';
import { ColumnDto } from '../../models/DTOs/column.model';

export interface ColumnState {
    Columns: ColumnDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialColumnState: ColumnState = {
    Columns: [],
    error: null,
    status: 'idle',
};

export const ColumnReducer = createReducer(initialColumnState);
