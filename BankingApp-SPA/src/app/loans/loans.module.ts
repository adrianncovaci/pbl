import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoanApplyComponent } from './loan-apply/loan-apply.component';
import { LoanRequestsListComponent } from './loan-requests-list/loan-requests-list.component';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  declarations: [LoanApplyComponent, LoanRequestsListComponent],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    AppRoutingModule,
  ],
  exports: [LoanApplyComponent, LoanRequestsListComponent],
})
export class LoansModule {}
