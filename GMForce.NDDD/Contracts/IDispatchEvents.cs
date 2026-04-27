namespace GMForce.NDDD.Contracts;
public interface IDispatchEvents
{
    Task Dispatch(IDomainEvent domainEvent);
}
