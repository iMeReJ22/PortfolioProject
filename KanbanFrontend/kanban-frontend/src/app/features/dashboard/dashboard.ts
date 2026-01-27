import { Component, inject, signal } from '@angular/core';
import { Store } from '@ngrx/store';
import { BoardsActions } from '../../core/state/boards/boards.actions';
import { selectTilesByRole } from '../../core/state/boards/boards.selector';
import { BoardTile } from './board-tile/board-tile';
import { ɵInternalFormsSharedModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BoardFormModal } from './board-form-modal/board-form-modal';

@Component({
    selector: 'app-dashboard',
    imports: [BoardTile, ɵInternalFormsSharedModule, ReactiveFormsModule, BoardFormModal],
    templateUrl: './dashboard.html',
    styleUrl: './dashboard.scss',
})
export class Dashboard {
    private store = inject(Store);
    private router = inject(Router);
    ownerTiles = this.store.selectSignal(selectTilesByRole('owner'));
    guestTiles = this.store.selectSignal(selectTilesByRole('guest'));
    memberTiles = this.store.selectSignal(selectTilesByRole('member'));

    ngOnInit(): void {
        this.store.dispatch(BoardsActions.getDashboardBoardTiles());
    }
    onClickedTile(boardId: number) {
        this.router.navigate(['/board', boardId]);
        console.log(`Clicked: ${boardId}`);
    }
    onDeleteBoard(boardId: number) {
        this.store.dispatch(BoardsActions.deleteBoard({ boardId }));
    }

    isModalOpen = signal(false);
    openCreateBoardModal() {
        this.isModalOpen.set(true);
    }
    closeModal() {
        this.isModalOpen.set(false);
    }
}
