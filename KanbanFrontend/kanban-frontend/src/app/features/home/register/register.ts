import { Component, inject, signal } from '@angular/core';
import {
    AbstractControl,
    NonNullableFormBuilder,
    ReactiveFormsModule,
    ValidationErrors,
    ValidatorFn,
    Validators,
} from '@angular/forms';
import { Store } from '@ngrx/store';
import { selectUsersStatus } from '../../../core/state/users/users.selector';
import { UsersActions } from '../../../core/state/users/users.actions';
import { CreateUserRequest } from '../../../core/models/Requests/user-requests.models';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-register',
    imports: [ReactiveFormsModule, RouterLink],
    templateUrl: './register.html',
    styleUrl: './register.scss',
})
export class Register {
    private fb = inject(NonNullableFormBuilder);
    private store = inject(Store);

    registerForm = this.fb.group(
        {
            username: ['', [Validators.required, Validators.minLength(3)]],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(8)]],
            password2: ['', [Validators.required, Validators.minLength(8)]],
        },
        {
            validators: passwordMatchValidator,
        },
    );

    status = this.store.selectSignal(selectUsersStatus);

    onSubmit() {
        if (this.registerForm.valid) {
            const request: CreateUserRequest = {
                email: this.registerForm.getRawValue().email,
                displayName: this.registerForm.getRawValue().username,
                password: this.registerForm.getRawValue().password,
            };
            this.store.dispatch(UsersActions.createUser({ create: request }));
        }
    }

    get f() {
        return this.registerForm.controls;
    }
}

export const passwordMatchValidator: ValidatorFn = (
    control: AbstractControl,
): ValidationErrors | null => {
    const pass = control.get('password');
    const confPass = control.get('password2');

    return pass && confPass && pass.value !== confPass.value ? { passwordMismatch: true } : null;
};
