import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankaccountCardComponent } from './bankaccount-card.component';

describe('BankaccountCardComponent', () => {
  let component: BankaccountCardComponent;
  let fixture: ComponentFixture<BankaccountCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankaccountCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankaccountCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
