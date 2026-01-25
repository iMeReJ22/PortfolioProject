import { BoardMemberDto } from './board-member.models';
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
