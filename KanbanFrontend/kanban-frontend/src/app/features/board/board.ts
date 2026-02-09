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
import { ActivityLogs } from './activity-logs/activity-logs';
import { UserDetails } from './user-details/user-details';
import { UserFormModal } from './user-form-modal/user-form-modal';
import { TaskDto } from '../../core/models/DTOs/task.model';

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
        ActivityLogs,
        UserDetails,
        UserFormModal,
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
        this.columnToEditId.set(null);
    }

    deleteColumn(columnId: number) {
        this.store.dispatch(ColumnsActions.deleteColumn({ columnId }));
    }

    columnToEditId = signal<number | null>(null);
    editColumn(columnId: number) {
        this.columnToEditId.set(columnId);
        this.openCreateColumnModal();
    }

    isCreateTaskModalOpen = signal(false);
    createTaskColumnId = signal<number>(0);
    openCreateTaskModal(columnId: number) {
        this.createTaskColumnId.set(columnId);
        this.isCreateTaskModalOpen.set(true);
    }
    closeCreateTaskModal() {
        this.isCreateTaskModalOpen.set(false);
        this.editTask.set(null);
    }

    isTaskDetailsOpen = signal(false);
    lastClickedTaskId = signal(0);
    openTaskDetails(taskId: number) {
        this.lastClickedTaskId.set(taskId);
        this.isTaskDetailsOpen.set(true);
        this.closeActivityDetails();
        this.closeUserDetails();
    }
    closeTaskDetails() {
        this.isTaskDetailsOpen.set(false);
    }

    editTask = signal<TaskDto | null>(null);
    onEditTask(event: TaskDto) {
        this.editTask.set(event);
        this.openCreateTaskModal(event.columnId);
    }

    onDeleteTask(taskId: number) {
        this.store.dispatch(TasksActions.deleteTask({ taskId }));
    }

    isActivityDetailsOpen = signal(false);
    openActivityDetails() {
        this.isActivityDetailsOpen.set(true);
        this.closeTaskDetails();
        this.closeUserDetails();
    }
    closeActivityDetails() {
        this.isActivityDetailsOpen.set(false);
    }

    isUsersDetailsOpen = signal(false);
    openUserDetails() {
        this.isUsersDetailsOpen.set(true);
        this.closeTaskDetails();
        this.closeActivityDetails();
    }
    closeUserDetails() {
        this.isUsersDetailsOpen.set(false);
    }

    isAddUserModalOpen = signal(false);
    openAddUserModal() {
        this.isAddUserModalOpen.set(true);
    }
    closeAddUserModal() {
        this.isAddUserModalOpen.set(false);
    }
}
