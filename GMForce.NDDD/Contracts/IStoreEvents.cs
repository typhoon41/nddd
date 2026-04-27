namespace GMForce.NDDD.Contracts;
public interface IStoreEvents
{
    void Add(IDomainEvent domainEvent);
    Task Publish();
}
