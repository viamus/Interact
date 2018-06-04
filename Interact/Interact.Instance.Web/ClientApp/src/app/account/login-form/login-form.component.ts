import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { Subscription } from 'rxjs';
//import { HttpClient } from '@angular/common/http/';
import { HttpClientModule } from '@angular/common/http';
import { FormGroup } from '@angular/forms/src/model';
import { FormControl, Validators } from '@angular/forms';
import { AlertService } from '../../shared/services/alert.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  private model = {
    userName: '',
    passwordHash: ''
  };
  
  private loading = false;
  private returnUrl: string;

  private subscription: Subscription;
  private brandNew: boolean;
  private errors: string;
  private isRequesting: boolean;
  private submitted: boolean = false;

  constructor(
    private userService: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private alertService: AlertService
    //private http: HttpClientModule
  ) { }

  ngOnInit() {
    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => {
        this.brandNew = param['brandNew'];
        //this.credentials.userName = param['userName'];
      });
  }

  login() {
    this.loading = true;

    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (this.model.userName != '') {
      this.userService.login(this.model.userName, this.model.passwordHash)
        .subscribe(
        result => {
          if (result) {
            this.router.navigate(['/dashboard']);
          }
          else {
            this.alertService.error('Login failed.');
          }
        },
        error => {
          this.loading = false;
          this.errors = error;
        });
    }
  }
}
