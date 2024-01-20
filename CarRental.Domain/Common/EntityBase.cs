namespace CarRental.Domain.Common;
public abstract class EntityBase
{
    public Guid Id { get; init; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}
