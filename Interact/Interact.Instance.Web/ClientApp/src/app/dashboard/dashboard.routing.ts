import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';

import { AuthGuard } from '../auth.guard';
import { DashboardComponent } from './dashboard.component';
import { TestComponent } from './test/test.component';

export const Routing: ModuleWithProviders = RouterModule.forChild([
  {
    path: '', component: DashboardComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'test', component: TestComponent }
    ]
  },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'test', component: TestComponent }
    ]
  }
]);

//export const Routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
