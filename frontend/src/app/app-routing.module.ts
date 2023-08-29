import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutomateComponent } from './components/Automate/Automate';
import { JobDetailsComponent } from './components/JobDetails/JobDetails';
import { DashboardComponent } from './components/Dashboard/Dashboard';
import { HomeComponent } from './components/Home/Home';
import { LiveComponent } from './components/Live/Live';
import { ScreenshotComponent } from './components/Screenshot/Screenshot';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  { path: 'automate', component: AutomateComponent },
  { path: 'automate/:id', component: JobDetailsComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'live', component: LiveComponent },
  { path: 'screenshot', component: ScreenshotComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
