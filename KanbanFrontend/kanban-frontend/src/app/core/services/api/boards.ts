import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import {
    AddBoardMemberRequest,
    CreateBoardRequest,
} from '../../models/Requests/board-requests.models';
import { BoardDto } from '../../models/DTOs/board.model';
import { Observable } from 'rxjs';
import { UserDto } from '../../models/DTOs/user.model';

@Injectable({
    providedIn: 'root',
})
export class Boards {
    private readonly endpoint = 'Boards';

    constructor(private api: BaseApiService) {}

    createBoard(request: CreateBoardRequest): Observable<BoardDto> {
        return this.api.post<BoardDto>(this.endpoint, request);
    }
    getBoardById(boardId: number): Observable<BoardDto> {
        return this.api.get<BoardDto>(`${this.endpoint}/${boardId}`);
    }
    getBoardsForUser(userId: number): Observable<BoardDto[]> {
        return this.api.get<BoardDto[]>(`${this.endpoint}/user/${userId}`);
    }
    updateBoard(boardId: number, request: CreateBoardRequest): Observable<BoardDto> {
        return this.api.put<BoardDto>(`${this.endpoint}/${boardId}`, request);
    }
    deleteBoard(boardId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${boardId}`);
    }
    addMemberToBoard(boardId: number, request: AddBoardMemberRequest): Observable<void> {
        return this.api.post<void>(`${this.endpoint}/${boardId}/members`, request);
    }
    getBoardMembers(boardId: number): Observable<UserDto[]> {
        return this.api.get<UserDto[]>(`${this.endpoint}/${boardId}/members`);
    }
    removeMemberFromBoard(boardId: number, userId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${boardId}/members/${userId}`);
    }
}
