import { createReducer, on } from '@ngrx/store';
import { TagDto } from '../../models/DTOs/tag.model';
import { TagsActions } from './tags.actions';

export interface TagState {
    tags: TagDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialTagsState: TagState = {
    tags: [],
    error: null,
    status: 'idle',
};

export const tagReducer = createReducer(
    initialTagsState,

    on(TagsActions.createTag, (state, { create, tempId }) => {
        const optimisticTag: TagDto = {
            id: tempId as any,
            name: create.name,
            colorHex: create.colorHex,
            boardId: create.boardId,
            createdAt: new Date(Date.now()),
        };

        return { ...state, status: 'creating', tags: [...state.tags, optimisticTag] };
    }),
    on(TagsActions.createTagSuccess, (state, { createdTag, tempId }) => ({
        ...state,
        status: 'success',
        error: null,
        tags: state.tags.map((t) => (t.id === tempId ? createdTag : t)),
    })),
    on(TagsActions.createTagFailure, (state, { error, tempId }) => ({
        ...state,
        status: 'error',
        error,
        tags: state.tags.filter((t) => t.id !== tempId),
    })),

    on(TagsActions.getTagsForBoard, (state, { boardId }) => ({
        ...state,
        status: 'loading',
    })),
    on(TagsActions.getTagsForBoardSuccess, (state, { tags }) => ({
        ...state,
        status: 'success',
        error: null,
        tags: tags,
    })),
    on(TagsActions.getTagsForBoardFailure, (state, { error }) => ({
        ...state,
        status: 'error',
        error,
    })),

    on(TagsActions.updateTag, (state, { tagId, update }) => ({
        ...state,
        status: 'updating',
        tags: state.tags.map((t) => (t.id === update.id ? { ...t, ...update } : t)),
    })),
    on(TagsActions.updateTagSuccess, (state, { updatedTag }) => ({
        ...state,
        status: 'success',
        error: null,
        tags: state.tags.map((t) => (t.id === updatedTag.id ? updatedTag : t)),
    })),
    on(TagsActions.updateTagFailure, (state, { error, tagBefore }) => ({
        ...state,
        status: 'error',
        error,
        tags: state.tags.map((t) => (t.id === tagBefore.id ? tagBefore : t)),
    })),

    on(TagsActions.deleteTag, (state, { tagId }) => ({
        ...state,
        status: 'deleting',
        tags: state.tags.filter((t) => t.id !== tagId),
    })),
    on(TagsActions.deleteTagSuccess, (state, {}) => ({
        ...state,
        status: 'success',
        error: null,
        tags: state.tags,
    })),
    on(TagsActions.deleteTagFailure, (state, { error, deletedTag }) => ({
        ...state,
        status: 'error',
        error,
        tags: [...state.tags, deletedTag].sort(
            (a, b) => a.createdAt.getTime() - b.createdAt.getTime(),
        ),
    })),

    on(TagsActions.upsertTags, (state, { tags }) => {
        return {
            ...state,
            tags: mergeTag(state.tags, tags),
        };
    }),
);
function mergeTag(left: TagDto[], right: TagDto[]) {
    const map = new Map<number, TagDto>();
    [...left, ...right].forEach((item) => {
        const key = item.id;
        map.set(key, { ...map.get(key), ...item });
    });
    return Array.from(map.values());
}
