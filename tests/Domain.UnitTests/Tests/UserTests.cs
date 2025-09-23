namespace Domain.UnitTests.Tests;

public class UserTests
{
    [Fact]
    public void TryCreate_WithValidParameters_ShouldCreateUser()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = Email.TryFrom("john.doe@example.com").ValueObject;
        var wantsEmailNotifications = true;

        // Act
        var user = User.TryCreate(firstName, lastName, email, wantsEmailNotifications);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(email, user.Email);
        Assert.Equal(wantsEmailNotifications, user.WantsEmailNotifications);
    }

    [Fact]
    public void TryCreate_WithEmptyFirstName_ShouldThrowDomainException()
    {
        var lastName = "Doe";
        var email = Email.TryFrom("john.doe@example.com").ValueObject;

        Assert.Throws<DomainException>(() =>
            User.TryCreate("", lastName, email, true));
    }

    [Fact]
    public void SetRefreshToken_ShouldUpdateTokenAndExpiry()
    {
        var user = User.TryCreate("John", "Doe", Email.TryFrom("john.doe@example.com").ValueObject, true);
        var token = "refresh_token";

        user.SetRefreshToken(token);

        Assert.Equal(token, user.RefreshToken);
        Assert.True(user.RefreshTokenExpiryTime > DateTime.UtcNow);
    }
}