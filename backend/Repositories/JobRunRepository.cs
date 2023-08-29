namespace backend.Repositories;

using Dapper;
using backend.Entities;
using backend.Helpers;

public interface IJobRunRepository
{
  Task<IEnumerable<JobRun>> GetAll(int jobid);
  Task<JobRun> GetById(int id);
  Task<JobRun> Create(JobRun jobRun);
}

public class JobRunRepository : IJobRunRepository
{
  private DataContext _context;

  public JobRunRepository(DataContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<JobRun>> GetAll(int jobid)
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM jobs_runs WHERE jobid = @JobId ORDER BY id DESC;";
    return await connection.QueryAsync<JobRun>(sql, new { jobid });
  }

  public async Task<JobRun> GetById(int id)
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM jobs_runs WHERE id = @Id;";
    return await connection.QueryFirstOrDefaultAsync<JobRun>(sql, new { id });
  }

  public async Task<JobRun> Create(JobRun jobRun)
  {
    using var connection = _context.CreateConnection();
    var sql = """
      INSERT INTO jobs_runs (jobid, os, devicename) 
      VALUES (@JobId, @Os, @DeviceName)
      RETURNING id;
      """;
    var newId = await connection.QueryFirstOrDefaultAsync(sql, jobRun);
    var sqlGet = "SELECT * FROM jobs_runs WHERE id = @Id;";
    return await connection.QueryFirstOrDefaultAsync<JobRun>(sqlGet, new { newId.id });
  }
}