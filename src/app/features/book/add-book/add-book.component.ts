import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormsModule } from '@angular/forms';
import { AddBook } from '../models/add-book';
import { BookService } from '../services/book.service';
import { Router } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { Book } from '../models/book';
import { MarkdownModule } from 'ngx-markdown';
import { ImageService } from '../../../shared/components/image-selector/image.service';
import { ImageSelectorComponent } from '../../../shared/components/image-selector/image-selector.component';

@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [
    FormsModule, 
    CommonModule, 
    HttpClientModule,
    MarkdownModule,
    ImageSelectorComponent
  ],
  providers: [BookService, ImageService],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent implements OnInit, OnDestroy {
  model: AddBook;
  book$?:Observable<Book[]>;
  private addBookSubscription?: Subscription;
  imagePreview: string = '';
  isImageSelectorVisible: boolean = false;
  imageSelectorSubscription?: Subscription;

  constructor(private bookService: BookService,private router: Router,private imageService: ImageService ) {
    this.model = {
      id : '',
      title :'',
      author: '',  
      publisher:'',
      category: '',
      availableCopies: 5,
      featuredImageUrl: ''
    }
  }

  ngOnInit(): void {
      this.book$ = this.bookService.GetAllBooks();
      this.imageSelectorSubscription = this.imageService.onSelectImage()
      .subscribe({
        next: (selectedImage) => {
          this.model.featuredImageUrl = selectedImage.url;
          this.closeImageSelector();
        }
      });

  }

  
  onFormSubmit():void{
    console.log("form submit");
    this.addBookSubscription = this.bookService.createBook(this.model)
    .subscribe({
      next : (response) => {
        this.router.navigateByUrl('/admin/book');
      }
    });
  }

  openImageSelector():void{
    this.isImageSelectorVisible = true;
  }

  closeImageSelector():void{
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
      this.addBookSubscription?.unsubscribe();
  }


}
