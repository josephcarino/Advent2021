import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { Problem } from './problem';

@Injectable({
  providedIn: 'root',
})
export class ProblemService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,) {

  }

  getProblemIds(): Observable<Number[]> {
    return this.http.get<Number[]>(this.baseUrl + 'problem');
  }

  getProblem(id: number | string): Observable<Problem> {
    return this.http.get<Problem>(this.baseUrl + 'problem/' + id);
  }
}
