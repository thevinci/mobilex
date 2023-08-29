namespace backend.Controllers;

using CliWrap;
using CliWrap.Buffered;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

[ApiController]
[Route("[controller]")]
public class JobRunsController : ControllerBase
{
  private readonly ILogger<JobRunsController> _logger;
  private IJobRunService _jobRunService;

  public JobRunsController(IJobRunService jobRunService, ILogger<JobRunsController> logger)
  {
    _logger = logger;
    _jobRunService = jobRunService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll([FromQuery(Name = "jobid")] int jobid)
  {
    var jobRuns = await _jobRunService.GetAll(jobid);
    return Ok(jobRuns);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var jobRun = await _jobRunService.GetById(id);
    return Ok(jobRun);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] JobRunCreateRequest model)
  {
    var newJobRun = await _jobRunService.Create(model);

    Cli.Wrap("/bin/bash")
      .WithArguments(new[] { "scripts/appium/start.sh", "t=1", $"j={newJobRun.JobId}", $"r={newJobRun.Id}" })
      .WithValidation(CommandResultValidation.None)
      .WithWorkingDirectory("../")
      .ExecuteBufferedAsync();

    return Ok(newJobRun);
  }
}