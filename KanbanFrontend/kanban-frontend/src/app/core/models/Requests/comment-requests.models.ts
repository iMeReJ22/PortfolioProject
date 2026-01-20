export interface CreateCommentRequest {
    taskId: number;
    authorId: number;
    content: string;
}
export interface UpdateCommentRequest {
    commentId: number;
    content: string;
}
