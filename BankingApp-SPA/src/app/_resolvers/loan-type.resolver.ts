import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { LoanService } from '../_services/loan.service';
import { LoanType } from '../_models/loan-type';

@Injectable()
export class LoanTypeResolver implements Resolve<LoanType[]> {
  constructor(
    private loanRequest: LoanService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<LoanType[]> {
    return this.loanRequest.getAllLoanTypes().pipe(
      catchError((error) => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['home']);
        return of(null);
      })
    );
  }
}
