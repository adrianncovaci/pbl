import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Bankaccount } from '../_models/bankaccount';
import { BankaccountsService } from '../_services/bankaccounts.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { BankAccountTypes } from '../_models/bankaccounttypes';

@Injectable()
export class AccountTypeResolver implements Resolve<BankAccountTypes[]> {
    constructor(private bankAccountService: BankaccountsService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<BankAccountTypes[]> {
        return this.bankAccountService.getBankAccountTypes().pipe(
            catchError( error => {
                this.alertify.error("Problem retrieving data");
                this.router.navigate(['home']);
                return of(null);
            })
        );
    }

}