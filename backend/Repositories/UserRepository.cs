namespace backend.Repositories;

using Dapper;
using backend.Entities;
using backend.Helpers;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetAll();
  Task<User> GetById(int id);
  Task<User> GetByEmail(string email);
  Task Create(User user);
  Task Update(User user);
  Task Delete(int id);
}

public class UserRepository : IUserRepository
{
  private DataContext _context;

  public UserRepository(DataContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<User>> GetAll()
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM users;";
    return await connection.QueryAsync<User>(sql);
  }

  public async Task<User> GetById(int id)
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM users WHERE id = @Id;";
    return await connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
  }

  public async Task<User> GetByEmail(string email)
  {
    using var connection = _context.CreateConnection();
    var sql = "SELECT * FROM users WHERE email = @Email;";
    return await connection.QueryFirstOrDefaultAsync<User>(sql, new { email });
  }

  public async Task Create(User user)
  {
    using var connection = _context.CreateConnection();
    var sql = """
              INSERT INTO users (firstname, lastname, email, password)
              VALUES (@FirstName, @LastName, @Email, @Password)
              """;
    await connection.ExecuteAsync(sql, user);
  }

  public async Task Update(User user)
  {
    using var connection = _context.CreateConnection();
    var sql = """
              UPDATE users
              SET firstname = @FirstName,
                  lastname = @LastName,
                  email = @Email,
                  password = @Password,
                  updated = NOW(),
                  lastlogin = @LastLogin
              WHERE id = @Id
              """;
    await connection.ExecuteAsync(sql, user);
  }

  public async Task Delete(int id)
  {
    using var connection = _context.CreateConnection();
    var sql = "DELETE FROM users WHERE id = @Id;";
    await connection.ExecuteAsync(sql, new { id });
  }
}