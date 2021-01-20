import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Observable } from 'rxjs';
import { CreditScoreData } from '../_models/credit-score-data';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl + 'customer/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'authenticate', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  roleMatch(acceptedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    acceptedRoles.forEach((element) => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logOut() {
    localStorage.removeItem('token');
  }

  getCustomer(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + id);
  }

  getLoggedInCustomerId(): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'loggedin');
  }

  getCreditScoreHistory(id: number): Observable<CreditScoreData[]> {
    return this.http.get<CreditScoreData[]>(
      this.baseUrl + 'scorehistory/' + id
    );
  }

  changePasswordLoggedInUser(model: any) {
    return this.http.post(this.baseUrl + 'changepassword', model);
  }
}
