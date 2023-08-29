namespace backend.Helpers;

using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

public class DataContext
{
  private DbSettings _dbSettings;

  public DataContext(IOptions<DbSettings> dbSettings)
  {
    _dbSettings = dbSettings.Value;
  }

  public IDbConnection CreateConnection()
  {
    var connectionString = $"Host={_dbSettings.Server}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
    return new NpgsqlConnection(connectionString);
  }

  public async Task Init()
  {
    await _initDatabase();
    await _initTables();
  }

  private async Task _initDatabase()
  {
    // create database if it doesn't exist
    var connectionString = $"Host={_dbSettings.Server}; Database=postgres; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
    using var connection = new NpgsqlConnection(connectionString);
    var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
    var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
    if (dbCount == 0)
    {
      var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
      await connection.ExecuteAsync(sql);
    }
  }

  private async Task _initTables()
  {
    // create tables if they don't exist
    using var connection = CreateConnection();

    await _initActivities();
    await _initJobs();
    await _initJobRuns();
    await _initStats();
    await _initUsers();

    async Task _initActivities()
    {
      var sql = """
        CREATE TABLE IF NOT EXISTS activities (
          Id SERIAL PRIMARY KEY, 
          Name VARCHAR,
          Description VARCHAR,
          UserId INT,
          TeamId INT,
          Created TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
          Updated TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
        );
        """;
      await connection.ExecuteAsync(sql);
    }

    async Task _initJobs()
    {
      var sql = """
        CREATE TABLE IF NOT EXISTS jobs (
          Id SERIAL PRIMARY KEY, 
          Name VARCHAR,
          Description VARCHAR,
          Status VARCHAR,
          AverageTime INT,
          TotalRuns INT,
          TotalSuccess INT,
          Type VARCHAR,
          UserId INT,
          TeamId INT,
          Created TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
          Updated TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
        );
        """;
      await connection.ExecuteAsync(sql);
    }

    async Task _initJobRuns()
    {
      var sql = """
        CREATE TABLE IF NOT EXISTS "jobs_runs" (
          Id SERIAL PRIMARY KEY,
          DeviceName VARCHAR NULL DEFAULT NULL,
          Duration INTEGER NULL DEFAULT NULL,
          HasBoolean BOOLEAN NULL DEFAULT 'false',
          Os VARCHAR NULL DEFAULT NULL,
          Status VARCHAR NULL DEFAULT 'not-started',
          DeviceId INTEGER NULL DEFAULT NULL,
          Logs TEXT NULL DEFAULT NULL,
          JobId INTEGER NULL DEFAULT NULL,
          TeamId INTEGER NULL DEFAULT NULL,
          UserId INTEGER NULL DEFAULT NULL,
          Created TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
          Updated TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
        );
        """;
      await connection.ExecuteAsync(sql);
    }

    async Task _initStats()
    {
      var sql = """
        CREATE TABLE IF NOT EXISTS stats (
          Id SERIAL PRIMARY KEY, 
          Name VARCHAR,
          Value INT,
          Type VARCHAR,
          Created TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
          Updated TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
        )
        """;
      await connection.ExecuteAsync(sql);
    }

    async Task _initUsers()
    {
      var sql = """
        CREATE TABLE IF NOT EXISTS "users" (
          Id SERIAL PRIMARY KEY,
          FirstName VARCHAR NULL DEFAULT NULL,
          LastName VARCHAR NULL DEFAULT NULL,
          Email VARCHAR NULL DEFAULT NULL,
          Phone VARCHAR NULL DEFAULT NULL,
          Password VARCHAR NULL DEFAULT NULL,
          LastLogin TIMESTAMP NULL DEFAULT NULL,
          Created TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
          Updated TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
        );
        """;
      await connection.ExecuteAsync(sql);
    }
  }
}