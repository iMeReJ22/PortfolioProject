import { ApplicationConfig, provideBrowserGlobalErrorListeners, isDevMode } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './core/interceptors/error-interceptor';
import { provideEffects } from '@ngrx/effects';
import { authInterceptor } from './core/interceptors/auth-interceptor';
import { taskReducer } from './core/state/tasks/tasks.reducer';
import { boardReducer } from './core/state/boards/boards.reducer';
import { columnReducer } from './core/state/columns/columns.reducer';
import { commentReducer } from './core/state/comments/comments.reducer';
import { tagReducer } from './core/state/tags/tags.reducer';
import { userReducer } from './core/state/users/users.reducer';
import { logReducer } from './core/state/activity-log/activity-log.reducer';
import { TasksEffects } from './core/state/tasks/tasks.effects';
import { ActivityLogEffects } from './core/state/activity-log/activity-log.effects';
import { BoardsEffects } from './core/state/boards/boards.effects';
import { ColumnsEffects } from './core/state/columns/columns.effects';
import { CommentsEffects } from './core/state/comments/comments.effects';
import { TagsEffects } from './core/state/tags/tags.effects';
import { UsersEffects } from './core/state/users/users.effects';

export const appConfig: ApplicationConfig = {
    providers: [
        provideHttpClient(withInterceptors([errorInterceptor, authInterceptor])),
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes, withComponentInputBinding()),
        provideStore({
            tasks: taskReducer,
            logs: logReducer,
            boards: boardReducer,
            columns: columnReducer,
            comments: commentReducer,
            tags: tagReducer,
            users: userReducer,
        }),
        provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
        provideEffects([
            TasksEffects,
            ActivityLogEffects,
            BoardsEffects,
            ColumnsEffects,
            CommentsEffects,
            TagsEffects,
            UsersEffects,
        ]),
    ],
};

export const environment = {
    production: false,
    apiUrl: 'https://localhost:7057/api',
};
