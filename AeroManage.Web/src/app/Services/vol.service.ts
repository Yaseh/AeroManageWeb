import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Vol, CreateVolDto } from '../models/vol.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class VolService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5287/api/Vol';

  getAll(): Observable<Vol[]> {
    return this.http.get<Vol[]>(this.apiUrl);
  }
  getById(id: number): Observable<Vol> {
    return this.http.get<Vol>(`${this.apiUrl}/${id}`);
  }
  create(dto: CreateVolDto): Observable<Vol> {
    return this.http.post<Vol>(this.apiUrl, dto);
  }
  update(id: number, dto: CreateVolDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
