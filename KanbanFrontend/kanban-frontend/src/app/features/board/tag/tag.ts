import { Component, input } from '@angular/core';
import { TagDto } from '../../../core/models/DTOs/tag.model';

@Component({
    selector: 'app-tag',
    imports: [],
    templateUrl: './tag.html',
    styleUrl: './tag.scss',
})
export class Tag {
    thisTag = input.required<TagDto>();
}
