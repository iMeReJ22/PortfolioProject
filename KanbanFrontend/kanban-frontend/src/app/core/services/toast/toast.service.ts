import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({ providedIn: 'root' })
export class ToastService {
    success(title: string, text: string) {
        Swal.fire({
            title,
            text,
            icon: 'success',
            timer: 5000,
            toast: true,
            position: 'bottom',
        });
    }

    error(title: string, text: string) {
        Swal.fire({
            title,
            text,
            icon: 'error',
            position: 'center',
            confirmButtonColor: '#6c5ce7',
        });
    }

    show(message: string, type: 'success' | 'error' = 'success') {
        alert(`${type.toUpperCase()}: ${message}`);
    }
}
