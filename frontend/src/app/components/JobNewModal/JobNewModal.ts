import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { JobsService } from '../../services/jobs';
import { Job } from '../../types/job';

@Component({
  selector: 'JobNewModal',
  templateUrl: './JobNewModal.html',
  styleUrls: ['./JobNewModal.scss'],
})
export class JobNewModalComponent {
  @Input() show: boolean = false;
  @Output() showChange = new EventEmitter<boolean>();
  @Output() jobCreated = new EventEmitter<Job>();

  display = 'none';
  form = this.formBuilder.group({
    name: '',
    description: '',
  });

  constructor(
    private jobsService: JobsService,
    private formBuilder: FormBuilder
  ) {}

  ngOnChanges(): void {
    this.display = this.show ? 'block' : 'none';
  }

  close(): void {
    this.form.reset();
    this.display = 'none';
    this.showChange.emit(false);
  }

  open(): void {
    this.display = 'block';
    this.showChange.emit(true);
  }

  submit(): void {
    this.jobsService.add(this.form.value as Job).subscribe((newJob) => {
      this.jobCreated.emit(newJob);
      this.form.reset();
      this.close();
    });
  }
}
