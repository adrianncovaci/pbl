import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Bankaccount } from '../_models/bankaccount';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { LoanService } from '../_services/loan.service';
import { LoanRequest } from '../_models/loan-request';

@Injectable()
export class LoanRequestsResolver implements Resolve<LoanRequest[]> {
    constructor(private loanRequest: LoanService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<LoanRequest[]> {
        return this.loanRequest.getLoanRequestsByCurrentUser().pipe(
            catchError( error => {
                this.alertify.error("Problem retrieving data");
                this.router.navigate(['home']);
                return of(null);
            })
        );
    }

}