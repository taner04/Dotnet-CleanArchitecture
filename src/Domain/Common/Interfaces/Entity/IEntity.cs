namespace Domain.Common.Interfaces.Entity
{
    public interface IEntity<TId> where TId : struct
    {
        TId Id { get; init; }
    }
}
