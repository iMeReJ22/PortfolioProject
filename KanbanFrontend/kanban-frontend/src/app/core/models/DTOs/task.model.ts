import { TagDto } from './tag.model';
import { TaskCommentDto } from './task-comment.model';

export interface TaskDto {
    id: number;
    title: string;
    description: string;
    orderIndex: number;
    CreatedAt: Date;
    columnId: number;
    taskTypeId: number;
    createdByUserId?: number;
    tags: TagDto[];
    comments: TaskCommentDto[];
}
