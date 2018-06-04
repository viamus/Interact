import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AccountModule } from './account/account.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { AppComponent } from './app.component';
import { Routing } from './app.routing';
import { ConfigService } from './shared/utils/config.service';
import { UserService } from './shared/services/user.service';
import { NgProgressModule, NgProgressInterceptor } from 'ngx-progressbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule, MatGridListModule, MatCardModule, MatMenuModule, MatIconModule, MatButtonModule, MatSidenavModule, MatListModule } from '@angular/material';
import { AlertService } from './shared/services/alert.service';
import { AuthGuard } from './auth.guard';
import { LayoutModule } from '@angular/cdk/layout';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AccountModule,
    DashboardModule,
    NgProgressModule,
    RouterModule.forRoot(Routing),
    BrowserAnimationsModule,
    NoopAnimationsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    LayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
  ],
  exports: [
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: NgProgressInterceptor,
      multi: true
    },
    UserService,
    ConfigService,
    AlertService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
