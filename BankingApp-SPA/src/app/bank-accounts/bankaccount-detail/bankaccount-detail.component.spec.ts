import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankaccountDetailComponent } from './bankaccount-detail.component';

describe('BankaccountDetailComponent', () => {
  let component: BankaccountDetailComponent;
  let fixture: ComponentFixture<BankaccountDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankaccountDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankaccountDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
