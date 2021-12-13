import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-problems-list',
  templateUrl: './problems-list.component.html'
})
export class ProblemsListComponent {
  public problems: Number[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Number[]>(baseUrl + 'problem').subscribe(result => {
      this.problems = result;
    }, error => console.error(error));
  }
}
