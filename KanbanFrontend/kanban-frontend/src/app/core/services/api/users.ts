import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import {
    CreateUserRequest,
    LoginRequest,
    UpdateUserRequest,
} from '../../models/Requests/user-requests.models';
import { Observable } from 'rxjs';
import { UserDto } from '../../models/DTOs/user.model';
import { LoginResultDto } from '../../models/DTOs/login-result.models';

@Injectable({
    providedIn: 'root',
})
export class UsersApiService {
    private readonly endpoint = 'Users';

    constructor(private api: BaseApiService) {}

    createUser(request: CreateUserRequest): Observable<UserDto> {
        return this.api.post<UserDto>(this.endpoint, request);
    }
    login(request: LoginRequest): Observable<LoginResultDto> {
        return this.api.post<LoginResultDto>(`${this.endpoint}/login`, request);
    }
    getUsers(): Observable<UserDto[]> {
        return this.api.get<UserDto[]>(this.endpoint);
    }
    getUserById(userId: number): Observable<UserDto> {
        return this.api.get<UserDto>(`${this.endpoint}/${userId}`);
    }
    updateUser(userId: number, request: UpdateUserRequest): Observable<UserDto> {
        return this.api.put<UserDto>(`${this.endpoint}/${userId}`, request);
    }
    deleteUser(userId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${userId}`);
    }
    me(): Observable<UserDto> {
        return this.api.get<UserDto>(`${this.endpoint}/me`);
    }
    getUsersByBoard(boardId: number): Observable<UserDto[]> {
        return this.api.get<UserDto[]>(`${this.endpoint}/board/${boardId}`);
    }
}
