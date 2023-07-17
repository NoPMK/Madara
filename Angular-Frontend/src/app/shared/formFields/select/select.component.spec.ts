import { ComponentFixture, TestBed } from '@angular/core/testing';

import InputWithOptionsComponent from './select.component';

describe('InputWithFielsdComponent', () => {
  let component: InputWithOptionsComponent;
  let fixture: ComponentFixture<InputWithOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InputWithOptionsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(InputWithOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
