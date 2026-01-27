import { Component, inject, input, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { ColumnsActions } from '../../../core/state/columns/columns.actions';
import { selectColumnsStatus } from '../../../core/state/columns/columns.selector';

@Component({
    selector: 'app-column-form-modal',
    imports: [ReactiveFormsModule],
    templateUrl: './column-form-modal.html',
    styleUrl: './column-form-modal.scss',
})
export class ColumnFormModal {
    private fb = inject(FormBuilder);
    private store = inject(Store);

    boardId = input.required<number>();

    hideModal = output<void>();
    closeCreateColumnModal() {
        this.newColumnForm.reset();
        this.hideModal.emit();
    }

    newColumnForm = this.fb.group({
        columnName: ['', [Validators.required, Validators.maxLength(100)]],
    });
    columnsStatus = this.store.selectSignal(selectColumnsStatus);
    onSubmitCreateColumn() {
        console.log(this.newColumnForm.getRawValue());
        this.store.dispatch(
            ColumnsActions.createColumn({
                request: {
                    boardId: this.boardId(),
                    name: this.newColumnForm.getRawValue().columnName!,
                },
                tempId: Date.now(),
            }),
        );
        this.closeCreateColumnModal();
    }
}
