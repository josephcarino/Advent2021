import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProblemViewComponent } from './problem-view/problem-view.component';

const problemRoutes: Routes = [
  { path: 'problem/:id', component: ProblemViewComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(problemRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class ProblemRoutingModule { }
