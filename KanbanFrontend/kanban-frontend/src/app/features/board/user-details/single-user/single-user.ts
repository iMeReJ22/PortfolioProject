import { Component, input } from '@angular/core';
import { UserDto } from '../../../../core/models/DTOs/user.model';

@Component({
    selector: 'app-single-user',
    imports: [],
    templateUrl: './single-user.html',
    styleUrl: './single-user.scss',
})
export class SingleUser {
    thisUser = input.required<UserDto>();
}
