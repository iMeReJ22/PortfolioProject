import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardFormModal } from './board-form-modal';

describe('BoardFormModal', () => {
  let component: BoardFormModal;
  let fixture: ComponentFixture<BoardFormModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoardFormModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoardFormModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
