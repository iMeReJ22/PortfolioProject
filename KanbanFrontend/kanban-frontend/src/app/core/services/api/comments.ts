import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import {
    CreateCommentRequest,
    UpdateCommentRequest,
} from '../../models/Requests/comment-requests.models';
import { Observable } from 'rxjs';
import { TaskCommentDto } from '../../models/DTOs/task-comment.model';

@Injectable({
    providedIn: 'root',
})
export class CommentsApiService {
    private readonly endpoint = 'Comments';

    constructor(private api: BaseApiService) {}
    createComment(request: CreateCommentRequest): Observable<TaskCommentDto> {
        return this.api.post<TaskCommentDto>(`${this.endpoint}`, request);
    }
    getCommentsForTask(taskId: number): Observable<TaskCommentDto[]> {
        return this.api.get<TaskCommentDto[]>(`${this.endpoint}/task/${taskId}`);
    }
    updateComment(commentId: number, request: UpdateCommentRequest): Observable<TaskCommentDto> {
        return this.api.put<TaskCommentDto>(`${this.endpoint}/${commentId}`, request);
    }
    deleteComment(commentId: number): Observable<void> {
        return this.api.delete<void>(`${this.endpoint}/${commentId}`);
    }
}
