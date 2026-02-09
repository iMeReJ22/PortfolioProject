import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleUser } from './single-user';

describe('SingleUser', () => {
  let component: SingleUser;
  let fixture: ComponentFixture<SingleUser>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SingleUser]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleUser);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
