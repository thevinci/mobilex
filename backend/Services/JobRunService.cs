namespace backend.Services;

using AutoMapper;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using backend.Repositories;

public interface IJobRunService
{
  Task<IEnumerable<JobRun>> GetAll(int jobid);
  Task<JobRun> GetById(int id);
  Task<JobRun> Create(JobRunCreateRequest model);
}

public class JobRunService : IJobRunService
{
  private IJobRunRepository _jobRunRepository;
  private readonly IMapper _mapper;

  public JobRunService(
      IJobRunRepository jobRunRepository,
      IMapper mapper)
  {
    _jobRunRepository = jobRunRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<JobRun>> GetAll(int jobid)
  {
    if (jobid == 0)
      throw new AppException("JobId is required");

    return await _jobRunRepository.GetAll(jobid);
  }

  public async Task<JobRun> GetById(int id)
  {
    var jobRun = await _jobRunRepository.GetById(id);

    if (jobRun == null)
      throw new KeyNotFoundException("JobRun not found");

    return jobRun;
  }

  public async Task<JobRun> Create(JobRunCreateRequest model)
  {
    // map model to new job object
    var jobRun = _mapper.Map<JobRun>(model);

    // save job
    return await _jobRunRepository.Create(jobRun);
  }
}