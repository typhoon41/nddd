namespace GMForce.NDDD.Tests.Persistance;

internal sealed class EntityDtoFixture
{
    [Test]
    public void InitiallyHasNoDomainEvents()
    {
        var dto = new ConcreteEntityDto();

        dto.DomainEvents.ShouldBeEmpty();
    }

    [Test]
    public void AddsDomainEventToCollection()
    {
        var dto = new ConcreteEntityDto();
        var @event = A.Fake<IDomainEvent>();

        dto.AddDomainEvent(@event);

        dto.DomainEvents.ShouldHaveSingleItem().ShouldBe(@event);
    }

    [Test]
    public void AddingMultipleEventsPreservesAll()
    {
        var dto = new ConcreteEntityDto();
        var first = A.Fake<IDomainEvent>();
        var second = A.Fake<IDomainEvent>();

        dto.AddDomainEvent(first);
        dto.AddDomainEvent(second);

        dto.DomainEvents.ShouldBe(new[] { first, second });
    }

    [Test]
    public void ClearsDomainEvents()
    {
        var dto = new ConcreteEntityDto();
        dto.AddDomainEvent(A.Fake<IDomainEvent>());

        dto.ClearDomainEvents();

        dto.DomainEvents.ShouldBeEmpty();
    }
}

file sealed record ConcreteEntityDto : EntityDto;
