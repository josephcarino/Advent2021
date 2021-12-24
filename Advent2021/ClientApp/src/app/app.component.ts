import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, Inject, OnDestroy } from '@angular/core';
//import { ProblemService } from './problem/problem.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent implements OnDestroy {
  title: string = "Joseph Carino's Advent of Code 2021";
  mobileQuery: MediaQueryList;

  availableProblems!: Number[];

  private _mobileQueryListener: () => void;

  constructor(//private problemService: ProblemService,
    changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

    //problemService.getProblemIds().subscribe(result => {
    //  this.availableProblems = result;
    //}, error => console.error(error));
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  isLoading(): boolean {
    return this.availableProblems! == null;
  }
}
