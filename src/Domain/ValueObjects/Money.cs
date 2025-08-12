using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects
{
    [ValueObject<decimal>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public partial class Money : IValueObject 
    {
        private static Validation Validate(decimal input)
        {
            if(input < 0)
            {
                return Validation.Invalid("The value cannot be less then 0.");
            }

            return Validation.Ok;
        }
    }
}
