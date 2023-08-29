const { remote } = require("webdriverio");
const fs = require("fs");
const dayjs = require("dayjs");

const capabilities = {
  platformName: "Android",
  "appium:automationName": "UiAutomator2",
  "appium:deviceName": "Android",
  "appium:deviceType": "phone",
  "appium:appPackage": "com.android.settings",
  "appium:appActivity": ".Settings",
};

const wdOpts = {
  hostname: process.env.APPIUM_HOST || "localhost",
  port: parseInt(process.env.APPIUM_PORT, 10) || 4723,
  logLevel: "info",
  capabilities,
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
    // This will change to JobRunId. We are temporary using datetime for now. This replaces : with . for file system.
    const dateFolder = dayjs()
      .format("YYYY-MM-DDTHH:mm:ssZ")
      .replace(/[:]+/g, ".");
    const writePath = `./cdn/teams/0/jobs/0/runs/${dateFolder}`;
    fs.mkdirSync(writePath, { recursive: true });
    fs.writeFileSync(`${writePath}/video.mp4`, videoRecording, "base64");
  });
