export interface UserDto {
    id: number;
    email: string;
    displayName: string;
    role?: 'owner' | 'user' | 'guest';
    createdAt: Date;
}
