import { UserDto } from './user.model';
export interface BoardMemberDto {
    userId: number;
    boardId: number;
    role: 'owner' | 'member' | 'guest';
    user: UserDto;
}
