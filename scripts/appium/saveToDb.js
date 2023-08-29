import "dotenv/config";
import { Command } from "commander";
import fs from "fs";
import pg from "pg";

const { Client } = pg;

// CLI options
const program = new Command();
program
  .description("Save Appium test results to database.")
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

const path = `./cdn/teams/${teamId}/jobs/${jobId}/runs/${runId}`;
let hasVideo = false;
let logs = "";
let duration = 0;

const datePattern = /\d{4}-\d{2}-\d{2}[0-9:.TZ]+/;

// Check if logs exist
if (fs.existsSync(`${path}/logs.txt`)) {
  logs = fs.readFileSync(`${path}/logs.txt`, "utf8");
  const logsLines = logs.split("\n");

  // Get duration from logs
  const startReg = logsLines[0].match(datePattern);
  const endReg = logsLines[logsLines.length - 2].match(datePattern);
  if (startReg.length && endReg.length) {
    const startTime = new Date(startReg[0]);
    const endTime = new Date(endReg[0]);
    duration = Math.round((endTime - startTime) / 1000);
  }
}

// Checks if video exists
if (fs.existsSync(`${path}/video.mp4`)) {
  hasVideo = true;
}

// Save to the database
(async () => {
  const db = new Client({
    user: process.env.POSTGRES_USER,
    host: process.env.POSTGRES_HOST,
    database: process.env.POSTGRES_DB,
    password: process.env.POSTGRES_PASSWORD,
    port: 5432,
  });
  await db.connect();
  const sql = `UPDATE jobs_runs SET 
    logs = $1, 
    status = 'success', 
    hasvideo = true, 
    duration = ${isNaN(duration) ? 0 : duration}, 
    updated = NOW() 
    WHERE id = ${runId};`;
  await db.query(sql, [logs]).catch((err) => {
    throw new Error(err);
  });
  await db.end();
})();
