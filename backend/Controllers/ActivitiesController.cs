namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
  private readonly ILogger<ActivitiesController> _logger;
  private IActivityService _activityService;

  public ActivitiesController(IActivityService activityService, ILogger<ActivitiesController> logger)
  {
    _logger = logger;
    _activityService = activityService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var activities = await _activityService.GetAll();
    return Ok(activities);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var activity = await _activityService.GetById(id);
    return Ok(activity);
  }
}