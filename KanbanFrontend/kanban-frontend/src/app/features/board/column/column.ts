import { Component, computed, inject, input, output } from '@angular/core';
import { ColumnDto } from '../../../core/models/DTOs/column.model';
import { TaskTile } from './task-tile/task-tile';
import { Store } from '@ngrx/store';
import { selectTasksForColumn } from '../../../core/state/tasks/tasks.selectors';

@Component({
    selector: 'app-column',
    imports: [TaskTile],
    templateUrl: './column.html',
    styleUrl: './column.scss',
})
export class Column {
    private store = inject(Store);
    thisColumn = input.required<ColumnDto>();

    tasks = computed(() => this.store.selectSignal(selectTasksForColumn(this.thisColumn().id))());
    canEdit = input.required<boolean>();

    onClickedTile(event: Event) {}

    onDeleteTile(event: Event) {}

    deleteRequest = output<number>();
    onDeleteColumn(event: Event) {
        event.stopPropagation();
        const confirmed = window.confirm(
            `Are you sure you want to delete "${this.thisColumn().name}" column?`,
        );
        if (confirmed) this.deleteRequest.emit(this.thisColumn().id);
    }
}
