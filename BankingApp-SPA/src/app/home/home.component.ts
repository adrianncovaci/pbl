import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  currentUser: User;
  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.authService.getLoggedInCustomerId().subscribe(
      (usr) => {
        this.currentUser = usr;
      },
      (error) => {
        // this.alertify.error(error);
        this.router.navigate(['home']);
      }
    );
  }
}
