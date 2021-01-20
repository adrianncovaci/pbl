import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanRequestsListComponent } from './loan-requests-list.component';

describe('LoanRequestsListComponent', () => {
  let component: LoanRequestsListComponent;
  let fixture: ComponentFixture<LoanRequestsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoanRequestsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanRequestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
