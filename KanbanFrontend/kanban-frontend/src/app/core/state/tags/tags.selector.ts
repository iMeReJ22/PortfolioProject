import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { TagState } from './tags.reducer';

export const selectTagsState = (state: AppState) => state.tagsState;

export const selectAllTags = createSelector(selectTagsState, (state: TagState) => state.tags);

export const selectTagById = (tagId: number) =>
    createSelector(selectAllTags, (tags) => tags.find((t) => t.id === tagId));
