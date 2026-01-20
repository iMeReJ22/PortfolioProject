export interface CreateUserRequest {
    email: string;
    displayName: string;
    password: string;
}
export interface UpdateUserRequest {
    userId: number;
    displayName: string;
    password: string;
}
export interface LoginRequest {
    email: string;
    password: string;
}
