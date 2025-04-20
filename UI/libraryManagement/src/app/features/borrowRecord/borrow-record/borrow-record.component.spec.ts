import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowRecordComponent } from './borrow-record.component';

describe('BorrowRecordComponent', () => {
  let component: BorrowRecordComponent;
  let fixture: ComponentFixture<BorrowRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowRecordComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BorrowRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
