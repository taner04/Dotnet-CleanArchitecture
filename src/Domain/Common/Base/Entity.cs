using Domain.Common.Interfaces;

namespace Domain.Common.Base
{
    public abstract class Entity<TId> : IEntity<TId> 
        where TId : struct
    {
        public TId Id { get; init; }
    }
}
