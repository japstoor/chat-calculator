import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'http://localhost:54011/api/chat';

  constructor(private http: HttpClient) {}

  calculate(userId: number, expression: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/calculate`, { userId, expression });
  }

  getHistory(userId: number): Observable<any> {
    return this.http.get(`http://localhost:54011/api/history/${userId}`);
  }
}
