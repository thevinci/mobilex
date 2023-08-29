import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomateComponent } from './Automate';

describe('AutomateComponent', () => {
  let component: AutomateComponent;
  let fixture: ComponentFixture<AutomateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AutomateComponent],
    });
    fixture = TestBed.createComponent(AutomateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
