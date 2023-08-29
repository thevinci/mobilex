namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
  private readonly ILogger<JobsController> _logger;
  private IJobService _jobService;

  public JobsController(IJobService jobService, ILogger<JobsController> logger)
  {
    _logger = logger;
    _jobService = jobService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var jobs = await _jobService.GetAll();
    return Ok(jobs);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var job = await _jobService.GetById(id);
    return Ok(job);
  }

  [HttpPost]
  public async Task<IActionResult> Create(JobCreateRequest model)
  {
    var newJob = await _jobService.Create(model);
    return Ok(newJob);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, JobUpdateRequest model)
  {
    await _jobService.Update(id, model);
    return Ok(new { message = "Job updated" });
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    await _jobService.Delete(id);
    return Ok(new { message = "Job deleted" });
  }
}