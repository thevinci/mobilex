export interface JobRun {
  id?: number;
  deviceName: string;
  duration: number;
  hasVideo: boolean;
  os: string;
  status: string;
  logs?: string;
  deviceid?: number;
  jobid: number;
  teamid: number;
  userid: number;
  created?: Date;
  updated?: Date;
}
