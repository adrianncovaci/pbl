import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerPanelComponent } from './officer-panel.component';

describe('OfficerPanelComponent', () => {
  let component: OfficerPanelComponent;
  let fixture: ComponentFixture<OfficerPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfficerPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OfficerPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
