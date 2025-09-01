namespace Domain.Abstraction;

/// <summary>
/// Defines properties for tracking creation and modification timestamps of an entity.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated, or <c>null</c> if it has not been updated.
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}