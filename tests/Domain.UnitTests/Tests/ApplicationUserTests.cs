using Domain.Entities.ApplicationUsers;
using Email = Domain.Entities.ApplicationUsers.ValueObjects.Email;

namespace Domain.UnitTests.Tests;

public class ApplicationUserTests
{
    [Fact]
    public void TryCreate_WithValidParameters_ShouldCreateUser()
    {
        var user = ApplicationUser.TryCreate("John", "Doe", Email.TryFrom("john.doe@example.com").ValueObject, true);

        Assert.NotNull(user);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
        Assert.Equal(Email.TryFrom("john.doe@example.com").ValueObject, user.Email);
        Assert.True(user.WantsEmailNotifications);
    }

    [Fact]
    public void TryCreate_WithEmptyFirstName_ShouldThrowDomainException()
    {
        Assert.Throws<DomainException>(() =>
            ApplicationUser.TryCreate("", "Doe", Email.TryFrom("john.doe@example.com").ValueObject, true));
    }

    [Fact]
    public void SetRefreshToken_ShouldUpdateTokenAndExpiry()
    {
        var user = ApplicationUser.TryCreate("John", "Doe", Email.TryFrom("john.doe@example.com").ValueObject, true);
        user.SetRefreshToken("refresh_token");

        Assert.Equal("refresh_token", user.RefreshToken);
        Assert.True(user.RefreshTokenExpiryTime > DateTime.UtcNow);
    }
}