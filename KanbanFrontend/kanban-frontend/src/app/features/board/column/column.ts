import { Component, computed, inject, input, output, signal } from '@angular/core';
import { ColumnDto } from '../../../core/models/DTOs/column.model';
import { TaskTile } from './task-tile/task-tile';
import { Store } from '@ngrx/store';
import { selectTasksForColumn } from '../../../core/state/tasks/tasks.selectors';
import {
    CdkDragDrop,
    transferArrayItem,
    CdkDropList,
    DragDropModule,
} from '@angular/cdk/drag-drop';
import { TaskDto, TaskForColumnDto } from '../../../core/models/DTOs/task.model';
import { TasksActions } from '../../../core/state/tasks/tasks.actions';

@Component({
    selector: 'app-column',
    imports: [TaskTile, CdkDropList, DragDropModule],
    templateUrl: './column.html',
    styleUrl: './column.scss',
})
export class Column {
    private store = inject(Store);
    thisColumn = input.required<ColumnDto>();

    tasks = computed(() => this.store.selectSignal(selectTasksForColumn(this.thisColumn().id))());
    canEdit = input.required<boolean>();
    clickedTileEvent = output<number>();
    onClickedTile(taskId: number) {
        this.clickedTileEvent.emit(taskId);
    }

    deleteRequest = output<number>();
    onDeleteColumn(event: Event) {
        event.stopPropagation();
        const confirmed = window.confirm(
            `Are you sure you want to delete "${this.thisColumn().name}" column?`,
        );
        if (confirmed) this.deleteRequest.emit(this.thisColumn().id);
    }
    openCreateTaskModal = output<number>();
    onCreateTask() {
        this.openCreateTaskModal.emit(this.thisColumn().id);
    }

    onTaskDrop(event: CdkDragDrop<TaskForColumnDto[]>) {
        if (
            event.previousContainer === event.container &&
            event.previousIndex === event.currentIndex
        ) {
            return;
        } else {
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex,
            );

            const task = event.item.data;
            const newColumnId = this.thisColumn().id;

            console.log(task, newColumnId);

            this.store.dispatch(
                TasksActions.moveTask({
                    move: {
                        taskId: task.id,
                        targetColumnId: newColumnId,
                        newOrderIndex: event.currentIndex,
                    },
                }),
            );
        }
    }
}
