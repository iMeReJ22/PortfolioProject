import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TagState } from './tags.reducer';

export const selectTagsState = createFeatureSelector<TagState>('tags');

export const selectAllTags = createSelector(selectTagsState, (state: TagState) => state.tags);

export const selectTagById = (tagId: number) =>
    createSelector(selectAllTags, (tags) => tags.find((t) => t.id === tagId));

export const selectTagsError = createSelector(selectTagsState, (state: TagState) => state.error);
export const selectTagsStatus = createSelector(selectTagsState, (state: TagState) => state.status);

export const selectTagsByIds = (tagIds: number[]) =>
    createSelector(selectAllTags, (tags) => tags.filter((t) => tagIds.includes(t.id)));
