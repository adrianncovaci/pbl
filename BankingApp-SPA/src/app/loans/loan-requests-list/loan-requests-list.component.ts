import { Component, OnInit } from '@angular/core';
import { LoanService } from '../../_services/loan.service';
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { LoanRequest } from '../../_models/loan-request';

@Component({
  selector: 'app-loan-requests-list',
  templateUrl: './loan-requests-list.component.html',
  styleUrls: ['./loan-requests-list.component.css'],
})
export class LoanRequestsListComponent implements OnInit {
  loanRequests: LoanRequest[];

  constructor(
    private loanService: LoanService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe(
      (data) => (this.loanRequests = data['loanRequests'])
    );
  }
}
