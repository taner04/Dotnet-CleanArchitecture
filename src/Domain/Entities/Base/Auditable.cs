using Domain.Abstraction;

namespace Domain.Entities.Base;

public abstract class Auditable : IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}