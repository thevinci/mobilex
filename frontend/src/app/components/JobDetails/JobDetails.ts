import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Job } from 'src/app/types/job';
import { JobRun } from 'src/app/types/jobRun';
import { JobsService } from 'src/app/services/jobs';
import { JobRunsService } from 'src/app/services/jobRuns';

@Component({
  selector: 'JobDetails',
  styleUrls: ['./JobDetails.scss'],
  templateUrl: './JobDetails.html',
})
export class JobDetailsComponent implements OnInit {
  selectedJobId: number = Number(this.route.snapshot.paramMap.get('id'));
  selectedJobRun: JobRun | undefined;
  job: Job | undefined;
  jobRuns: JobRun[] | undefined;

  constructor(
    private route: ActivatedRoute,
    private jobsService: JobsService,
    private jobRunsService: JobRunsService
  ) {}

  ngOnInit(): void {
    this.getJob();
    this.getRuns();
  }

  getJob(): void {
    this.jobsService
      .getById(this.selectedJobId)
      .subscribe((job) => (this.job = job));
  }

  getRuns(): void {
    this.jobRunsService.get(this.selectedJobId).subscribe((runs) => {
      this.jobRuns = runs;

      if (runs.length > 0) {
        this.selectedJobRun = runs[0];
      }
    });
  }

  onClickExecute(): void {
    const _this = this;

    this.jobRunsService
      .execute({ jobid: this.selectedJobId } as JobRun)
      .subscribe((run) => {
        this.jobRuns?.unshift(run);
        // TODO: Should make a websocket connection to get the status of the job run
        setTimeout(() => {
          _this.getRuns.call(_this);
        }, 10000);
      });
  }

  onClickJobRun(jobRun: JobRun): void {
    this.selectedJobRun = jobRun;
  }
}
