import { Component, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn, selectLoggedData } from '../../core/state/users/users.selector';
import { UsersActions } from '../../core/state/users/users.actions';

@Component({
    selector: 'app-sidebar',
    imports: [RouterLink, RouterLinkActive],
    templateUrl: './sidebar.html',
    styleUrl: './sidebar.scss',
})
export class Sidebar {
    private store = inject(Store);
    user = this.store.selectSignal(selectLoggedData);

    isLoggedIn = this.store.selectSignal(selectIsLoggedIn);
    sidebarVisibility = signal(true);

    onLogout() {
        this.store.dispatch(UsersActions.logout());
    }

    hideSidebar() {
        this.sidebarVisibility.set(false);
    }
    showSidebar() {
        this.sidebarVisibility.set(true);
    }
}
