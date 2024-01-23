using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PestControl.Domain.Entities;

namespace PestControl.Data.Context;

public class PestControlDbContext(DbContextOptions<PestControlDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
