import { Component, computed, inject, input, Input, output, signal } from '@angular/core';
import { BoardTileDto } from '../../../core/models/DTOs/board.model';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
@Component({
    selector: 'app-board-tile',
    imports: [CommonModule],
    templateUrl: './board-tile.html',
    styleUrl: './board-tile.scss',
})
export class BoardTile {
    isOwner = input.required<boolean>();
    boardTile = input.required<BoardTileDto>();
    tileCLick = output<number>();
    deleteRequest = output<number>();

    onTileClick() {
        this.tileCLick.emit(this.boardTile().id);
    }

    onDelete(event: Event) {
        event.stopPropagation();
        const confirmed = window.confirm(
            `Are you sure you want to delete"${this.boardTile().name}?`,
        );
        if (confirmed) this.deleteRequest.emit(this.boardTile().id);
    }
}
