import { createReducer, on } from '@ngrx/store';
import { ActivityLogDto } from '../../models/DTOs/activity-log.model';
import { LogsActions } from './activity-log.actions';

export interface LogState {
    logs: ActivityLogDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error';
}

export const initialLogsState: LogState = {
    logs: [],
    error: null,
    status: 'idle',
};

export const logReducer = createReducer(
    initialLogsState,
    on(LogsActions.getActivityForBoard, (state) => ({
        ...state,
        status: 'loading',
    })),
    on(LogsActions.getActivityForBoardSuccess, (state, { logs }) => ({
        ...state,
        logs: logs,
        status: 'success',
        error: null,
    })),
    on(LogsActions.getActivityForBoardFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error: error,
    })),
);
