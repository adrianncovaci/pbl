import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { LoanService } from '../../_services/loan.service';
import { AlertifyService } from '../../_services/alertify.service';
import { LoanType } from '../../_models/loan-type';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PagedResult } from '../../_models/pagination/paged-result';
import { RequestFilters } from '../../_models/pagination/request-filters';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PaginatedRequest } from '../../_models/pagination/paginated-request';
import { merge } from 'rxjs';
import { Filter } from '../../_models/pagination/filter';
import { FilterOperators } from '../../_models/pagination/filter-operators';

@Component({
  selector: 'app-loan-apply',
  templateUrl: './loan-apply.component.html',
  styleUrls: ['./loan-apply.component.css'],
})
export class LoanApplyComponent implements OnInit, AfterViewInit {
  loanTypes: PagedResult<LoanType>;
  message: Boolean[] = [];
  applyForm: FormGroup;
  model: any;
  arr = [];
  index: number;

  filterForms = { auto: true, mortgage: true, student: true };

  requestFilters: RequestFilters[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private loanService: LoanService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private route: Router
  ) {
    this.applyForm = this.fb.group({
      comments: ['', [Validators.maxLength(256), Validators.required]],
    });
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.getLoanTypes();
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page).subscribe(() => {
      this.getLoanTypes();
    });
  }

  get formValue() {
    return this.applyForm.controls;
  }

  apply() {
    this.model = {
      ...this.applyForm.value,
    };
    console.log(this.loanTypes);
    for (var i = 0; i < this.message.length; i++)
      if (this.message[i] === true)
        this.model.loanId = this.loanTypes.data[i].id;

    this.loanService.requestLoan(this.model).subscribe(
      () => {
        this.alertify.success('Successfully requested');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.route.navigate(['']);
      }
    );
  }

  getLoanTypes() {
    const request = new PaginatedRequest(
      this.paginator,
      this.requestFilters,
      this.sort
    );
    this.loanService.getLoanTypes(request).subscribe(
      (loans: PagedResult<LoanType>) => {
        this.loanTypes = loans;
        this.loanTypes.data.forEach((_) => {
          this.message.push(false);
        });
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  toggleMessage(id: number) {
    for (var i = 0; i < this.message.length; i++) this.message[i] = false;
    this.message[id] = true !== this.message[id];
  }

  applyFilters() {
    this.requestFilters = [];
    let arr: Filter[] = [];
    Object.keys(this.filterForms).forEach((element) => {
      if (this.filterForms[element] === true) {
        const _filter: Filter = {
          path: 'loantype.type',
          value: element,
        };
        arr.push(_filter);
      }
    });
    this.requestFilters.push({
      logicalOperator: FilterOperators.Or,
      filters: arr,
    });
    this.getLoanTypes();
  }
}
