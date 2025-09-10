using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.ApplicationUsers;

public class ApplicationUser(string userName) : IdentityUser<UserId>(userName)
{
    public bool SendEmailNotification { get; private set; } = true;
    public bool SendSmsNotification { get; private set; } = false;
}