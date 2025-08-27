namespace Domain.Common.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}