import { Component, computed, effect, inject, input, output } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectUsersByRoleInCurrentBoard } from '../../../core/state/boards/boards.selector';
import { SingleUser } from './single-user/single-user';

@Component({
    selector: 'app-user-details',
    imports: [SingleUser],
    templateUrl: './user-details.html',
    styleUrl: './user-details.scss',
})
export class UserDetails {
    store = inject(Store);
    thisUserRole = input<string>();

    isOwner = computed(() => this.thisUserRole() === 'owner');

    ownerUser = this.store.selectSignal(selectUsersByRoleInCurrentBoard('owner'));
    memberUsers = this.store.selectSignal(selectUsersByRoleInCurrentBoard('member'));
    guestUsers = this.store.selectSignal(selectUsersByRoleInCurrentBoard('guest'));

    addUserEvent = output<void>();
    onAddUser() {
        this.addUserEvent.emit();
    }

    closeUserDetailsEvent = output<void>();
    closeUserDetails() {
        this.closeUserDetailsEvent.emit();
    }
    constructor() {
        effect(() => {
            console.log(this.ownerUser());
            console.log(this.memberUsers());
            console.log(this.guestUsers());
        });
    }
}
