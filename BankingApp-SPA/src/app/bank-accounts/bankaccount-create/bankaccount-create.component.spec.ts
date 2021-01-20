import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankaccountCreateComponent } from './bankaccount-create.component';

describe('BankaccountCreateComponent', () => {
  let component: BankaccountCreateComponent;
  let fixture: ComponentFixture<BankaccountCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankaccountCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankaccountCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
