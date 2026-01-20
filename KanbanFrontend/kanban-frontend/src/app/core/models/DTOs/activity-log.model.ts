export interface ActivityLogDto {
    id: number;
    name: string;
    description: string;
    createdAt: Date;
    boardId: number;
    userId?: number;
    activityAuthorId?: number;
    tagId?: number;
    columnId?: number;
    taskId?: number;
    taskCommentId?: number;
}
