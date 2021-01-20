import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { LoanRequest } from 'src/app/_models/loan-request';
import { LoanType } from 'src/app/_models/loan-type';
import { LoanService } from 'src/app/_services/loan.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { TableColumn } from 'src/app/_models/pagination/table-column';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { PaginatedRequest } from '../../_models/pagination/paginated-request';
import { PagedResult } from '../../_models/pagination/paged-result';
import { RequestFilters } from 'src/app/_models/pagination/request-filters';
import { merge } from 'rxjs';
import { Filter } from 'src/app/_models/pagination/filter';
import { FilterOperators } from 'src/app/_models/pagination/filter-operators';

@Component({
  selector: 'app-officer-panel',
  templateUrl: './officer-panel.component.html',
  styleUrls: ['./officer-panel.component.css'],
})
export class OfficerPanelComponent implements AfterViewInit {
  pagedLoans: PagedResult<LoanRequest>;

  tableColumns: TableColumn[] = [
    {
      name: 'loanName',
      displayName: 'Loan',
      index: 'loan.loantype.type',
      useInSearch: true,
    },
    {
      name: 'customerName',
      displayName: 'Customer CNP',
      index: 'customer.cnp',
      useInSearch: true,
    },
    {
      name: 'dateIssued',
      displayName: 'Date Issued',
      index: 'dateIssued',
      useInSearch: false,
    },
    {
      name: 'status',
      displayName: 'Status',
      index: 'status',
      useInSearch: false,
    },
    {
      name: 'comments',
      displayName: 'Comments',
      index: 'comments',
      useInSearch: false,
    },
    { name: 'id', displayName: 'Actions', index: 'id', useInSearch: false },
  ];
  displayedColumns: string[];
  loanRequests: LoanRequest[];
  loanTypes: LoanType[];
  requestFilters: RequestFilters[] = [];
  searchInput = new FormControl('');
  filterForm: FormGroup;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private activeRouter: ActivatedRoute,
    private loanService: LoanService,
    private alertify: AlertifyService,
    private fb: FormBuilder
  ) {
    this.displayedColumns = this.tableColumns.map((el) => el.name);
    this.filterForm = this.fb.group({
      loanName: [''],
      customerName: [''],
    });
  }

  ngAfterViewInit(): void {
    this.loadLoans();
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page).subscribe(() => {
      this.loadLoans();
    });
  }

  filterFromForm() {
    this.requestFilters = [];
    this.createFilterFromForm();
    this.loadLoans();
  }
  loadLoans() {
    const request = new PaginatedRequest(
      this.paginator,
      this.requestFilters,
      this.sort
    );
    this.loanService
      .getAllLoanRequests(request)
      .subscribe((loans: PagedResult<LoanRequest>) => {
        this.pagedLoans = loans;
      });
  }

  createFilterFromForm() {
    if (this.filterForm.value) {
      console.log(this.filterForm.value);
      const filters: Filter[] = [];
      Object.keys(this.filterForm.controls).forEach((key) => {
        const controlValue = this.filterForm.controls[key].value;
        if (controlValue) {
          const foundTableColumn = this.tableColumns.find(
            (tableColumn) => tableColumn.name === key
          );
          const filter: Filter = {
            path: foundTableColumn.index,
            value: controlValue,
          };
          filters.push(filter);
        }
      });

      this.requestFilters.push({
        logicalOperator: FilterOperators.And,
        filters,
      });
    }
  }

  resetGrid() {
    this.requestFilters = [
      { filters: [], logicalOperator: FilterOperators.And },
    ];
    this.loadLoans();
  }

  acceptLoanRequest(id: number, loanId: number, cnp: string) {
    let model = { loanId: id, customerCnp: cnp };
    this.loanService.acceptLoanRequest(loanId, model).subscribe(
      (next) => {
        this.alertify.success('Successfully accepted');
      },
      (error) => {
        this.alertify.error('Something went wrong');
      }
    );
  }

  rejectLoanRequest(id: number) {
    this.loanService.rejectLoanRequest(id).subscribe(
      (next) => {
        this.alertify.success('Successfully rejected');
      },
      (error) => {
        this.alertify.error('Something went wrong');
      }
    );
  }
}
