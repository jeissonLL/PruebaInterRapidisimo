import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { login } from '../interfaces/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5029/login/'; // Ajusta la URL según tu backend

  constructor(private http: HttpClient) {}

  login(credentials: login): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(this.apiUrl, credentials);
  }
}
