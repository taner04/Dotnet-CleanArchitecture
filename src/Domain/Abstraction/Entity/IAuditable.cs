namespace Domain.Abstraction.Entity;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset? UpdatedAt { get; }
    
    string CreatedBy { get; }
    string? UpdatedBy { get;  }

    void SetCreated(string createdBy = null!);

    void SetUpdated(string updatedBy = null!);
}