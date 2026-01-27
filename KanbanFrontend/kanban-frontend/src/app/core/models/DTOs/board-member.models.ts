import { UserDto } from './user.model';

export interface BoardMemberDto {
    userId: number;
    boardId: number;
    role: 'owner' | 'member' | 'guest' | 'Owner';
}

export function compareRoles(a: BoardMemberDto, b: BoardMemberDto) {
    if (a.role === b.role) return 0;
    if (a.role === 'owner') {
        return 1;
    }
    if (a.role === 'member') {
        if (b.role === 'owner') return -1;
        if (b.role === 'guest') return 1;
    }
    return -1;
}

export interface DetailedBoardMemberDto extends BoardMemberDto {
    users: UserDto[];
}
