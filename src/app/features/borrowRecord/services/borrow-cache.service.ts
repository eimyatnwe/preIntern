// borrow-cache.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BorrowCacheService {
  private storageKey = 'borrowRecords';

  private getStorage(): { [email: string]: any[] } {
    const data = localStorage.getItem(this.storageKey);
    return data ? JSON.parse(data) : {};
  }

  private saveStorage(data: { [email: string]: any[] }) {
    localStorage.setItem(this.storageKey, JSON.stringify(data));
  }

  getBorrowRecords(email: string): any[] {
    const data = this.getStorage();
    return data[email] || [];
  }

  addBorrowRecord(email: string, record: any): void {
    const data = this.getStorage();
    if (!data[email]) {
      data[email] = [];
    }
    data[email].push(record);
    this.saveStorage(data);
  }

  private getAllFromStorage(): { [email: string]: any[] } {
    const raw = localStorage.getItem(this.storageKey);
    console.log('raw', raw);
    return raw ? JSON.parse(raw) : {};
  }


  //to map to admin side
  getAllBorrowRecordsMap(): { [email: string]: any[] } {
    return this.getAllFromStorage();
  }
}
