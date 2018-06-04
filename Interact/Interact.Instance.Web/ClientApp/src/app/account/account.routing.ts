import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { AccountGuard } from './account.guard';

export const Routing: ModuleWithProviders = RouterModule.forChild([
  { path: 'register', component: RegistrationFormComponent, canActivate: [AccountGuard] },
  { path: 'login', component: LoginFormComponent, canActivate: [AccountGuard] },
]);
