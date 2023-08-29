namespace backend.Entities;

using System.Text.Json.Serialization;

public class User
{
  public int Id { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? Email { get; set; }
  public DateTime Created { get; set; }
  public DateTime? Updated { get; set; }
  public DateTime? LastLogin { get; set; }

  [JsonIgnore]
  public string? Password { get; set; }
}