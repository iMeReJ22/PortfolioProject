import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColumnFormModal } from './column-form-modal';

describe('ColumnFormModal', () => {
  let component: ColumnFormModal;
  let fixture: ComponentFixture<ColumnFormModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ColumnFormModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColumnFormModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
