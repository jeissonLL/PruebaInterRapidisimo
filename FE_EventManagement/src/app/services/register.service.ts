import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { register } from '../interfaces/register';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = 'http://localhost:5029/api/User'; // Ajusta la URL de tu API

  constructor(private http: HttpClient) {}

  // Crear usuario
  createUser(user: register): Observable<register> {
    return this.http.post<register>(`${this.apiUrl}`, user);
  }

}
