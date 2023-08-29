import { Component } from '@angular/core';

@Component({
  selector: 'Automate',
  templateUrl: './Automate.html',
  styleUrls: ['./Automate.scss'],
})
export class AutomateComponent {
  refreshJobsList: boolean = false;
  showJobNewModal = false;

  closeJobNewModal(): void {
    this.showJobNewModal = false;
  }

  onJobCreated(event: any): void {
    this.refreshJobsList = true;
  }

  openJobNewModal(): void {
    this.showJobNewModal = true;
  }
}
