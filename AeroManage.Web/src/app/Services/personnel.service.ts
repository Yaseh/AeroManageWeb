import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Personnel, CreatePersonnelDto } from '../models/personnel.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PersonnelService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5287/api/Personnel';

  getAll(): Observable<Personnel[]> {
    return this.http.get<Personnel[]>(this.apiUrl);
  }
  getById(id: number): Observable<Personnel> {
    return this.http.get<Personnel>(`${this.apiUrl}/${id}`);
  }
  create(dto: CreatePersonnelDto): Observable<Personnel> {
    return this.http.post<Personnel>(this.apiUrl, dto);
  }
  update(id: number, dto: CreatePersonnelDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
