import { Component, inject, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { BoardsActions } from '../../../core/state/boards/boards.actions';
import { selectLoggedUser } from '../../../core/state/users/users.selector';
import { selectBoardsStatus } from '../../../core/state/boards/boards.selector';

@Component({
    selector: 'app-board-form-modal',
    imports: [ReactiveFormsModule],
    templateUrl: './board-form-modal.html',
    styleUrl: './board-form-modal.scss',
})
export class BoardFormModal {
    private store = inject(Store);
    private fb = inject(FormBuilder);

    private loggedUser = this.store.selectSignal(selectLoggedUser);
    newBoardForm = this.fb.group({
        boardName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
        boardDescription: ['', [Validators.required, Validators.maxLength(500)]],
    });

    status = this.store.selectSignal(selectBoardsStatus);
    onSubmit(): void {
        this.store.dispatch(
            BoardsActions.createBoard({
                create: {
                    name: this.newBoardForm.getRawValue().boardName!,
                    description: this.newBoardForm.getRawValue().boardDescription!,
                    ownerId: this.loggedUser()?.id!,
                },
                tempId: Date.now(),
            }),
        );
        this.closeModal();
    }

    closeFormModal = output<void>();
    closeModal() {
        this.newBoardForm.reset();
        this.closeFormModal.emit();
    }
}
