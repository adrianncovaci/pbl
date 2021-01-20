import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  model: any = {};

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  login() {
    this.model = {
      ...this.loginForm.value,
    };

    this.authService.login(this.model).subscribe(
      (next) => {
        this.alertify.success('Logged in successfully!');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        const roles = this.authService.decodedToken.role as Array<string>;
        if (roles.includes('Customer')) {
          this.router.navigate(['']);
        } else {
          this.router.navigate(['/officer']);
        }
      }
    );
  }
}
