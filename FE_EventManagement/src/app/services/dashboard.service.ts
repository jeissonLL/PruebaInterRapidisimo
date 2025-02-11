import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { event } from '../interfaces/event';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = 'http://localhost:5029/api/Event'; // Ajusta según tu API

  constructor(private http: HttpClient) {}

  getEvents(): Observable<event[]> {
    return this.http.get<event[]>(this.apiUrl);
  }
}
