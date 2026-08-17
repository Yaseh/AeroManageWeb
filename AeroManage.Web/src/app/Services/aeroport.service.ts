import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Aeroport, CreateAeroportDto } from '../models/aeroport.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AeroportService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5287/api/Aeroport';

  getAll(): Observable<Aeroport[]> {
    return this.http.get<Aeroport[]>(this.apiUrl);
  }
  getById(id: string): Observable<Aeroport> {
    return this.http.get<Aeroport>(`${this.apiUrl}/${id}`);
  }
  create(dto: CreateAeroportDto): Observable<Aeroport> {
    return this.http.post<Aeroport>(this.apiUrl, dto);
  }
  update(id: string, dto: CreateAeroportDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
