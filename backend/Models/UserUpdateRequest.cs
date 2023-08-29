namespace backend.Models;

using System.ComponentModel.DataAnnotations;
using backend.Entities;

public class UserUpdateRequest
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }

  [EmailAddress]
  public string? Email { get; set; }

  private string? _password;
  [MinLength(8)]
  public string? Password
  {
    get => _password;
    set => _password = replaceEmptyWithNull(value);
  }

  private string? _confirmPassword;
  [Compare("Password")]
  public string? ConfirmPassword
  {
    get => _confirmPassword;
    set => _confirmPassword = replaceEmptyWithNull(value);
  }

  private string? replaceEmptyWithNull(string? value)
  {
    // replace empty string with null to make field optional
    return string.IsNullOrEmpty(value) ? null : value;
  }
}