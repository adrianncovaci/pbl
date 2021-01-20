import { Component, OnInit } from '@angular/core';
import { Bankaccount } from '../../_models/bankaccount';
import { AlertifyService } from '../../_services/alertify.service';
import { BankaccountsService } from '../../_services/bankaccounts.service';
import { BankAccountTypes } from '../../_models/bankaccounttypes';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bankaccount-list',
  templateUrl: './bankaccount-list.component.html',
  styleUrls: ['./bankaccount-list.component.css']
})
export class BankaccountListComponent implements OnInit {

  bankAccountTypes: BankAccountTypes[];
  bankAccounts: Bankaccount[];
  accountMap = new Map<string, Bankaccount[]>();
  panelOpenState = false;

  constructor(private _bankAccountService: BankaccountsService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.bankAccounts = data['bankAccounts'];
      this.bankAccountTypes = data['bankAccountTypes'];
      this.bankAccountTypes.forEach(el => {
        let accounts = [];
        this.bankAccounts.forEach(bankAccount => {
          if (bankAccount.accountTypeId === el.id)
            accounts.push(bankAccount);
        })
        this.accountMap.set(el.accountType, accounts);
      })
    });
  }
}
