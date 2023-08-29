namespace backend.Repositories;

using Dapper;
using backend.Entities;
using backend.Helpers;

public interface IActivityRepository
{
  Task<IEnumerable<Activity>> GetAll();
  Task<Activity> GetById(int id);
}

public class ActivityRepository : IActivityRepository
{
  private DataContext _context;

  public ActivityRepository(DataContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Activity>> GetAll()
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM activities ORDER BY id DESC;";
    return await connection.QueryAsync<Activity>(sql);
  }

  public async Task<Activity> GetById(int id)
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM activities WHERE id = @Id;";
    return await connection.QueryFirstOrDefaultAsync<Activity>(sql, new { id });
  }
}