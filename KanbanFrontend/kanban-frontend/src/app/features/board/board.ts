import {} from '@angular/common';
import { Component, computed, effect, inject, input, signal, numberAttribute } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectBoardById, selectUserRoleForBoard } from '../../core/state/boards/boards.selector';
import { BoardsActions } from '../../core/state/boards/boards.actions';
import {
    selectColumnsByBoardId,
    selectColumnsStatus,
} from '../../core/state/columns/columns.selector';
import { Column } from './column/column';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ColumnsActions } from '../../core/state/columns/columns.actions';
import { TasksActions } from '../../core/state/tasks/tasks.actions';
import { ColumnFormModal } from './column-form-modal/column-form-modal';

@Component({
    selector: 'app-board',
    templateUrl: './board.html',
    styleUrl: './board.scss',
    imports: [Column, ReactiveFormsModule, ColumnFormModal],
})
export class Board {
    id = input.required({ transform: numberAttribute });

    private store = inject(Store);

    canEdit = computed(
        () =>
            this.thisUserRole()?.toLowerCase() === 'owner' ||
            this.thisUserRole()?.toLowerCase() === 'member',
    );
    thisUserRole = computed(() => this.store.selectSignal(selectUserRoleForBoard(this?.id()))());
    thisBoard = this.store.selectSignal(selectBoardById(2));
    columns = computed(() => this.store.selectSignal(selectColumnsByBoardId(this?.id()))());

    ngOnInit() {
        this.store.dispatch(BoardsActions.getDetailedBoardById({ boardId: this.id() }));
        this.store.dispatch(TasksActions.getTaskTypes());
    }

    closeCreateColumnModal() {
        this.isCreateColumnModalOpen.set(false);
    }
    isCreateColumnModalOpen = signal(false);
    openCreateColumnModal() {
        this.isCreateColumnModalOpen.set(true);
    }

    constructor() {}
}
