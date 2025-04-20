import { Component, OnInit } from '@angular/core';
import { BorrowService } from '../services/borrow.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { BorrowCacheService } from '../services/borrow-cache.service';

@Component({
  selector: 'app-borrow-record',
  imports: [HttpClientModule, CommonModule],
  providers: [BorrowService],
  templateUrl: './borrow-record.component.html',
  styleUrl: './borrow-record.component.css'
})
export class BorrowRecordComponent implements OnInit {
  borrowRecords: any[] = [];
  memberEmail: string = '';

  constructor(
    private route: ActivatedRoute,
    private borrowService: BorrowService,
    private borrowCache: BorrowCacheService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      console.log('Query params received:', params);
      const bookTitle = params['bookTitle'];
      this.memberEmail = params['memberEmail'];
      console.log('email:', this.memberEmail);
  
      if (!this.memberEmail) {
        console.error('Missing required memberEmail');
        return;
      }
  
      this.borrowRecords = this.borrowCache.getBorrowRecords(this.memberEmail);
      console.log('Loaded borrowRecords from cache:', this.borrowRecords);
  
      if (!bookTitle) return;
  
      const borrowRecord = {
        bookTitle: bookTitle,
        memberEmail: this.memberEmail,
        borrowDate: formatDate(new Date(), 'yyyy-MM-dd', 'en-US'),
        dueDate: formatDate(new Date(Date.now() + 14 * 24 * 60 * 60 * 1000), 'yyyy-MM-dd', 'en-US'),
        status: 'Borrowed'
      };
  
      this.borrowService.createBorrowRecord(borrowRecord).subscribe({
        next: (response) => {
          console.log('Borrow record created:', response);
          this.borrowCache.addBorrowRecord(this.memberEmail, response);
          this.borrowRecords = this.borrowCache.getBorrowRecords(this.memberEmail);
          console.log('Updated borrowRecords array:', this.borrowRecords);
        },
        error: (error) => {
          console.error('Error creating borrow record:', error);
        }
      });
    });
  }

  
  
}
