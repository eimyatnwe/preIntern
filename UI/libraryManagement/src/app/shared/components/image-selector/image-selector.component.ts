import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { BookImage } from '../../models/book-image.model';
import { FormsModule, NgForm } from '@angular/forms';
import { ImageService } from './image.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-image-selector',
  imports: [CommonModule,FormsModule],
  templateUrl: './image-selector.component.html',
  styleUrl: './image-selector.component.css'
})
export class ImageSelectorComponent implements OnInit{
  private file?: File;
  fileName: string = '';
  title: string = '';
  images$? : Observable<BookImage[]>;
  @ViewChild('form',{static: false}) imageUploadForm?: NgForm;

  constructor(private imageService: ImageService) {}

  ngOnInit(): void {
      this.getImages();
  }

  onFileUploadChange(event: Event):void{
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  uploadImage():void{
    if(this.file && this.fileName !== '' && this.title !== ''){
      this.imageService.uploadImage(this.file,this.fileName,this.title)
      .subscribe({
        next : (response) => {
          this.imageUploadForm?.resetForm();
          this.getImages();
        }
      });
    }
  }

  selectImage(image: BookImage):void{
    this.imageService.selectImage(image);
  }

  private getImages(){
    this.images$ = this.imageService.getAllImages();
  }
}
