import { Component, computed, effect, inject, input, output } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectDetailedTask } from '../../../core/state/tasks/tasks.selectors';
import { Tag } from '../tag/tag';
import { TaskComment } from './task-comment/task-comment';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommentsActions } from '../../../core/state/comments/comments.actions';
import {
    selectAllCommentsForTask,
    selectCommentsStatus,
} from '../../../core/state/comments/comments.selector';
import { selectLoggedUser } from '../../../core/state/users/users.selector';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-task-details',
    imports: [Tag, TaskComment, ReactiveFormsModule, DatePipe],
    templateUrl: './task-details.html',
    styleUrl: './task-details.scss',
})
export class TaskDetails {
    private store = inject(Store);
    private fb = inject(FormBuilder);

    comments = computed(() => this.store.selectSignal(selectAllCommentsForTask(this.taskId()))());
    loggedUser = this.store.selectSignal(selectLoggedUser);
    taskId = input.required<number>();
    thisTask = computed(() => this.store.selectSignal(selectDetailedTask(this.taskId()!))());
    constructor() {
        effect(() => {
            this.store.dispatch(CommentsActions.getCommentsForTask({ taskId: this.taskId()! }));
            this.newCommentForm.reset();
        });
    }
    status = this.store.selectSignal(selectCommentsStatus);
    newCommentForm = this.fb.group({
        commentContent: ['', [Validators.required]],
    });
    onSubmitCommentForm() {
        this.store.dispatch(
            CommentsActions.createComment({
                create: {
                    taskId: this.taskId(),
                    authorId: this.loggedUser()?.id!,
                    content: this.newCommentForm.getRawValue().commentContent!,
                },
                tempId: Date.now(),
            }),
        );
    }

    closeTaskDetailsEvent = output<void>();
    closeTaskDetails() {
        this.closeTaskDetailsEvent.emit();
    }
}
