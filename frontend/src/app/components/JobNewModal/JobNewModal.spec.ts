import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobNewModalComponent } from './JobNewModal';

describe('JobNewModalComponent', () => {
  let component: JobNewModalComponent;
  let fixture: ComponentFixture<JobNewModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobNewModalComponent],
    });
    fixture = TestBed.createComponent(JobNewModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
