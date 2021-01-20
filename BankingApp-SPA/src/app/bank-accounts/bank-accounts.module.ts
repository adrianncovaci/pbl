import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BankaccountCardComponent } from './bankaccount-card/bankaccount-card.component';
import { BankaccountCreateComponent } from './bankaccount-create/bankaccount-create.component';
import { BankaccountDetailComponent } from './bankaccount-detail/bankaccount-detail.component';
import { BankaccountListComponent } from './bankaccount-list/bankaccount-list.component';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { TransferComponent } from './transfer/transfer.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';

@NgModule({
  declarations: [
    BankaccountCardComponent,
    BankaccountCreateComponent,
    BankaccountDetailComponent,
    BankaccountListComponent,
    TransactionListComponent,
    TransferComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    AppRoutingModule,
  ],
  providers: [],
  exports: [
    BankaccountCardComponent,
    BankaccountCreateComponent,
    BankaccountDetailComponent,
    BankaccountListComponent,
    TransactionListComponent,
    TransferComponent,
  ],
})
export class BankAccountsModule {}
