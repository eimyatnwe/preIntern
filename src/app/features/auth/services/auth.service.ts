import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environment/environment';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient, private cookieService: CookieService) { }
  
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/auth/login`, 
    {
      email: request.email,
      password: request.password
    });
  }

  register(request: {email: string, password: string}): Observable<any> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/auth/register`, 
    {
      email: request.email,
      password: request.password
    });
  }
  
  setUser(user: User) : void {
    this.$user.next(user);
    localStorage.setItem('user-email',user.email);
    localStorage.setItem('user-roles',user.roles.join(','));
  }

  user() : Observable<User | undefined> {
   
    return this.$user.asObservable();
    
  }

  storeMemberInfo(memberId: string): void {
    localStorage.setItem('memberId', memberId);
  }

  getCurrentMemberId(): string | null {
    return localStorage.getItem('user-email');
  }

  logout(): void {
    
    this.cookieService.delete('Authorization', '/');
  
    this.setUser({ email: '', roles: [] });
  }
  
}
