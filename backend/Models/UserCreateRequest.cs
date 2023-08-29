namespace backend.Models;

using System.ComponentModel.DataAnnotations;
using backend.Entities;

public class UserCreateRequest
{
  [Required]
  public string? FirstName { get; set; }

  [Required]
  public string? LastName { get; set; }

  [Required]
  [EmailAddress]
  public string? Email { get; set; }

  [Required]
  [MinLength(8)]
  public string? Password { get; set; }

  [Required]
  [Compare("Password")]
  public string? ConfirmPassword { get; set; }
}