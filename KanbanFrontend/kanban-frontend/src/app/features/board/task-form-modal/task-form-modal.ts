import { Component, computed, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { TasksActions } from '../../../core/state/tasks/tasks.actions';
import { selectLoggedUser } from '../../../core/state/users/users.selector';
import { selectTaskTypesDict, selectTasksStatus } from '../../../core/state/tasks/tasks.selectors';

@Component({
    selector: 'app-task-form-modal',
    imports: [ReactiveFormsModule],
    templateUrl: './task-form-modal.html',
    styleUrl: './task-form-modal.scss',
})
export class TaskFormModal {
    private fb = inject(FormBuilder);
    private store = inject(Store);

    columnId = input.required<number>();
    taskId = input.required<number | null>();

    closeModal = output<void>();
    closeCreateTaskModal() {
        this.closeModal.emit();
    }

    taskTypesDict = computed(() => Object.entries(this.store.selectSignal(selectTaskTypesDict)()));

    newTaskForm = this.fb.group({
        taskTitle: ['', [Validators.required, Validators.maxLength(200)]],
        taskType: ['', [Validators.required]],
        taskDescription: ['', [Validators.required]],
    });
    user = this.store.selectSignal(selectLoggedUser);
    taskStatus = this.store.selectSignal(selectTasksStatus);
    onSubmitCreateTask() {
        if (this.taskId() === null) {
            this.store.dispatch(
                TasksActions.createTask({
                    create: {
                        columnId: this.columnId()!,
                        title: this.newTaskForm.getRawValue().taskTitle!,
                        description: this.newTaskForm.getRawValue().taskDescription!,
                        taskTypeId: Number.parseInt(this.newTaskForm.getRawValue().taskType!),
                        createdByUserId: this.user()?.id!,
                    },
                    tempId: Date.now(),
                }),
            );
        } else {
            this.store.dispatch(
                TasksActions.updateTask({
                    update: {
                        id: this.taskId()!,
                        title: this.newTaskForm.getRawValue().taskTitle!,
                        description: this.newTaskForm.getRawValue().taskDescription!,
                        taskTypeId: Number.parseInt(this.newTaskForm.getRawValue().taskType!),
                    },
                }),
            );
        }
        this.closeCreateTaskModal();
    }
}
