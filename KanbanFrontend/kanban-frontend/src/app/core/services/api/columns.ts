import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import {
    CreateColumnRequest,
    ReorderColumnsRequest,
    UpdateColumnRequest,
} from '../../models/Requests/column-requests.models';
import { Observable } from 'rxjs';
import { ColumnDto } from '../../models/DTOs/column.model';

@Injectable({
    providedIn: 'root',
})
export class Columns {
    private readonly endpoint = 'Columns';

    constructor(private api: BaseApiService) {}
    createColumn(request: CreateColumnRequest): Observable<ColumnDto> {
        return this.api.post<ColumnDto>(this.endpoint, request);
    }
    getColumnsForBoard(boardId: number): Observable<ColumnDto[]> {
        return this.api.get<ColumnDto[]>(`${this.endpoint}/board/${boardId}`);
    }
    updateColumn(columnId: number, request: UpdateColumnRequest): Observable<ColumnDto> {
        return this.api.put<ColumnDto>(`${this.endpoint}/${columnId}`, request);
    }
    deleteColumn(columnId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${columnId}`);
    }
    reorderColumns(boardId: number, request: ReorderColumnsRequest): Observable<void> {
        return this.api.post<void>(`${this.endpoint}/reorder/board/${boardId}`, request);
    }
}
