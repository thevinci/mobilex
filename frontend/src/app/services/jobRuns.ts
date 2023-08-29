import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { JobRun } from '../types/jobRun';
import { log, handleError } from '../helpers/services';

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class JobRunsService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  private apiUrl = 'http://localhost:5127/JobRuns';

  constructor(private http: HttpClient) {}

  execute(jobRun: JobRun): Observable<JobRun> {
    return this.http.post<JobRun>(this.apiUrl, jobRun, this.httpOptions).pipe(
      tap((_) => log('JobRun executed')),
      catchError(handleError<JobRun>('execute'))
    );
  }

  get(jobid: number): Observable<JobRun[]> {
    return this.http.get<JobRun[]>(`${this.apiUrl}?jobid=${jobid}`).pipe(
      tap((_) => log('JobRuns fetched')),
      catchError(handleError<JobRun[]>('get', []))
    );
  }

  getById(id: number): Observable<JobRun> {
    return this.http.get<JobRun>(`${this.apiUrl}/${id}`).pipe(
      tap((_) => log(`JobRun fetched. id=${id}`)),
      catchError(handleError<JobRun>(`getById id=${id}`))
    );
  }
}
