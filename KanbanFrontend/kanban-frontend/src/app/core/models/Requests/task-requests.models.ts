export interface AssignTagToTaskRequest {
    taskId: number;
    tagId: number;
}
export interface RemoveTagFromTaskRequest {
    taskId: number;
    tagId: number;
}
export interface CreateTaskRequest {
    columnId: number;
    title: string;
    description?: string;
    taskTypeId: number;
    createdByUserId: number;
}
export interface UpdateTaskRequest {
    taskId: number;
    title: string;
    description: string;
    taskTypeId: number;
}
export interface ReorderTasksRequest {
    columnId: number;
    tasks: TaskOrderDto[];
}
export interface TaskOrderDto {
    taskId: number;
    orderIndex: number;
}
export interface MoveTaskRequest {
    taskId: number;
    targetColumnId: number;
    newOrderIndex: number;
}
