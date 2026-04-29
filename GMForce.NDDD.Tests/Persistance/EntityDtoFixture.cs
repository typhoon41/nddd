namespace GMForce.NDDD.Tests.Persistance;

internal sealed class EntityDtoFixture
{
    [Test]
    public void initiallyHasNoDomainEvents()
    {
        var dto = new ConcreteEntityDto();

        dto.DomainEvents.Should().BeEmpty();
    }

    [Test]
    public void addsDomainEventToCollection()
    {
        var dto = new ConcreteEntityDto();
        var @event = A.Fake<IDomainEvent>();

        dto.AddDomainEvent(@event);

        dto.DomainEvents.Should().ContainSingle().Which.Should().Be(@event);
    }

    [Test]
    public void addingMultipleEventsPreservesAll()
    {
        var dto = new ConcreteEntityDto();
        var first = A.Fake<IDomainEvent>();
        var second = A.Fake<IDomainEvent>();

        dto.AddDomainEvent(first);
        dto.AddDomainEvent(second);

        dto.DomainEvents.Should().HaveCount(2).And.ContainInOrder(first, second);
    }

    [Test]
    public void clearsDomainEvents()
    {
        var dto = new ConcreteEntityDto();
        dto.AddDomainEvent(A.Fake<IDomainEvent>());

        dto.ClearDomainEvents();

        dto.DomainEvents.Should().BeEmpty();
    }
}

file sealed record ConcreteEntityDto : EntityDto;
