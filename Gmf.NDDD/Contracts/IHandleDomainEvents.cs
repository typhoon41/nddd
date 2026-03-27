namespace Gmf.NDDD.Contracts;

public interface IHandleDomainEvents
{
    Task Handle(IDomainEvent notification, CancellationToken cancellationToken = default);
}
