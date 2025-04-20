import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, RouterLink, HttpClientModule],
  providers: [BookService],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {
  book$?: Observable<Book[]>;

  
 
  constructor(private bookService : BookService, private router: Router) {}
  ngOnInit(): void {
    console.log('BookListComponent initialized');
      this.book$ = this.bookService.GetAllBooks();
  }

  onDelete(id:string):void{
    const confirmed = window.confirm('Are you sure you want to delete this book?');
    if (!confirmed) return;
    this.bookService.deleteBook(id)
    .subscribe({
      next: ()=>{
        this.book$ = this.bookService.GetAllBooks();
      }
    });
  }
}
