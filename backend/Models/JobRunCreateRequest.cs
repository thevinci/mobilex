namespace backend.Models;

using System.ComponentModel.DataAnnotations;
using backend.Entities;

public class JobRunCreateRequest
{
  [Required]
  public int JobId { get; set; }

  public string? Os { get; set; }
  public string? DeviceName { get; set; }
}