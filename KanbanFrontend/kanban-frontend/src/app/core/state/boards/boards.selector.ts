import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BoardState } from './boards.reducer';
import { selectAllUsers, selectUsersState } from '../users/users.selector';
import { UserState } from '../users/users.reducer';
import { BoardTileDto } from '../../models/DTOs/board.model';
import { BoardMemberDto } from '../../models/DTOs/board-member.models';
import { UserDto } from '../../models/DTOs/user.model';

export const selectBoardState = createFeatureSelector<BoardState>('boards');

export const selectAllBoardMembers = createSelector(
    selectBoardState,
    (state: BoardState) => state.boardMembers,
);

export const selectBoardMemberByIds = (boardId: number, userId: number) =>
    createSelector(selectAllBoardMembers, (bms) =>
        bms.find((bm) => bm.boardId === boardId && bm.userId === userId),
    );

export const selectAllBoards = createSelector(
    selectBoardState,
    (state: BoardState) => state.boards,
);

export const selectBoardById = (boardId: number) =>
    createSelector(selectAllBoards, (boards) => boards.find((board) => board.id === boardId));

export const selectBoardsError = createSelector(
    selectBoardState,
    (state: BoardState) => state.error,
);
export const selectBoardsStatus = createSelector(
    selectBoardState,
    (state: BoardState) => state.status,
);

export const selectBoardMembersByRoleInCurrentBoard = (role: 'owner' | 'guest' | 'member') =>
    createSelector(selectBoardState, (state: BoardState) =>
        state.boardMembers.filter(
            (bm) => bm.role === role && bm.boardId === state.currentBoard?.id,
        ),
    );

export const selectUsersByRoleInCurrentBoard = (role: 'owner' | 'guest' | 'member') =>
    createSelector(
        selectBoardMembersByRoleInCurrentBoard(role),
        selectAllUsers,
        (boardMembers, users) => {
            const resultUsers: UserDto[] = [];
            boardMembers.forEach((boardMember) => {
                const match = users.find((u) => u.id === boardMember.userId);
                if (match) resultUsers.push({ ...match, role: boardMember.role });
            });
            return resultUsers;
        },
    );

export const selectTilesByRole = (role: 'owner' | 'guest' | 'member') =>
    createSelector(
        selectBoardState,
        selectUsersState,
        (boardState: BoardState, userState: UserState) => {
            const userId = userState.loggedUser?.user.id;

            const memberships = boardState.boardMembers.filter(
                (bm) => bm.userId === userId && bm.role.toLowerCase() === role,
            );

            const boardsWithRoleForUser = boardState.boards.filter((b) => {
                const match = memberships.find((m) => m.boardId === b.id);
                return match ? true : false;
            });

            const boardTiles = boardsWithRoleForUser.map((b) => {
                const tile: BoardTileDto = {
                    ...b,
                    boardMembers: boardState.boardMembers.filter((bm) => bm.boardId === b.id),
                    owner: userState.users.find((u) => u.id === b.ownerId)!,
                };
                return tile;
            });
            return boardTiles;
        },
    );

export const selectRoleForIds = (userId: number, boardId: number) =>
    createSelector(
        selectBoardState,
        (state: BoardState) =>
            state.boardMembers.find((bm) => bm.boardId === boardId && bm.userId === userId)?.role,
    );

export const selectUserRoleForBoard = (boardId: number) =>
    createSelector(
        selectBoardState,
        selectUsersState,
        (boardState: BoardState, userState: UserState) =>
            boardState.boardMembers.find(
                (bm) => bm.boardId === boardId && bm.userId === userState.loggedUser?.user.id,
            )?.role,
    );
