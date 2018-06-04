import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './dashboard/home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './auth.guard';
import { TestComponent } from './dashboard/test/test.component';

export const Routing: Routes = [
  //{ path: 'dashboard-new', component: DashboardNewComponent },
  //{
  //  path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard],
  //  children: [
  //    { path: '', component: HomeComponent },
  //    { path: 'home', component: HomeComponent },
  //    { path: 'test', component: TestComponent }
  //  ]
  //}
  //{ path: '', component: HomeComponent },
  //{ path: 'home', component: HomeComponent },
  //{ path: 'page1', component: Page1Component },
  //{ path: 'page2', component: Page2Component }
];

//export const Routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
