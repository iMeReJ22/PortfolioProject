import { TaskDto } from './task.model';

export interface ColumnDto {
    id: number;
    name: string;
    orderIndex: number;
    boardId: number;
    tasks: TaskDto[];
}
