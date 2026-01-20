import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    return next(req).pipe(
        catchError((error) => {
            if (error.status === 404) {
                console.error('Resource not found:', error);
            } else if (error.status === 500) {
                console.error('Internal server error:', error);
            }

            return throwError(() => error);
        }),
    );
};
