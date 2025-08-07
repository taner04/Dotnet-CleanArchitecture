using Domain.Common.Interfaces;

namespace Domain.Common.Base
{
    public abstract class Auditable : IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
