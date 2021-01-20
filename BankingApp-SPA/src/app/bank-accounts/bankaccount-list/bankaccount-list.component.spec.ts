import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankaccountListComponent } from './bankaccount-list.component';

describe('BankaccountListComponent', () => {
  let component: BankaccountListComponent;
  let fixture: ComponentFixture<BankaccountListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankaccountListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankaccountListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
