import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Sidebar } from './features/sidebar/sidebar';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn } from './core/state/users/users.selector';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, Sidebar],
    templateUrl: './app.html',
    styleUrl: './app.scss',
})
export class App {
    protected readonly title = signal('kanban-frontend');
    private store = inject(Store);

    isLoggedIn = this.store.selectSignal(selectIsLoggedIn);
}
