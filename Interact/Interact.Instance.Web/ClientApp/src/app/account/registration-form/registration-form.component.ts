import { Component, OnInit } from '@angular/core';
import { UserRegistration } from '../../shared/models/user.registration.interface';
import { UserService } from '../../shared/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(
    private userService: UserService,
    private router: Router) { }

  ngOnInit() {
  }

  registerUser({ model, valid }: { model: UserRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';

    if (valid) {
      this.userService.register(model.userName, model.passwordHash, model.email)
        .finally(() => this.isRequesting = false)
        .subscribe(result => {
          if (result) {
            this.router.navigate(['/login'], { queryParams: { brandNew: true, userName: model.userName } });
          }
        },
        errors => this.errors = errors);
    }
  }

}
