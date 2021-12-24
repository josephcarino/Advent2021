import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProblemViewComponent } from './problem-view/problem-view.component';

import { ProblemRoutingModule } from './problem-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ProblemRoutingModule
  ],
  declarations: [
    ProblemViewComponent
  ]
})
export class ProblemModule { }
