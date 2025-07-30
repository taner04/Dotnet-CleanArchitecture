using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // Passe den ConnectionString ggf. an!
            optionsBuilder.UseNpgsql("Host=localhost;Database=eshop;Username=postgres;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}