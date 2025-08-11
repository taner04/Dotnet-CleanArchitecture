using Domain.Entities.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds
{
    public static class UserSeed
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            var userId = UserId.From(Guid.Parse("01989884-c581-745b-b2f2-d431f5602652"));

            modelBuilder.Entity<User>().HasData(new
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "doe@mail.com",
                PasswordHash = "$2a$12$Me0cidHOdqgvogI2GRBLN.zpF9MIynObUTmlP2vRqRvz3pKjGbJ9q",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            });

            modelBuilder.Entity<User>().OwnsOne(u => u.Jwt).HasData(new
            {
                UserId = userId, // <- Foreign Key!
                Token = JwtToken.From("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwMTk4OTg4NC1jNTgxLTc0NWItYjJmMi1kNDMxZjU2MDI2NTIiLCJlbWFpbCI6ImRvZUBtYWlsLmNvbSIsIm5iZiI6MTc1NDkwNTQ5NSwiZXhwIjoxNzU0OTA5MDk1LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.gqv_MmGUdCBFr9CKBlQYI6oID9J0lQGL6Sh3maiyCgQ"),
                TokenExpiration = JwtTokenExpiration.From(new DateTime(2025, 12, 31, 23, 59, 59, DateTimeKind.Utc)),
                RefreshToken = JwtToken.From("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwMTk4OTg4NC1jNTgxLTc0NWItYjJmMi1kNDMxZjU2MDI2NTIiLCJlbWFpbCI6ImRvZUBtYWlsLmNvbSIsIm5iZiI6MTc1NDkwNTQ5NSwiZXhwIjoxNzU3NDk3NDk1LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.DlqCR-wmvI7CY5QE5-Kq6XH5jXL-b-HMlaJ4_NjRfyA"),
                RefreshTokenExpiration = JwtTokenExpiration.From(new DateTime(2025, 12, 31, 23, 59, 59, DateTimeKind.Utc))
            });
        }
    }
}
