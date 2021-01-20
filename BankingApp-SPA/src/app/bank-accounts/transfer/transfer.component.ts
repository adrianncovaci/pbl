import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { TransferService } from '../../_services/transfer.service';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.css'],
})
export class TransferComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private alertify: AlertifyService,
    private router: Router,
    private transfer_service: TransferService
  ) {}

  ngOnInit(): void {}
}
