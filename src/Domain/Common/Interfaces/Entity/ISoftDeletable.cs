namespace Domain.Common.Interfaces.Entity
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
