import { createActionGroup, emptyProps, props } from '@ngrx/store';
import {
    AddBoardMemberRequest,
    CreateBoardRequest,
    UpdateBoardRequest,
} from '../../models/Requests/board-requests.models';
import { BoardDto, BoardTileDto, DetailedBoardDto } from '../../models/DTOs/board.model';
import { BoardMemberDto } from '../../models/DTOs/board-member.models';
import { TaskTypeDto } from '../../models/DTOs/task-type.model';

export const BoardsActions = createActionGroup({
    source: 'Boards API',
    events: {
        'Create Board': props<{ create: CreateBoardRequest; tempId: number }>(),
        'Create Board Success': props<{ created: BoardDto; tempId: number }>(),
        'Create Board Failure': props<{ error: string; tempId: number }>(),

        'Get Board By Id': props<{ boardId: number }>(),
        'Get Board By Id Success': props<{ board: BoardDto }>(),
        'Get Board By Id Failure': props<{ error: string }>(),

        'Get Boards For User': props<{ userId: number }>(),
        'Get Boards For User Success': props<{ boards: BoardDto[] }>(),
        'Get Boards For User Failure': props<{ error: string }>(),

        'Update Board': props<{ boardId: number; update: UpdateBoardRequest }>(),
        'Update Board Success': props<{ board: BoardDto }>(),
        'Update Board Failure': props<{ error: string; boardBefore: BoardDto }>(),

        'Delete Board': props<{ boardId: number }>(),
        'Delete Board Success': emptyProps(),
        'Delete Board Failure': props<{ error: string; deletedBoard: BoardDto }>(),

        'Add Member To Board': props<{ boardId: number; request: AddBoardMemberRequest }>(),
        'Add Member To Board Success': emptyProps(),
        'Add Member To Board Failure': props<{ error: string; boardId: number; userId: number }>(),

        'Remove Member From Board': props<{ boardId: number; userId: number }>(),
        'Remove Member From Board Success': emptyProps(),
        'Remove Member From Board Failure': props<{
            error: string;
            removedBoardMember: BoardMemberDto;
        }>(),

        'Get Dashboard Board Tiles': emptyProps(),
        'Get Dashboard Board Tiles Success': props<{ tiles: BoardTileDto[] }>(),
        'Get Dashboard Board Tiles Failure': props<{ error: string }>(),

        'Upsert Board Members': props<{ members: BoardMemberDto[] }>(),

        'Get Detailed Board By Id': props<{ boardId: number }>(),
        'Get Detailed Board By Id Success': props<{ board: DetailedBoardDto }>(),
        'Get Detailed Board By Id Failure': props<{ error: string }>(),
    },
});
