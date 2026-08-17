import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Avion, CreateAvionDto } from '../models/avion.models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AvionService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5287/api/Avion';

  getAll(): Observable<Avion[]> {
    return this.http.get<Avion[]>(this.apiUrl);
  }
  getById(id:number):Observable<Avion> {
    return this.http.get<Avion>(`${this.apiUrl}/${id}`);
  }
  create(dto:CreateAvionDto): Observable<Avion>{
    return this.http.post<Avion>(this.apiUrl,dto);
  }
  update(id:number,dto:CreateAvionDto): Observable<void>{
    return this.http.put<void>(`${this.apiUrl}/${id}`,dto);
  }
  delete(id:number):Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
