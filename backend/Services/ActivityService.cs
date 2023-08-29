namespace backend.Services;

using AutoMapper;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using backend.Repositories;

public interface IActivityService
{
  Task<IEnumerable<Activity>> GetAll();
  Task<Activity> GetById(int id);
}

public class ActivityService : IActivityService
{
  private IActivityRepository _activityRepository;
  private readonly IMapper _mapper;

  public ActivityService(
      IActivityRepository activityRepository,
      IMapper mapper)
  {
    _activityRepository = activityRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<Activity>> GetAll()
  {
    return await _activityRepository.GetAll();
  }

  public async Task<Activity> GetById(int id)
  {
    var activity = await _activityRepository.GetById(id);

    if (activity == null)
      throw new KeyNotFoundException("Activity not found");

    return activity;
  }
}