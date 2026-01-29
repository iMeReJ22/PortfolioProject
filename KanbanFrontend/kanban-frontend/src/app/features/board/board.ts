import { Component, computed, inject, input, signal, numberAttribute } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectBoardById, selectUserRoleForBoard } from '../../core/state/boards/boards.selector';
import { BoardsActions } from '../../core/state/boards/boards.actions';
import { selectColumnsByBoardId } from '../../core/state/columns/columns.selector';
import { Column } from './column/column';
import { ReactiveFormsModule } from '@angular/forms';
import { ColumnsActions } from '../../core/state/columns/columns.actions';
import { TasksActions } from '../../core/state/tasks/tasks.actions';
import { ColumnFormModal } from './column-form-modal/column-form-modal';
import { TaskFormModal } from './task-form-modal/task-form-modal';
import { TaskDetails } from './task-details/task-details';
import { DragDropModule } from '@angular/cdk/drag-drop';

@Component({
    selector: 'app-board',
    templateUrl: './board.html',
    styleUrl: './board.scss',
    imports: [
        Column,
        ReactiveFormsModule,
        ColumnFormModal,
        TaskFormModal,
        TaskDetails,
        DragDropModule,
    ],
})
export class Board {
    id = input.required({ transform: numberAttribute });

    private store = inject(Store);

    thisUserRole = computed(() => this.store.selectSignal(selectUserRoleForBoard(this?.id()))());
    canEdit = computed(
        () =>
            this.thisUserRole()?.toLowerCase() === 'owner' ||
            this.thisUserRole()?.toLowerCase() === 'member',
    );
    thisBoard = computed(() => this.store.selectSignal(selectBoardById(this?.id()))());
    columns = computed(() => this.store.selectSignal(selectColumnsByBoardId(this?.id()))());

    ngOnInit() {
        this.store.dispatch(BoardsActions.getDetailedBoardById({ boardId: this.id() }));
        this.store.dispatch(TasksActions.getTaskTypes());
    }

    isCreateColumnModalOpen = signal(false);
    openCreateColumnModal() {
        this.isCreateColumnModalOpen.set(true);
    }
    closeCreateColumnModal() {
        this.isCreateColumnModalOpen.set(false);
    }

    deleteColumn(columnId: number) {
        this.store.dispatch(ColumnsActions.deleteColumn({ columnId }));
    }

    isCreateTaskModalOpen = signal(false);
    createTaskColumnId = signal<number>(0);
    openCreateTaskModal(columnId: number) {
        this.createTaskColumnId.set(columnId);
        this.isCreateTaskModalOpen.set(true);
    }
    closeCreateTaskModal() {
        this.isCreateTaskModalOpen.set(false);
    }

    isTaskDetailsOpen = signal(false);
    lastClickedTaskId = signal(0);
    openTaskDetails(taskId: number) {
        this.lastClickedTaskId.set(taskId);
        this.isTaskDetailsOpen.set(true);
    }
    closeTaskDetails() {
        this.isTaskDetailsOpen.set(false);
    }
}
