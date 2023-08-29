import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CdsModule } from '@cds/angular';
import { ClarityModule } from '@clr/angular';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// components
import { AppComponent } from './app.component';
import { AutomateComponent } from './components/Automate/Automate';
import { DashboardComponent } from './components/Dashboard/Dashboard';
import { HomeComponent } from './components/Home/Home';
import { JobDetailsComponent } from './components/JobDetails/JobDetails';
import { JobsListComponent } from './components/JobsList/JobsList';
import { LiveComponent } from './components/Live/Live';
import { JobNewModalComponent } from './components/JobNewModal/JobNewModal';
import { ScreenshotComponent } from './components/Screenshot/Screenshot';

@NgModule({
  declarations: [
    AppComponent,
    AutomateComponent,
    DashboardComponent,
    HomeComponent,
    JobDetailsComponent,
    JobsListComponent,
    LiveComponent,
    JobNewModalComponent,
    ScreenshotComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CdsModule,
    ClarityModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
