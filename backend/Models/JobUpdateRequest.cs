namespace backend.Models;

using System.ComponentModel.DataAnnotations;
using backend.Entities;

public class JobUpdateRequest
{
  public string? Name { get; set; }
  public string? Description { get; set; }
}