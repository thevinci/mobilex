namespace backend.Repositories;

using Dapper;
using backend.Entities;
using backend.Helpers;

public interface IJobRepository
{
  Task<IEnumerable<Job>> GetAll();
  Task<Job> GetById(int id);
  Task<Job> Create(Job job);
  Task Update(Job job);
  Task Delete(int id);
}

public class JobRepository : IJobRepository
{
  private DataContext _context;

  public JobRepository(DataContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Job>> GetAll()
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM jobs ORDER BY id DESC;";
    return await connection.QueryAsync<Job>(sql);
  }

  public async Task<Job> GetById(int id)
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM jobs WHERE id = @Id;";
    return await connection.QueryFirstOrDefaultAsync<Job>(sql, new { id });
  }

  public async Task<Job> Create(Job job)
  {
    using var connection = _context.CreateConnection();
    var sql = """
      INSERT INTO jobs (name, description)
      VALUES (@Name, @Description)
      RETURNING ID
      """;
    var newId = await connection.QueryFirstOrDefaultAsync(sql, job);
    var sqlGet = "SELECT * FROM jobs WHERE id = @Id;";
    return await connection.QueryFirstOrDefaultAsync<Job>(sqlGet, new { newId.id });
  }

  public async Task Update(Job job)
  {
    using var connection = _context.CreateConnection();
    var sql = """
      UPDATE jobs
      SET 
        name=@Name, 
        description=@Description, 
        type=@Type, 
        updated=NOW()
      WHERE id=@Id
      """;
    await connection.ExecuteAsync(sql, job);
  }

  public async Task Delete(int id)
  {
    using var connection = _context.CreateConnection();
    var sql = "DELETE FROM jobs WHERE id = @Id;";
    await connection.ExecuteAsync(sql, new { id });
  }
}