import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import { Observable } from 'rxjs';
import { TaskDto } from '../../models/DTOs/task.model';
import {
    AssignTagToTaskRequest,
    CreateTaskRequest,
    MoveTaskRequest,
    ReorderTasksRequest,
    UpdateTaskRequest,
} from '../../models/Requests/task-requests.models';
import { TaskTypeDto } from '../../models/DTOs/task-type.model';

@Injectable({
    providedIn: 'root',
})
export class TasksApiService {
    private readonly endpoint = 'Tasks';

    constructor(private api: BaseApiService) {}

    createTask(request: CreateTaskRequest): Observable<TaskDto> {
        return this.api.post<TaskDto>(this.endpoint, request);
    }
    getTasksForColumn(columnId: number): Observable<TaskDto[]> {
        return this.api.get<TaskDto[]>(`${this.endpoint}/column/${columnId}`);
    }
    getTasksForBoard(boardId: number): Observable<TaskDto[]> {
        return this.api.get<TaskDto[]>(`${this.endpoint}/board/${boardId}`);
    }
    getTaskById(taskId: number): Observable<TaskDto> {
        return this.api.get<TaskDto>(`${this.endpoint}/${taskId}`);
    }
    updateTask(taskId: number, request: UpdateTaskRequest): Observable<TaskDto> {
        return this.api.put<TaskDto>(`${this.endpoint}/${taskId}`, request);
    }
    deleteTask(taskId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${taskId}`);
    }
    reorderTasks(request: ReorderTasksRequest): Observable<void> {
        return this.api.post<void>(`${this.endpoint}/reorder`, request);
    }
    moveTask(request: MoveTaskRequest): Observable<void> {
        return this.api.post<void>(`${this.endpoint}/move`, request);
    }
    assignTagToTask(taskId: number, request: AssignTagToTaskRequest): Observable<void> {
        return this.api.post<void>(`${this.endpoint}/${taskId}/assign-tag`, request);
    }
    removeTagFromTask(taskId: number, tagId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${taskId}/remove-tag/${tagId}`);
    }
    getTaskTypes(): Observable<TaskTypeDto[]> {
        return this.api.get<TaskTypeDto[]>(`${this.endpoint}/types`);
    }
}
