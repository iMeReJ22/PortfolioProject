import { createActionGroup, emptyProps, props } from '@ngrx/store';
import {
    CreateUserRequest,
    LoginRequest,
    UpdateUserRequest,
} from '../../models/Requests/user-requests.models';
import { UserDto } from '../../models/DTOs/user.model';
import { LoginResultDto } from '../../models/DTOs/login-result.models';

export const UsersActions = createActionGroup({
    source: 'Users API',
    events: {
        'Create User': props<{ create: CreateUserRequest }>(),
        'Create User Success': props<{ createdUser: UserDto }>(),
        'Create User Failure': props<{ error: string }>(),

        Login: props<{ login: LoginRequest }>(),
        'Login Success': props<{ result: LoginResultDto }>(),
        'Login Failure': props<{ error: string }>(),

        'Get User By Id': props<{ userId: number }>(),
        'Get User By Id Success': props<{ user: UserDto }>(),
        'Get User By Id Failure': props<{ error: string }>(),

        'Get Users': emptyProps(),
        'Get Users Success': props<{ users: UserDto[] }>(),
        'Get Users Failure': props<{ error: string }>(),

        'Update User': props<{ userId: number; update: UpdateUserRequest }>(),
        'Update User Success': props<{ updatedUser: UserDto }>(),
        'Update User Failure': props<{ error: string; oldUser: UserDto }>(),

        'Delete User': props<{ userId: number }>(),
        'Delete User Success': emptyProps(),
        'Delete User Failure': props<{ error: string; deletedUser: UserDto }>(),

        'Get Users By Board': props<{ boardId: number }>(),
        'Get Users By Board Success': props<{ users: UserDto[] }>(),
        'Get Users By Board Failure': props<{ error: string }>(),

        Logout: emptyProps(),
    },
});
