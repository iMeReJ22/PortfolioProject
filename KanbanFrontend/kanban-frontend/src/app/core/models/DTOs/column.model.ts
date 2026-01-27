import { DetailedTaskDto } from './task.model';

export interface ColumnDto {
    id: number;
    name: string;
    orderIndex: number;
    boardId: number;
    taskIds: number[];
}

export interface DetailedColumnDto extends ColumnDto {
    tasks: DetailedTaskDto[];
}
