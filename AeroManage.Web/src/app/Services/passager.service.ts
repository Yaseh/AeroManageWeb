import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Passager, CreatePassagerDto } from '../models/passager.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PassagerService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5287/api/Passager';

  getAll(): Observable<Passager[]> {
    return this.http.get<Passager[]>(this.apiUrl);
  }
  getById(id: number): Observable<Passager> {
    return this.http.get<Passager>(`${this.apiUrl}/${id}`);
  }
  create(dto: CreatePassagerDto): Observable<Passager> {
    return this.http.post<Passager>(this.apiUrl, dto);
  }
  update(id: number, dto: CreatePassagerDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
