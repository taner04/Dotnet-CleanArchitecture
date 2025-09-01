namespace Domain.Abstraction;

/// <summary>
/// Defines a contract for entities that support soft deletion.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is considered deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}