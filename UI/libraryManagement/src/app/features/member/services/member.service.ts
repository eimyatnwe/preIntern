import { Injectable } from '@angular/core';
import { Member } from '../models/member';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) { }
  GetAllMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(`${environment.apiBaseUrl}/api/member`);
  }
}
