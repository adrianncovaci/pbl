import { Injectable } from '@angular/core';
import { AlertifyService } from './alertify.service';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient ,private userService: UserService, private alertify: AlertifyService) { }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'customer/' + id);
  }
}
