import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditScoreHistoryComponent } from './credit-score-history.component';

describe('CreditScoreHistoryComponent', () => {
  let component: CreditScoreHistoryComponent;
  let fixture: ComponentFixture<CreditScoreHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreditScoreHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreditScoreHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
