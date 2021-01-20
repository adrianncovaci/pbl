import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './customers/login/login.component';
import { RegisterComponent } from './customers/register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { BankaccountListComponent } from './bank-accounts/bankaccount-list/bankaccount-list.component';
import { AccountListResolver } from './_resolvers/account-list.resolver';
import { AccountTypeResolver } from './_resolvers/account-type.resolver';
import { BankaccountCreateComponent } from './bank-accounts/bankaccount-create/bankaccount-create.component';
import { BankaccountDetailComponent } from './bank-accounts/bankaccount-detail/bankaccount-detail.component';
import { LoanApplyComponent } from './loans/loan-apply/loan-apply.component';
import { LoanRequestsListComponent } from './loans/loan-requests-list/loan-requests-list.component';
import { LoanRequestsResolver } from './_resolvers/loan-requests.resolver';
import { OfficerPanelComponent } from './loan-officers/officer-panel/officer-panel.component';
import { LoanTypeResolver } from './_resolvers/loan-type.resolver';
import { HomepageComponent } from './homepage/homepage.component';
import { UserDetailComponent } from './customers/user-detail/user-detail.component';

const routes: Routes = [
  { path: 'home', component: HomepageComponent },
  // { path: 'openaccount', component: BankaccountCreateComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: HomeComponent,
        resolve: {
          bankAccounts: AccountListResolver,
          bankAccountTypes: AccountTypeResolver,
        },
        data: { roles: ['Customer'] },
      },
      {
        path: 'openaccount',
        component: BankaccountCreateComponent,
        resolve: { bankAccountTypes: AccountTypeResolver },
        data: { roles: ['Customer'] },
      },
      {
        path: 'applyloan',
        component: LoanApplyComponent,
        data: { roles: ['Customer'] },
      },
      {
        path: 'loanrequests',
        component: LoanRequestsListComponent,
        resolve: { loanRequests: LoanRequestsResolver },
        data: { roles: ['Customer'] },
      },
      {
        path: 'officer',
        component: OfficerPanelComponent,
        data: { roles: ['Admin', 'LoanOfficer'] },
      },
      {
        path: 'profile/:id',
        component: UserDetailComponent,
        data: { roles: ['Customer', 'Admin', 'LoanOfficer'] },
      },
      {
        path: ':id',
        component: BankaccountDetailComponent,
        data: { roles: ['Customer'] },
      },
    ],
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
