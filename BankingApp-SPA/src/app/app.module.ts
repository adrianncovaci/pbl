import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './navbar/navbar.component';
import { MaterialModule } from './material/material.module';

import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { FooterComponent } from './footer/footer.component';
import { AuthService } from './_services/auth.service';
import { BankaccountListComponent } from './bank-accounts/bankaccount-list/bankaccount-list.component';
import { BankaccountCardComponent } from './bank-accounts/bankaccount-card/bankaccount-card.component';
import { HomeComponent } from './home/home.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AccountListResolver } from './_resolvers/account-list.resolver';
import { AccountTypeResolver } from './_resolvers/account-type.resolver';
import { RouterModule } from '@angular/router';
import { BankaccountCreateComponent } from './bank-accounts/bankaccount-create/bankaccount-create.component';
import { BankaccountDetailComponent } from './bank-accounts/bankaccount-detail/bankaccount-detail.component';
import { TransactionListComponent } from './bank-accounts/transaction-list/transaction-list.component';
import { LoanApplyComponent } from './loans/loan-apply/loan-apply.component';
import { LoanRequestsListComponent } from './loans/loan-requests-list/loan-requests-list.component';
import { LoanRequestsResolver } from './_resolvers/loan-requests.resolver';
import { ExchangeRateComponent } from './exchange-rate/exchange-rate.component';
import { HasRolesDirective } from './_directives/has-roles.directive';
import { LoanTypeResolver } from './_resolvers/loan-type.resolver';
import { HomepageComponent } from './homepage/homepage.component';
import { BankAccountsModule } from './bank-accounts/bank-accounts.module';
import { BankaccountsService } from './_services/bankaccounts.service';
import { LoanOfficersModule } from './loan-officers/loan-officers.module';
import { LoansModule } from './loans/loans.module';
import { CustomersModule } from './customers/customers.module';
import { ErrorInterceptor } from './_services/error.interceptor';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    ExchangeRateComponent,
    HasRolesDirective,
    HomepageComponent,
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
    BankAccountsModule,
    LoanOfficersModule,
    LoansModule,
    CustomersModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/customers'],
      },
    }),
  ],
  providers: [
    AuthService,
    BankaccountsService,
    AccountListResolver,
    AccountTypeResolver,
    LoanRequestsResolver,
    LoanTypeResolver,
    ErrorInterceptor,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
