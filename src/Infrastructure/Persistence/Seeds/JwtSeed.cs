using Domain.ValueObjects;

namespace Infrastructure.Persistence.Seeds
{
    public static class JwtSeed
    {
        public static Jwt Jwt => new(
                JwtToken.From("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwMTk4OTg4NC1jNTgxLTc0NWItYjJmMi1kNDMxZjU2MDI2NTIiLCJlbWFpbCI6ImRvZUBtYWlsLmNvbSIsIm5iZiI6MTc1NDkwNTQ5NSwiZXhwIjoxNzU0OTA5MDk1LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.gqv_MmGUdCBFr9CKBlQYI6oID9J0lQGL6Sh3maiyCgQ"),
                JwtToken.From("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwMTk4OTg4NC1jNTgxLTc0NWItYjJmMi1kNDMxZjU2MDI2NTIiLCJlbWFpbCI6ImRvZUBtYWlsLmNvbSIsIm5iZiI6MTc1NDkwNTQ5NSwiZXhwIjoxNzU3NDk3NDk1LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.DlqCR-wmvI7CY5QE5-Kq6XH5jXL-b-HMlaJ4_NjRfyA")
            );
    }
}
