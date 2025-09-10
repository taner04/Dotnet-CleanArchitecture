using Domain.Entities.ApplicationUsers;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction;

public interface IApplicationDbContext : IDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }
}