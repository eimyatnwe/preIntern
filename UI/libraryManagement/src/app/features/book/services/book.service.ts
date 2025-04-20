import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { environment } from '../../../../environment/environment';
import { AddBook } from '../models/add-book';
import { CookieService } from 'ngx-cookie-service';
import { updateBook } from '../models/update-book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  constructor(private http: HttpClient, private cookieService: CookieService) { }
  
  GetAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${environment.apiBaseUrl}/api/book`);
  }

  GetBookById(id: string): Observable<Book> {
    return this.http.get<Book>(`${environment.apiBaseUrl}/api/book/${id}`);
  }

  createBook(data: AddBook):Observable<Book>{
    const reqData = {
      title : data.title,
      author: data.author,  
      publisher: data.publisher,
      category: data.category,
      availableCopies : data.availableCopies,
      featuredImageUrl : data.featuredImageUrl
    }
    console.log('Token:', this.cookieService.get('Authorization'));

    
    return this.http.post<Book>(`${environment.apiBaseUrl}/api/book?addAuth=true`,reqData);
  }

  deleteBook(id: string):Observable<Book>{
    return this.http.delete<Book>(`${environment.apiBaseUrl}/api/book/${id}?addAuth=true`);
  }

  updateBook(id:string, updateBook: updateBook): Observable<Book>{
    return this.http.put<Book>(`${environment.apiBaseUrl}/api/book/${id}?addAuth=true`, updateBook);
  }
}
