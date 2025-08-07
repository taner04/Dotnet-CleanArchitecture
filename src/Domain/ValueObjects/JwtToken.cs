using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects
{
    [ValueObject<string>]
    public partial class JwtToken : IValueObject { }
}
