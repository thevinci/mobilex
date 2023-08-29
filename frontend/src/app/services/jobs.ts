import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Job } from '../types/job';
import { log, handleError } from '../helpers/services';

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class JobsService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  private apiUrl = 'http://localhost:5127/Jobs';

  constructor(private http: HttpClient) {}

  add(job: Job): Observable<Job> {
    return this.http.post<Job>(this.apiUrl, job, this.httpOptions).pipe(
      tap((_) => log('Job added')),
      catchError(handleError<Job>('add'))
    );
  }

  delete(id: number): Observable<Job> {
    return this.http.delete<Job>(`${this.apiUrl}/${id}`, this.httpOptions).pipe(
      tap((_) => log(`Job deleted. id=${id}`)),
      catchError(handleError<Job>('delete'))
    );
  }

  get(): Observable<Job[]> {
    return this.http.get<Job[]>(this.apiUrl).pipe(
      tap((_) => log('Jobs fetched')),
      catchError(handleError<Job[]>('get', []))
    );
  }

  getById(id: number): Observable<Job> {
    return this.http.get<Job>(`${this.apiUrl}/${id}`).pipe(
      tap((_) => log(`Job fetched. id=${id}`)),
      catchError(handleError<Job>(`getById id=${id}`))
    );
  }

  update(id: number, job: Job): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${id}`, job, this.httpOptions).pipe(
      tap((_) => log(`Job updated. id=${id}`)),
      catchError(handleError<Job>(`updated id=${id}`))
    );
  }
}
