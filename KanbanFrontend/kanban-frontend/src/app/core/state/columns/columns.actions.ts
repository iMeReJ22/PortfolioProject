import { createActionGroup, emptyProps, props } from '@ngrx/store';
import {
    CreateColumnRequest,
    ReorderColumnsRequest,
    UpdateColumnRequest,
} from '../../models/Requests/column-requests.models';
import { ColumnDto } from '../../models/DTOs/column.model';

export const ColumnsActions = createActionGroup({
    source: 'Columns API',
    events: {
        'Create Column': props<{ request: CreateColumnRequest; tempId: number }>(),
        'Create Column Success': props<{ created: ColumnDto; tempId: number }>(),
        'Create Column Failure': props<{ error: string; tempId: number }>(),

        'Get Columns For Board': props<{ boardId: number }>(),
        'Get Columns For Board Success': props<{ columns: ColumnDto[] }>(),
        'Get Columns For Board Failure': props<{ error: string }>(),

        'Update Column': props<{ columnId: number; request: UpdateColumnRequest }>(),
        'Update Column Success': props<{ updatedColumn: ColumnDto }>(),
        'Update Column Failure': props<{ error: string; columnBefore: ColumnDto }>(),

        'Delete Column': props<{ columnId: number }>(),
        'Delete Column Success': emptyProps(),
        'Delete Column Failure': props<{ error: string; deletedColumn: ColumnDto }>(),

        'Reorder Columns': props<{ boardId: number; request: ReorderColumnsRequest }>(),
        'Reorder Columns Success': emptyProps(),
        'Reorder Columns Failure': props<{ error: string; columnsBefore: ColumnDto[] }>(),

        'Upsert Columns': props<{ columns: ColumnDto[] }>(),
    },
});
