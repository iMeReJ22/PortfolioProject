import { Component, input } from '@angular/core';
import { TaskCommentDto } from '../../../../core/models/DTOs/task-comment.model';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-task-comment',
    imports: [DatePipe],
    templateUrl: './task-comment.html',
    styleUrl: './task-comment.scss',
})
export class TaskComment {
    thisComment = input.required<TaskCommentDto>();
}
