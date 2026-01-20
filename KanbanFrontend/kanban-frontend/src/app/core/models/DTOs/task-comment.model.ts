export interface TaskCommentDto {
    id: number;
    content: string;
    createdAt: Date;
    taskId: number;
    authorId?: number;
    authorName?: string;
}
