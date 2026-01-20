export interface CreateColumnRequest {
    boardId: number;
    name: string;
}
export interface UpdateColumnRequest {
    columnId: number;
    name: string;
}
export interface ReorderColumnsRequest {
    boardId: number;
    columns: ColumnOrderDto[];
}
interface ColumnOrderDto {
    columnId: number;
    orderIndex: number;
}
