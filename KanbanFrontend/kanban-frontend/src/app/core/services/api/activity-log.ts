import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api';
import { Observable } from 'rxjs';
import { ActivityLogDto } from '../../models/DTOs/activity-log.model';

@Injectable({
    providedIn: 'root',
})
export class ActivityLogApiService {
    private readonly endpoint = 'ActivityLog';
    constructor(private api: BaseApiService) {}
    getActivityForBoard(boardId: number): Observable<ActivityLogDto[]> {
        return this.api.get<ActivityLogDto[]>('${this.endpoint}/${boardId}');
    }
}
