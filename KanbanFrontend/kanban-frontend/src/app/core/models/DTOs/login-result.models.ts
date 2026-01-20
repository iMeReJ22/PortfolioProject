import { UserDto } from './user.model';

export interface LoginResultDto {
    token: string;
    user: UserDto;
}
