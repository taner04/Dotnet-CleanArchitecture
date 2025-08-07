using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects
{
    [ValueObject<DateTime>]
    public partial class JwtTokenExpiration : IValueObject { } 
}
