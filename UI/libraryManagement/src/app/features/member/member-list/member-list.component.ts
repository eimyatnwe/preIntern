import { Component, OnInit } from '@angular/core';
import { MemberService } from '../services/member.service';
import { Observable } from 'rxjs';
import { Member } from '../models/member';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BorrowCacheService } from '../../borrowRecord/services/borrow-cache.service';

@Component({
  selector: 'app-member-list',
  imports: [CommonModule, HttpClientModule],
  providers: [MemberService],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  member$?: Observable<Member[]>;
  selectedMemberEmail: string = '';
  borrowRecords: any[] = [];

  constructor(
    private memberService: MemberService,
    private borrowCache: BorrowCacheService
  ) {}

  ngOnInit(): void {
    this.member$ = this.memberService.GetAllMembers();
  }

  onViewRecord(member: Member): void {
    this.selectedMemberEmail = member.email;
    const allRecordsMap = this.borrowCache.getAllBorrowRecordsMap();
    console.log('All records map:', allRecordsMap);
    this.borrowRecords = allRecordsMap[this.selectedMemberEmail] || [];
    console.log(`Borrow records for ${member.email}:`, this.borrowRecords);
  }
}
