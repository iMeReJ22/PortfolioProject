export interface CreateTagRequest {
    boardId: number;
    name: string;
    colorHex: string;
}
export interface UpdateTagRequest {
    id: number;
    name: string;
    colorHex: string;
}
