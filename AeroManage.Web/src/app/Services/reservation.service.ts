import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reservation, CreateReservationDto } from '../models/reservation.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ReservationService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5287/api/Reservation';

  getAll(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl);
  }
  getById(id: number): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/${id}`);
  }
  create(dto: CreateReservationDto): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, dto);
  }
  update(id: number, dto: CreateReservationDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
