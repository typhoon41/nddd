namespace Gmf.NDDD.Contracts;
public interface IDomainEvent
{
    string Name { get; }
    dynamic Data { get; }
}
