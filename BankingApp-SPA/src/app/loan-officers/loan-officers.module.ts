import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { OfficerPanelComponent } from './officer-panel/officer-panel.component';

@NgModule({
  declarations: [OfficerPanelComponent],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    AppRoutingModule,
    CommonModule,
  ],
  exports: [OfficerPanelComponent],
})
export class LoanOfficersModule {}
