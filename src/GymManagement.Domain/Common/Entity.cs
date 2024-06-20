namespace GymManagement.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; init; }

    protected readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id) => this.Id = id;

    protected Entity() { }
}
