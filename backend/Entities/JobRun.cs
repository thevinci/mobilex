namespace backend.Entities;

using System.Text.Json.Serialization;

public class JobRun
{
  public int Id { get; set; }
  public string? DeviceName { get; set; }
  public int? Duration { get; set; }
  public bool? HasVideo { get; set; }
  public string? Os { get; set; }
  public string? Status { get; set; }
  public int? DeviceId { get; set; }
  public string? Logs { get; set; }
  public int JobId { get; set; }
  public int? TeamId { get; set; }
  public int? UserId { get; set; }
  public DateTime Created { get; set; }
  public DateTime? Updated { get; set; }
}