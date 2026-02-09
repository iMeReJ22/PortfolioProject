import { Component, computed, inject, input, output } from '@angular/core';
import { TaskDto, TaskForColumnDto } from '../../../../core/models/DTOs/task.model';
import { Tag } from '../../tag/tag';
import { DatePipe, NgStyle } from '@angular/common';

@Component({
    selector: 'app-task-tile',
    imports: [Tag, DatePipe, NgStyle],
    templateUrl: './task-tile.html',
    styleUrl: './task-tile.scss',
})
export class TaskTile {
    thisTask = input.required<TaskForColumnDto>();
    canEdit = input.required<boolean>();

    test() {
        this.thisTask().type.colorHex;
    }
    tileClick = output<number>();
    onTileClick() {
        this.tileClick.emit(this.thisTask().id);
    }

    typeColor = computed(() => this.thisTask().type.colorHex);
    setBorderColor() {
        const styles = {
            'border-color': this.thisTask().type.colorHex,
        };
        return styles;
    }
    setFontColor() {
        const styles = {
            color: this.thisTask().type.colorHex,
        };
        return styles;
    }
}
