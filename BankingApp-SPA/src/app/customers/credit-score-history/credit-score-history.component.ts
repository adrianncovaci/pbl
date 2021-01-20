import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { CreditScoreData } from '../../_models/credit-score-data';
import { Router, ActivatedRoute } from '@angular/router';
import { Chart } from 'chart.js';
import { AlertifyService } from '../../_services/alertify.service';
import { User } from '../../_models/user';

@Component({
  selector: 'app-credit-score-history',
  templateUrl: './credit-score-history.component.html',
  styleUrls: ['./credit-score-history.component.css'],
})
export class CreditScoreHistoryComponent implements OnInit {
  @Input() id: number;
  data: CreditScoreData[];
  scores: number[] = [];
  dates: Date[] = [];

  linechart;
  userId: number;
  currentUser: User;
  constructor(
    private service: AuthService,
    private route: ActivatedRoute,
    private alertify: AlertifyService
  ) {
    this.route.params.subscribe((params) => {
      this.userId = +params['id'];
    });
  }

  ngOnInit(): void {
    this.service.getCreditScoreHistory(this.id).subscribe((data) => {
      data.forEach((el) => {
        this.scores.push(el.creditScore);
        this.dates.push(el.dateIssued);
      });
      this.initialize_chart();
      console.log(this.linechart);
      this.linechart.labels = this.dates;
      this.linechart.datasets.foreach((el) => {
        el.data = this.scores;
      });
    });
  }

  initialize_chart() {
    this.linechart = new Chart('canvas', {
      type: 'line',
      data: {
        labels: this.dates,
        datasets: [
          {
            label: 'Credit Score',
            data: this.scores,
            borderColor: '#7b48d5',
          },
        ],
      },
      options: {
        responsive: false,
        legend: {
          display: true,
        },
        scales: {
          xAxes: [
            {
              display: false,
            },
          ],
          yAxes: [
            {
              display: true,
            },
          ],
        },
      },
    });
  }
}
