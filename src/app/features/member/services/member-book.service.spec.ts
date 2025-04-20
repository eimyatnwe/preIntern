import { TestBed } from '@angular/core/testing';

import { MemberBookService } from './member-book.service';

describe('MemberBookService', () => {
  let service: MemberBookService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MemberBookService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
