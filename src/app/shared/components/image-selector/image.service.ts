import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BookImage } from '../../models/book-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  
  selectedImage: BehaviorSubject<BookImage> = new BehaviorSubject<BookImage>({
    id : '',
    fileExtension: '',
    fileName: '',
    title:'',
    url: ''
  });


  constructor(private http: HttpClient) { }
  uploadImage(file: File, fileName: string, title: string): Observable<BookImage>{
    const formData = new FormData();
    formData.append('file',file);
    formData.append('fileName',fileName);
    formData.append('title',title);

    return this.http.post<BookImage>(`${environment.apiBaseUrl}/api/images`,formData);
  }

  getAllImages() : Observable<BookImage[]>{
    return this.http.get<BookImage[]>(`${environment.apiBaseUrl}/api/images`);
  }

  selectImage(image: BookImage): void{
    this.selectedImage.next(image);
  }

  onSelectImage() : Observable<BookImage>{
    return this.selectedImage.asObservable();
  }
}
