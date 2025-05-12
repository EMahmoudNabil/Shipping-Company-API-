// src/app/app.config.ts
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
// import { AuthInterceptor } from './Interceptors/auth.interceptor';
// import { loadingInterceptor } from './Interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),

    provideHttpClient(
      withInterceptors([
        AuthInterceptor,
        // loadingInterceptor
      ])
    ),
    // ✅ Uncomment this to fix HttpClient error
  

    // ✅ Later you can enable interceptors like this:
    // provideHttpClient(withInterceptors([AuthInterceptor, loadingInterceptor])),

    provideAnimations(),
    provideToastr({
      maxOpened: 3,
      positionClass: 'toast-bottom-left',
      progressBar: true,
      progressAnimation: 'increasing',
    }),
  ],
};
