import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { CreditScoreHistoryComponent } from './credit-score-history/credit-score-history.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UserDetailComponent,
    CreditScoreHistoryComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    AppRoutingModule,
  ],
  exports: [
    LoginComponent,
    RegisterComponent,
    UserDetailComponent,
    CreditScoreHistoryComponent,
  ],
})
export class CustomersModule {}
