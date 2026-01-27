import { createActionGroup, props } from '@ngrx/store';
import { ActivityLogDto } from '../../models/DTOs/activity-log.model';
export const LogsActions = createActionGroup({
    source: 'Logs API',
    events: {
        'Get Activity For Board': props<{ boardId: number }>(),
        'Get Activity For Board Success': props<{ logs: ActivityLogDto[] }>(),
        'Get Activity For Board Failure': props<{ error: string }>(),

        'Upset Activity': props<{ logs: ActivityLogDto[] }>(),
    },
});
