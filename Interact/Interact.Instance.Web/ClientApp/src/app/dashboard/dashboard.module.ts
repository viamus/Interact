import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Routing } from './dashboard.routing';
import { HomeComponent } from './home/home.component';

import { AuthGuard } from '../auth.guard';
import { DashboardComponent } from './dashboard.component';
import { TestComponent } from './test/test.component';
import { AppModule } from '../app.module';
import { MatToolbarModule, MatMenuModule, MatGridListModule, MatIconModule, MatCardModule, MatButtonModule, MatSidenavModule, MatNavList, MatListModule, MatTooltipModule } from '@angular/material';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgProgressModule } from 'ngx-progressbar';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatCardModule,
    Routing,
    MatCardModule,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatSidenavModule,
    MatListModule,
    MatTooltipModule
  ],
  declarations: [
    HomeComponent,
    DashboardComponent,
    TestComponent,
    NavbarComponent,
  ],
  providers: [AuthGuard],
  bootstrap: [DashboardComponent]
})
export class DashboardModule { }
