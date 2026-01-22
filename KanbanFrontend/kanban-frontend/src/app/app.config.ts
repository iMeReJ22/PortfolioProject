import { ApplicationConfig, provideBrowserGlobalErrorListeners, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './core/interceptors/error-interceptor';
import { provideEffects } from '@ngrx/effects';

export const appConfig: ApplicationConfig = {
    providers: [
    provideHttpClient(withInterceptors([errorInterceptor])),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideStore(),
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
    provideEffects()
],
};

export const environment = {
    production: false,
    apiUrl: 'https://localhost:7057/api',
};
