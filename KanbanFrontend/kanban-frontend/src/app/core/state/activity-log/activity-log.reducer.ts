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
    on(LogsActions.upsetActivity, (state, { logs }) => {
        return {
            ...state,
            logs: [...new Set([...state.logs, ...logs])],
        };
    }),
);

function mergeLogs(left: ActivityLogDto[], right: ActivityLogDto[]) {
    const map = new Map<number, ActivityLogDto>();
    [...left, ...right].forEach((item) => {
        const key = item.id;
        map.set(key, { ...map.get(key), ...item });
    });
    return Array.from(map.values());
}
