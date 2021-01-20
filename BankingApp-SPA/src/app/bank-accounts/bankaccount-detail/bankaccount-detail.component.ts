import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Bankaccount } from '../../_models/bankaccount';
import { BankaccountsService } from '../../_services/bankaccounts.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { TransferService } from '../../_services/transfer.service';
import { Transaction } from '../../_models/transaction';
import { PagedResult } from 'src/app/_models/pagination/paged-result';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { RequestFilters } from 'src/app/_models/pagination/request-filters';
import { PaginatedRequest } from 'src/app/_models/pagination/paginated-request';
import { merge } from 'rxjs';
import { Filter } from 'src/app/_models/pagination/filter';
import { FilterOperators } from 'src/app/_models/pagination/filter-operators';

@Component({
  selector: 'app-bankaccount-detail',
  templateUrl: './bankaccount-detail.component.html',
  styleUrls: ['./bankaccount-detail.component.css'],
})
export class BankaccountDetailComponent implements OnInit, AfterViewInit {
  model: any;
  depositForm: FormGroup;
  withdrawForm: FormGroup;
  transferForm: FormGroup;
  dateForm: FormGroup;
  startDate = null;
  endDate = null;

  bankAccount: Bankaccount;
  transactions: PagedResult<Transaction>;
  displayedColumns: string[] = [
    'dateIssued',
    'amount',
    'transactionType',
    'message',
    'receiver',
  ];
  filterProperties = { deposit: true, withdrawal: true, transfer: true };

  requestFilters: RequestFilters[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private bankAcc: BankaccountsService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private transferService: TransferService,
    private fbDeposit: FormBuilder,
    private fbWithdraw: FormBuilder,
    private fbTransfer: FormBuilder,
    private router: Router,
    private fbDate: FormBuilder
  ) {
    this.depositForm = this.fbDeposit.group({
      amount: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.pattern('^[0-9]*$'),
        ],
      ],
    });
    this.withdrawForm = this.fbWithdraw.group({
      amount: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.pattern('^[0-9]*$'),
        ],
      ],
    });
    this.transferForm = this.fbTransfer.group({
      amount: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.pattern('^[0-9]*$'),
        ],
      ],
      receiver: ['', [Validators.required]],
      message: ['', [Validators.maxLength(256)]],
    });
    this.dateForm = this.fbDate.group({
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.loadBankAccount();
  }

  ngAfterViewInit(): void {
    this.getTransactionsByAccount();
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page).subscribe(() => {
      this.getTransactionsByAccount();
    });
  }

  get transferFormValue() {
    return this.transferForm.controls;
  }

  get depositFormValue() {
    return this.depositForm.controls;
  }

  get withdrawFormValue() {
    return this.withdrawForm.controls;
  }

  get dateFilterFormValue() {
    return this.dateForm.controls;
  }

  loadBankAccount() {
    this.bankAcc.getBankAccount(+this.route.snapshot.params['id']).subscribe(
      (acc) => {
        this.bankAccount = acc;
        if (this.bankAccount.period) {
          this.bankAccount.monthlyPayment =
            (this.bankAccount.balance / this.bankAccount.period) *
            (1 + this.bankAccount.interestRate);
        }
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  getTransactionsByAccount() {
    const request = new PaginatedRequest(
      this.paginator,
      this.requestFilters,
      this.sort
    );
    this.transferService
      .getTransactionsByAccount(+this.route.snapshot.params['id'], request)
      .subscribe(
        (transactions: PagedResult<Transaction>) => {
          this.transactions = transactions;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  deposit() {
    this.model = {
      senderAccountId: this.bankAccount.id,
      amount: this.depositForm.get('amount').value,
    };

    this.transferService.deposit(this.model).subscribe(
      (next) => {
        this.alertify.success('Successfully deposited');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['']);
      }
    );
  }

  withdraw() {
    this.model = {
      senderAccountId: this.bankAccount.id,
      amount: this.withdrawForm.get('amount').value,
    };

    this.transferService.withdraw(this.model).subscribe(
      (next) => {
        this.alertify.success('Successfully withdrawn!');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['']);
      }
    );
  }

  transfer() {
    this.model = {
      senderAccountId: this.bankAccount.id,
      amount: this.transferForm.get('amount').value,
      receiverAccountName: this.transferForm.get('receiver').value,
      message: this.transferForm.get('message').value,
    };

    this.transferService.transfer(this.model).subscribe(
      (next) => {
        this.alertify.success('Successfully transfered');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['']);
      }
    );
  }
  applyFilters() {
    let arr: Filter[] = [];
    this.requestFilters = [];
    Object.keys(this.filterProperties).forEach((element) => {
      if (this.filterProperties[element] === true) {
        const _filter: Filter = {
          path: 'transactiontype',
          value: element,
        };
        arr.push(_filter);
      }
    });
    console.log(arr);
    let startDate = null;
    let endDate = null;
    if (this.dateForm.get('startDate').value != '') {
      startDate = this.dateForm.get('startDate').value;
    }
    if (this.dateForm.get('endDate').value != '') {
      endDate = this.dateForm.get('endDate').value;
    }
    let newReqFilter: RequestFilters = {
      logicalOperator: FilterOperators.Or,
      filters: arr,
      startDate: startDate,
      endDate: endDate,
    };
    this.requestFilters.push(newReqFilter);
    this.getTransactionsByAccount();
  }
}
