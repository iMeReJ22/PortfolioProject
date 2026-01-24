import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { selectUsersError, selectUsersStatus } from '../../../core/state/users/users.selector';
import { UsersActions } from '../../../core/state/users/users.actions';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-login',
    imports: [ReactiveFormsModule, CommonModule],
    templateUrl: './login.html',
    styleUrl: './login.scss',
})
export class Login {
    private fb = inject(FormBuilder);
    private store = inject(Store);

    status = this.store.selectSignal(selectUsersStatus);
    errorMessage = this.store.selectSignal(selectUsersError);

    loginForm = this.fb.group({
        email: ['', [Validators.required, Validators.email, Validators.maxLength(256)]],
        password: ['', [Validators.required, Validators.minLength(8)]],
    });

    onSubmit() {
        if (this.loginForm.valid) {
            this.store.dispatch(
                UsersActions.login({
                    login: {
                        email: this.loginForm.getRawValue().email!,
                        password: this.loginForm.getRawValue().password!,
                    },
                }),
            );
        }
    }
}
