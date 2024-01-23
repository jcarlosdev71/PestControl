using System.ComponentModel.DataAnnotations;

namespace PestControl.Domain.Requests;

public class UserRequest
{
    public string? Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string Dni { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}
