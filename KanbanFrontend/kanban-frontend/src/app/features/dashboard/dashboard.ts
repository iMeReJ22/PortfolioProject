import { Component, inject, signal } from '@angular/core';
import { Store } from '@ngrx/store';
import { BoardsActions } from '../../core/state/boards/boards.actions';
import { selectBoardsStatus, selectTilesByRole } from '../../core/state/boards/boards.selector';
import { BoardTile } from './board-tile/board-tile';
import {
    FormBuilder,
    Validators,
    ɵInternalFormsSharedModule,
    ReactiveFormsModule,
} from '@angular/forms';
import { selectLoggedUser } from '../../core/state/users/users.selector';

@Component({
    selector: 'app-dashboard',
    imports: [BoardTile, ɵInternalFormsSharedModule, ReactiveFormsModule],
    templateUrl: './dashboard.html',
    styleUrl: './dashboard.scss',
})
export class Dashboard {
    private fb = inject(FormBuilder);
    private store = inject(Store);
    isModalOpen = signal(false);
    private loggedUser = this.store.selectSignal(selectLoggedUser);
    ownerTiles = this.store.selectSignal(selectTilesByRole('owner'));
    guestTiles = this.store.selectSignal(selectTilesByRole('guest'));
    memberTiles = this.store.selectSignal(selectTilesByRole('member'));
    status = this.store.selectSignal(selectBoardsStatus);

    newBoardForm = this.fb.group({
        boardName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
        boardDescription: ['', [Validators.required, Validators.maxLength(500)]],
    });

    onSubmit(): void {
        this.store.dispatch(
            BoardsActions.createBoard({
                create: {
                    name: this.newBoardForm.getRawValue().boardName!,
                    description: this.newBoardForm.getRawValue().boardDescription!,
                    ownerId: this.loggedUser()?.id!,
                },
                tempId: Date.now(),
            }),
        );
        this.closeModal();
    }

    ngOnInit(): void {
        this.store.dispatch(BoardsActions.getDashboardBoardTiles());
    }
    openCreateBoardModal() {
        this.isModalOpen.set(true);
    }
    closeModal() {
        this.newBoardForm.reset();
        this.isModalOpen.set(false);
    }
    onDeleteBoard(boardId: number) {
        this.store.dispatch(BoardsActions.deleteBoard({ boardId }));
    }
    onClickedTile(boardId: number) {
        console.log(`Clicked: ${boardId}`);
    }
}
