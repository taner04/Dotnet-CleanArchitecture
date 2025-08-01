using Domain.Common.Interfaces.Entity;

namespace Domain.Common.Base
{
    public abstract class Auditable : IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
