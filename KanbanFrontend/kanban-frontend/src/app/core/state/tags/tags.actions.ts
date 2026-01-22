import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { CreateTagRequest, UpdateTagRequest } from '../../models/Requests/tag-requests.models';
import { TagDto } from '../../models/DTOs/tag.model';

export const TagsActions = createActionGroup({
    source: 'Tags API',
    events: {
        'Create Tag': props<{ create: CreateTagRequest; tempId: number }>(),
        'Create Tag Success': props<{ createdTag: TagDto; tempId: number }>(),
        'Create Tag Failure': props<{ error: string; tempId: number }>(),

        'Get Tags For Board': props<{ boardId: number }>(),
        'Get Tags For Board Success': props<{ tags: TagDto[] }>(),
        'Get Tags For Board Failure': props<{ error: string }>(),

        'Update Tag': props<{ tagId: number; update: UpdateTagRequest }>(),
        'Update Tag Success': props<{ updatedTag: TagDto }>(),
        'Update Tag Failure': props<{ error: string; tagBefore: TagDto }>(),

        'Delete Tag': props<{ tagId: number }>(),
        'Delete Tag Success': emptyProps(),
        'Delete Tag Failure': props<{ error: string; deletedTag: TagDto }>(),
    },
});
