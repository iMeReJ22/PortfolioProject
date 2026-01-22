import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { LogState } from './activity-log.reducer';

export const selectLogsState = (state: AppState) => state.logsState;

export const selectAllLogs = createSelector(selectLogsState, (state: LogState) => state.logs);
