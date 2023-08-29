namespace backend.Entities;

using System.Text.Json.Serialization;

public class Activity
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public string? Type { get; set; }
  public int? UserId { get; set; }
  public int? TeamId { get; set; }
  public DateTime Created { get; set; }
  public DateTime? Updated { get; set; }
}