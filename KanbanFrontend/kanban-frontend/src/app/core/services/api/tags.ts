import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import { CreateTagRequest, UpdateTagRequest } from '../../models/Requests/tag-requests.models';
import { TagDto } from '../../models/DTOs/tag.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class Tags {
    private readonly endpoint = 'Tags';

    constructor(private api: BaseApiService) {}

    createTag(request: CreateTagRequest): Observable<TagDto> {
        return this.api.post<TagDto>(`${this.endpoint}`, request);
    }
    getTagsForBoard(boardId: number): Observable<TagDto[]> {
        return this.api.get<TagDto[]>(`${this.endpoint}/board/${boardId}`);
    }
    updateTag(tagId: number, request: UpdateTagRequest): Observable<TagDto> {
        return this.api.put<TagDto>(`${this.endpoint}/${tagId}`, request);
    }
    deleteTag(tagId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${tagId}`);
    }
}
