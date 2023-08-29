import { remote } from "webdriverio";
import fs from "fs";
import { Command } from "commander";

// CLI options
const program = new Command();
program
  .description("Run Appium tests.")
  .version("1.0.0", "-v, --version")
  .usage("[options]...")
  .option("-t, --teamId <teamId>", "team id")
  .option("-j, --jobId <jobId>", "job id")
  .option("-r, --runId <runId>", "run id")
  .parse(process.argv);

const options = program.opts();
const { teamId, jobId, runId } = options;

if (!teamId) throw new Error("team id is required");
if (!jobId) throw new Error("job id is required");
if (!runId) throw new Error("run id is required");

// WebdriverIO options
const wdOpts = {
  hostname: process.env.APPIUM_HOST || "localhost",
  port: parseInt(process.env.APPIUM_PORT, 10) || 4723,
  logLevel: "info",
  capabilities: {
    platformName: "Android",
    "appium:automationName": "UiAutomator2",
    "appium:deviceName": "Android",
    "appium:deviceType": "phone",
    "appium:appPackage": "com.android.settings",
    "appium:appActivity": ".Settings",
  },
};

let videoRecording;

async function runTest() {
  const driver = await remote(wdOpts);
  await driver.startRecordingScreen();
  try {
    const batteryItem = await driver.$('//*[@text="Battery"]');
    await batteryItem.click();
  } finally {
    await driver.pause(1000);
    videoRecording = await driver.stopRecordingScreen();
    await driver.deleteSession();
  }
}

runTest()
  .catch(console.error)
  .then(() => {
    const writePath = `./cdn/teams/${teamId}/jobs/${jobId}/runs/${runId}`;
    fs.mkdirSync(writePath, { recursive: true });
    fs.writeFileSync(`${writePath}/video.mp4`, videoRecording, "base64");
  });
