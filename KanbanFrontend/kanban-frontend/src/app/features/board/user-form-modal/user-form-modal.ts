import { Component } from '@angular/core';
import { ɵInternalFormsSharedModule } from '@angular/forms';

@Component({
    selector: 'app-user-form-modal',
    imports: [ɵInternalFormsSharedModule],
    templateUrl: './user-form-modal.html',
    styleUrl: './user-form-modal.scss',
})
export class UserFormModal {}
