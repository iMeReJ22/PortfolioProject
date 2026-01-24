import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: 'board',
        component: BoardComponent,
        canActivate: [authGuard],
    },
    { path: 'login', component: LoginComponent },
];
