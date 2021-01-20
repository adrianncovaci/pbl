import { Component, OnInit, Input } from '@angular/core';
import { Bankaccount } from '../../_models/bankaccount';

@Component({
  selector: 'app-bankaccount-card',
  templateUrl: './bankaccount-card.component.html',
  styleUrls: ['./bankaccount-card.component.css']
})
export class BankaccountCardComponent implements OnInit {
  @Input() account: Bankaccount;
  constructor() { }

  ngOnInit(): void {
  }

}
