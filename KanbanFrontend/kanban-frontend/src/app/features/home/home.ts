import { Component, inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn } from '../../core/state/users/users.selector';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-home',
    imports: [RouterLink],
    templateUrl: './home.html',
    styleUrl: './home.scss',
})
export class Home {
    private store = inject(Store);

    isLoggedIn = this.store.selectSignal(selectIsLoggedIn);
}
