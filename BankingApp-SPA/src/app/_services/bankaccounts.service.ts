import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bankaccount } from '../_models/bankaccount';
import { BankAccountTypes } from '../_models/bankaccounttypes';
import { User } from '../_models/user';
import { BankAccountsModule } from '../bank-accounts/bank-accounts.module';

@Injectable({
  providedIn: 'root'
})
export class BankaccountsService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

    createBankAccount(model: any): Observable<Bankaccount> {
      return this.http.post<Bankaccount>(this.baseUrl + 'bankaccount', model);
    }

    getBankAccounts(): Observable<Bankaccount[]> {
      return this.http.get<Bankaccount[]>(this.baseUrl + 'bankaccount/all');
    }

    getBankAccount(id: number): Observable<Bankaccount> {
      return this.http.get<Bankaccount>(this.baseUrl + 'bankaccount/' + id);
    }

    getBankAccountsByLoggedInUser(): Observable<Bankaccount[]> {
      return this.http.get<Bankaccount[]>(this.baseUrl + "bankaccount");
    }

    getBankAccountTypes(): Observable<BankAccountTypes[]> {
      return this.http.get<BankAccountTypes[]>(this.baseUrl + 'bankaccount/types');
    }

    getUserById(id: number): Observable<User> {
      return this.http.get<User>(this.baseUrl + 'customer/' + id );
    } 
}
