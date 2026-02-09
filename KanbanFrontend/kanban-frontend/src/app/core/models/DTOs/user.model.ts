export interface UserDto {
    id: number;
    email: string;
    displayName: string;
    role?: 'owner' | 'member' | 'guest';
    createdAt: Date;
}
