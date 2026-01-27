import { TagDto } from './tag.model';
import { TaskCommentDto } from './task-comment.model';
import { TaskTypeDto } from './task-type.model';

export interface TaskDto {
    id: number;
    title: string;
    description: string;
    orderIndex: number;
    createdAt: Date;
    columnId: number;
    taskTypeId: number;
    createdByUserId?: number;
    tagIds: number[];
    commentIds: number[];
}

export interface DetailedTaskDto extends TaskDto {
    tags: TagDto[];
    comments: TaskCommentDto[];
}

export interface TaskForColumnDto extends TaskDto {
    tags: TagDto[];
    type: TaskTypeDto;
}
