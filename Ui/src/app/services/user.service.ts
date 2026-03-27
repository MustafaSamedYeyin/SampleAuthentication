import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private http: HttpClient) {}

  getToken() : Observable<tokenResponse> {
    return this.http.get<tokenResponse>('https://127.0.0.4:1234/api/user/gettoken',{
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    });
  }
}

interface tokenResponse {
    token: string;
  } 
  