import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../shared/services/user.service';
import { Routing } from './account.routing';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { AccountGuard } from './account.guard';
import { MatFormFieldModule, MatInputModule, MatCardModule } from '@angular/material';
import { AlertComponent } from '../alert/alert.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    Routing,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    FormsModule
  ],
  declarations: [
    RegistrationFormComponent,
    LoginFormComponent,
    AlertComponent
  ],
  providers: [
    AccountGuard
  ]
})
export class AccountModule { }
