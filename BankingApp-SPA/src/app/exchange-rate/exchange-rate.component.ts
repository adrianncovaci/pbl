import { Component, OnInit, ViewChild } from '@angular/core';
import { ExchangeRate } from '../_models/exchangerate';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { TransferService } from '../_services/transfer.service';
import { TableColumn } from 'src/app/_models/pagination/table-column';
import { MatTable } from '@angular/material/table';

export interface Currency {
  position: number;
  currency: string;
  value: number;
}

@Component({
  selector: 'app-exchange-rate',
  templateUrl: './exchange-rate.component.html',
  styleUrls: ['./exchange-rate.component.css']
})
export class ExchangeRateComponent implements OnInit {
  @ViewChild(MatTable) table: MatTable<any>;
  currencies: Currency[] = [];
  collumns: string[] = ["position", "currency", "value"];
  exchangeRate: ExchangeRate = null;
  val: number;
  loading = true;
  constructor(private transferService: TransferService, private alertify: AlertifyService, private route: ActivatedRoute) { 
  }

  ngOnInit() {
    this.getExchangeRate();
  }

  async getExchangeRate() {
    this.exchangeRate = await this.transferService.getExchangeRate().toPromise();
    let index = 1;
    for (const key in this.exchangeRate.rates) {
      this.currencies.push( { position: index, currency: key, value: this.exchangeRate.rates[key] } )
      index += 1;
    }
    this.table.renderRows();
    this.loading = false;
  }
}
