namespace Domain.Common.Interfaces;

/// <summary>
/// Interface for entities that support soft deletion.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Indicates whether the entity is soft deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}