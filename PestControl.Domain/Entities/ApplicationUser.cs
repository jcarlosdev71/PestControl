using Microsoft.AspNetCore.Identity;

namespace PestControl.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Dni { get; set; }
}
