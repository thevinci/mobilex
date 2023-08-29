import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { JobsService } from '../../services/jobs';
import { Job } from '../../types/job';

@Component({
  selector: 'JobsList',
  templateUrl: './JobsList.html',
  styleUrls: ['./JobsList.scss'],
})
export class JobsListComponent implements OnInit, OnChanges {
  @Input() refresh: boolean = false;

  showDeleteJobModal: boolean = false;
  jobToDelete: Job | null = null;
  jobs: Job[] = [];

  constructor(private jobsService: JobsService) {}

  ngOnInit(): void {
    this.getJobs();
  }

  ngOnChanges(): void {
    if (this.refresh) {
      this.refresh = false;
      this.getJobs();
    }
  }

  getJobs(): void {
    this.jobsService.get().subscribe((jobs) => (this.jobs = jobs));
  }

  onClickCancelDeleteJob(): void {
    this.showDeleteJobModal = false;
    this.jobToDelete = null;
  }

  onClickConfirmDeleteJob(): void {
    if (!this.jobToDelete?.id) {
      console.warn('Cannot delete this job because it is null.');
      return;
    }

    this.jobsService
      .delete(this.jobToDelete.id)
      .subscribe(() => this.getJobs());
    this.showDeleteJobModal = false;
    this.jobToDelete = null;
  }

  onClickRowDelete(job: Job): void {
    if (!job) {
      console.warn('Cannot delete this job because it is null.');
      return;
    }

    this.jobToDelete = job;
    this.showDeleteJobModal = true;
  }
}
