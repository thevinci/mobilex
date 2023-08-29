namespace backend.Services;

using AutoMapper;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using backend.Repositories;

public interface IJobService
{
  Task<IEnumerable<Job>> GetAll();
  Task<Job> GetById(int id);
  Task<Job> Create(JobCreateRequest model);
  Task Update(int id, JobUpdateRequest model);
  Task Delete(int id);
}

public class JobService : IJobService
{
  private IJobRepository _jobRepository;
  private readonly IMapper _mapper;

  public JobService(
      IJobRepository jobRepository,
      IMapper mapper)
  {
    _jobRepository = jobRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<Job>> GetAll()
  {
    return await _jobRepository.GetAll();
  }

  public async Task<Job> GetById(int id)
  {
    var job = await _jobRepository.GetById(id);

    if (job == null)
      throw new KeyNotFoundException("Job not found");

    return job;
  }

  public async Task<Job> Create(JobCreateRequest model)
  {
    // map model to new job object
    var job = _mapper.Map<Job>(model);

    // save job
    return await _jobRepository.Create(job);
  }
  public async Task Delete(int id)
  {
    await _jobRepository.Delete(id);
  }

  public async Task Update(int id, JobUpdateRequest model)
  {
    var job = await _jobRepository.GetById(id);

    if (job == null)
      throw new KeyNotFoundException("Job not found");

    _mapper.Map(model, job);

    await _jobRepository.Update(job);
  }
}