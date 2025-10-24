import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class GetUserService {
  private apiUrl = 'https://localhost:7255/api/Register';

  constructor(private http: HttpClient) { }

  getAllUsernames(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/all-usernames`);
  }
}
