namespace backend.Entities;

using System.Text.Json.Serialization;

public class Job
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public string? Status { get; set; }
  public int? AverageDuration { get; set; }
  public int? TotalRuns { get; set; }
  public int? TotalSuccess { get; set; }
  public int? UserId { get; set; }
  public DateTime Created { get; set; }
  public DateTime? Updated { get; set; }
}