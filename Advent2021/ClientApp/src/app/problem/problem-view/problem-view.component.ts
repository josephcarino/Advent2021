import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Problem } from '../problem';
import { ProblemService } from '../problem.service';

@Component({
  selector: 'app-problem-view',
  templateUrl: './problem-view.component.html',
})
export class ProblemViewComponent {
  problem$!: Observable<Problem>;
  problem!: Problem;

  constructor(private route : ActivatedRoute, private router: Router, private service: ProblemService) {

  }

  ngOnInit() {
    this.problem$! = this.route.paramMap.pipe(switchMap((params: ParamMap) => this.service.getProblem(params.get('ProblemId')!)));
    this.problem$!.subscribe(result => {
      this.problem = result;
    }, error => console.error(error));
  }

  isLoading(): boolean {
    return this.problem! == null;
  }
}
