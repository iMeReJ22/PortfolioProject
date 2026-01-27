import { ActivityLogDto } from './activity-log.model';
import { BoardMemberDto, DetailedBoardMemberDto } from './board-member.models';
import { ColumnDto, DetailedColumnDto } from './column.model';
import { TagDto } from './tag.model';
import { UserDto } from './user.model';

export interface BoardDto {
    id: number;
    name: string;
    description: string;
    createdAt: Date;
    ownerId: number;
}

export interface BoardTileDto {
    id: number;
    name: string;
    description: string;
    createdAt: Date;
    owner: UserDto;
    boardMembers: BoardMemberDto[];
}

export interface DetailedBoardDto extends BoardDto {
    boardMembers: DetailedBoardMemberDto[];
    columns: DetailedColumnDto[];
    activityLogs: ActivityLogDto[];
    tags: TagDto[];
}
