import { TestBed } from '@angular/core/testing';

import { BorrowCacheService } from './borrow-cache.service';

describe('BorrowCacheService', () => {
  let service: BorrowCacheService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BorrowCacheService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
