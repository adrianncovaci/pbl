import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../_models/transaction';
import { ExchangeRate } from '../_models/exchangerate';
import { createViewChildren } from '@angular/compiler/src/core';
import { PaginatedRequest } from '../_models/pagination/paginated-request';
import { PagedResult } from '../_models/pagination/paged-result';

@Injectable({
  providedIn: 'root',
})
export class TransferService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getExchangeRate(): Observable<ExchangeRate> {
    return this.http.get<ExchangeRate>(this.baseUrl + 'bankaccount/rates');
  }

  withdraw(model: any) {
    return this.http.post(this.baseUrl + 'bankaccount/withdraw', model);
  }

  deposit(model: any) {
    return this.http.post(this.baseUrl + 'bankaccount/deposit', model);
  }

  transfer(model: any) {
    return this.http.post(this.baseUrl + 'bankaccount/transfer', model);
  }

  getTransactionsByAccount(
    id: number,
    pagedRequest: PaginatedRequest
  ): Observable<PagedResult<Transaction>> {
    return this.http.post<PagedResult<Transaction>>(
      this.baseUrl + 'transaction/transactions/' + id,
      pagedRequest
    );
  }
}
