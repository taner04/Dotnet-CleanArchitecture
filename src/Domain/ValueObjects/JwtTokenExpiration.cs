using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects
{
    [ValueObject<DateTime>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public partial class JwtTokenExpiration : IValueObject 
    {
        private static Validation Validate(DateTime input)
        {
            if(input < DateTime.UtcNow)
            {
                return Validation.Invalid("The expiration date cannot be in the past.");
            }

            return Validation.Ok;
        }
    } 
}
