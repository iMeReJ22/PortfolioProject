import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TagsApiService } from '../../services/api/tags';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { catchError, concatMap, map, mergeMap, of, switchMap } from 'rxjs';
import { TagsActions } from './tags.actions';
import { concatLatestFrom } from '@ngrx/operators';
import { selectTagById } from './tags.selector';
import { TagDto } from '../../models/DTOs/tag.model';

export class TagsEffects {
    private actions$ = inject(Actions);
    private store = inject(Store<AppState>);
    private tagsService = inject(TagsApiService);

    createTag$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TagsActions.createTag),
            mergeMap(({ create, tempId }) =>
                this.tagsService.createTag(create).pipe(
                    map((createdTag) => TagsActions.createTagSuccess({ createdTag, tempId })),
                    catchError((error) =>
                        of(TagsActions.createTagFailure({ error: error.message, tempId })),
                    ),
                ),
            ),
        );
    });

    getTagsForBoard$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TagsActions.getTagsForBoard),
            switchMap(({ boardId }) =>
                this.tagsService.getTagsForBoard(boardId).pipe(
                    map((tags) => TagsActions.getTagsForBoardSuccess({ tags })),
                    catchError((error) =>
                        of(TagsActions.getTagsForBoardFailure({ error: error.message })),
                    ),
                ),
            ),
        );
    });

    updateTag$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TagsActions.updateTag),
            concatLatestFrom(({ update }) => this.store.select(selectTagById(update.id))),
            concatMap(([{ tagId, update }, tagBefore]) =>
                this.tagsService.updateTag(tagId, update).pipe(
                    map((updatedTag) => TagsActions.updateTagSuccess({ updatedTag })),
                    catchError((error) =>
                        of(
                            TagsActions.updateTagFailure({
                                error: error.message,
                                tagBefore: tagBefore ?? ({} as TagDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });

    deleteTag$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(TagsActions.deleteTag),
            concatLatestFrom(({ tagId }) => this.store.select(selectTagById(tagId))),
            concatMap(([{ tagId }, deletedTag]) =>
                this.tagsService.deleteTag(tagId).pipe(
                    map(() => TagsActions.deleteTagSuccess()),
                    catchError((error) =>
                        of(
                            TagsActions.deleteTagFailure({
                                error: error.message,
                                deletedTag: deletedTag ?? ({} as TagDto),
                            }),
                        ),
                    ),
                ),
            ),
        );
    });
}
