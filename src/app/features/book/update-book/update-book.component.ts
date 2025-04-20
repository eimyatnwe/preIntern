import { Component, OnDestroy, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../services/book.service';
import { ImageService } from '../../../shared/components/image-selector/image.service';
import { updateBook } from '../models/update-book.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ImageSelectorComponent } from '../../../shared/components/image-selector/image-selector.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-update-book',
  imports: [FormsModule,CommonModule,ImageSelectorComponent,HttpClientModule],
  providers: [BookService,ImageService],
  templateUrl: './update-book.component.html',
  styleUrl: './update-book.component.css'
})
export class UpdateBookComponent implements OnInit, OnDestroy{
  id: string | null = null;
  model?: Book;
  routeSubscription?: Subscription
  isImageSelectorVisible: boolean = false;
  imageSelectorSubscription?: Subscription;
  updateBookSubscription?: Subscription;

  constructor(private route: ActivatedRoute, private bookService: BookService,private router: Router, private imageService: ImageService) {}

  ngOnInit(): void {
      this.route.paramMap.subscribe({
        next: (response) => {
          this.id = response.get('id');
          if (this.id) {
            this.bookService.GetBookById(this.id).subscribe({
              next: (book) => {
                this.model = book;
              }
            });
          }
          this.imageSelectorSubscription = this.imageService.onSelectImage()
            .subscribe({
              next: (image) => {
                if (this.model) {
                  this.model.featuredImageUrl = image.url;
                  this.isImageSelectorVisible = false;
                }
              }
            });
        }
      });
  }

  onFormSubmit():void{
    if(this.model && this.id){
      var updateBook : updateBook = {
        title : this.model.title,
        author: this.model.author,  
        publisher: this.model.publisher,
        category: this.model.category,
        availableCopies: this.model.availableCopies,
        featuredImageUrl : this.model.featuredImageUrl
      };

      this.bookService.updateBook(this.id,updateBook).subscribe({
        next: (response) => {
          this.router.navigate(['/admin/book']);
        }
      });
    }
  }

  openImageSelector():void{
    this.isImageSelectorVisible = true;
  }

  closeImageSelector():void{
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.imageSelectorSubscription?.unsubscribe();
    this.updateBookSubscription?.unsubscribe();
  }


}
