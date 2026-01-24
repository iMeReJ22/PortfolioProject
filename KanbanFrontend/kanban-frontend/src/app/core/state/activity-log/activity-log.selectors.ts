import { createFeatureSelector, createSelector } from '@ngrx/store';
import { LogState } from './activity-log.reducer';

export const selectLogsState = createFeatureSelector<LogState>('logs');

export const selectAllLogs = createSelector(selectLogsState, (state: LogState) => state.logs);

export const selectLogsError = createSelector(selectLogsState, (state: LogState) => state.error);

export const selectLogsStatus = createSelector(selectLogsState, (state: LogState) => state.status);
