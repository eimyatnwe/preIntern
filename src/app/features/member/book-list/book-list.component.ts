import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Book } from '../../book/models/book';
import { BookService } from '../services/member-book.service';
import { AuthService } from '../../auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css',
  providers: [BookService, AuthService]
})
export class MemberBookListComponent implements OnInit {
  bookList: Book[] = [];

  constructor(
    private bookService: BookService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    console.log('BookListComponent initialized');
    this.bookService.GetAllBooks().subscribe(books => {
      this.bookList = books;
    });
  }

  onBorrow(book: Book): void {
    const copies = window.prompt(`How many copies of ${book.title} you want to borrow? Available Copies: ${book.availableCopies}`);
    if (copies !== null) {
      const numberOfCopies = parseInt(copies);
      // console.log('Number of Copies:', typeof(numberOfCopies));
      if (!isNaN(numberOfCopies) && numberOfCopies > 0 && numberOfCopies <= book.availableCopies) {
        const memberEmail = this.authService.getCurrentMemberId();
        console.log('Member Email:', memberEmail);

        book.availableCopies -= numberOfCopies;
        // console.log(`Book "${book.title}" now has ${book.availableCopies} copies.`);
        this.bookList = [...this.bookList];

        this.router.navigate(['/user/record'], {
          queryParams: {
            bookId: book.id,
            bookTitle: book.title,
            memberEmail: memberEmail,
            numberOfCopies: numberOfCopies
          }
        });

      } else {
        window.alert('Please enter a valid number of copies.');
      }
    }
  }
}
