import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { BankaccountsService } from '../../_services/bankaccounts.service';
import { AlertifyService } from '../../_services/alertify.service';
import { BankAccountTypes } from '../../_models/bankaccounttypes';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-bankaccount-create',
  templateUrl: './bankaccount-create.component.html',
  styleUrls: ['./bankaccount-create.component.css']
})
export class BankaccountCreateComponent implements OnInit {

  accountTypes: BankAccountTypes[];

  createForm: FormGroup;
  model: any = {};
  constructor(private fb: FormBuilder, private service: BankaccountsService,
    private alertify: AlertifyService, private route: ActivatedRoute, private router: Router) { }
  

  ngOnInit(): void {
    this.createForm = this.fb.group ({
      accountType: ['', [
        Validators.required,
      ]],
      initialDeposit: ['', [
      ]]
    });

    this.route.data.subscribe(data => {
      this.accountTypes = data['bankAccountTypes'];
    })
  }

  get formValue() {
    return this.createForm.controls;
  }

  create() {
    this.model = {
      ...this.createForm.value
    };

    this.service.createBankAccount(this.model).subscribe(success => {
      this.alertify.success("Account Created Successfully!");
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['']);
    });
  }

}
