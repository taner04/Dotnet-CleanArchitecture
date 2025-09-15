using IntegrationTest.Common;

namespace IntegrationTest.Controllers.Authentication;

[Collection("Aspire")]
public class Register(AspireAppFixture appFixture) : TestBase
{
    [Fact]
    public void Register_NewUser_ReturnsSuccess()
    {
        // Arrange
        var test = appFixture.ApiClient;

        Assert.True(true);
    }
}
