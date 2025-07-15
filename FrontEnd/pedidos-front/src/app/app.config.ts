import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';

import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { PedidosComponent } from './pages/pedidos/pedidos.component';
import { provideHttpClient, withFetch } from '@angular/common/http';

const routes: Routes = [
  { path: '', component: PedidosComponent }
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes), provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch())
  ]
};
