using System.Transactions;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction;

public interface IBudgetDbContext : IDbContext
{
    DbSet<User> Users { get; }
}