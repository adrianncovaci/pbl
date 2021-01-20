import { Component, OnInit, AfterViewInit } from '@angular/core';
import { User } from '../../_models/user';
import { AuthService } from '../../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

export interface ChangePassword {
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css'],
})
export class UserDetailComponent implements OnInit {
  user: User;
  userId: number;
  subscribeId;
  model: ChangePassword;
  changePasswordForm: FormGroup;
  constructor(
    private _service: AuthService,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private formBuilder: FormBuilder,
    public authService: AuthService
  ) {
    this.subscribeId = this.route.params.subscribe((params) => {
      this.userId = +params['id'];
    });
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
    });
  }

  get formValues() {
    return this.changePasswordForm.controls;
  }

  ngOnInit(): void {
    this._service.getCustomer(this.userId).subscribe(
      (usr) => {
        this.user = usr;
        console.log(this.user);
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    console.log('NAME: ', this.authService.decodedToken);
  }

  changePassword() {
    this.model = {
      ...this.changePasswordForm.value,
    };
    this.authService.changePasswordLoggedInUser(this.model).subscribe(
      () => {
        this.alertify.success('Successfully changed password');
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
