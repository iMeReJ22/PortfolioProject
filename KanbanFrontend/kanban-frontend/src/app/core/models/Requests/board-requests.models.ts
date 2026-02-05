export interface AddBoardMemberRequest {
    boardId: number;
    userId: number;
    role: 'member' | 'guest';
}

export interface RemoveBoardMemberRequest {
    boardId: number;
    userId: number;
}

export interface CreateBoardRequest {
    name: string;
    description: string;
    ownerId: number;
}
export interface DeleteBoardRequest {
    boardId: number;
}
export interface UpdateBoardRequest {
    id: number;
    name?: string;
    description?: string;
}
