import { Component, computed, inject, input, output } from '@angular/core';
import { TaskDto, TaskForColumnDto } from '../../../../core/models/DTOs/task.model';
import { Store } from '@ngrx/store';
import { Tag } from '../../tag/tag';

@Component({
    selector: 'app-task-tile',
    imports: [Tag],
    templateUrl: './task-tile.html',
    styleUrl: './task-tile.scss',
})
export class TaskTile {
    private store = inject(Store);
    thisTask = input.required<TaskForColumnDto>();
    canEdit = input.required<boolean>();

    tileClick = output<number>();
    onTileClick() {
        console.log(`clicked task: ${this.thisTask().id}`);
        this.tileClick.emit(this.thisTask().id);
    }
}
