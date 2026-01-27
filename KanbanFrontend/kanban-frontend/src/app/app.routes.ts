import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { Login } from './features/home/login/login';
import { Register } from './features/home/register/register';
import { Dashboard } from './features/dashboard/dashboard';
import { Home } from './features/home/home';
import { Board } from './features/board/board';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: 'home',
        component: Home,
    },
    {
        path: 'login',
        component: Login,
    },
    {
        path: 'register',
        component: Register,
    },
    {
        path: 'dashboard',
        component: Dashboard,
        canActivate: [authGuard],
    },
    {
        path: 'board/:id',
        component: Board,
        canActivate: [authGuard],
    },
];
