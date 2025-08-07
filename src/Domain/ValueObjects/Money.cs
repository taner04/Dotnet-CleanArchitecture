using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects
{
    [ValueObject<decimal>]
    public partial class Money : IValueObject { }
}
