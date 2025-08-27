namespace Domain.Common.Interfaces;

/// <summary>
/// Interface for auditable entities, providing properties for creation and update timestamps.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Nullable if the entity has not been updated since creation.
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}