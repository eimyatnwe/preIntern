import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environment/environment';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class BorrowService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  createBorrowRecord(borrowRecord: { bookTitle: string, memberEmail: string, borrowDate: string, dueDate: string, status: string }): Observable<any> {
    return this.http.post(`${environment.apiBaseUrl}/api/borrowRecord?addAuth=true`, borrowRecord);
  }

  getBorrowRecords(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiBaseUrl}/api/borrowRecord?addAuth=true`);
  }

}
