namespace GMForce.NDDD.Tests.Concepts;

internal sealed class EntityFixture
{
    [Test]
    public void equalWhenSameTypeAndId()
    {
        var a = new OrderEntity(EntityIds.First);
        var b = new OrderEntity(EntityIds.First);

        a.Equals(b).Should().BeTrue();
    }

    [TestCaseSource(nameof(UnequalCases))]
    public void notEqualWhenTypeOrIdDiffers(Entity<int> entity, object? other)
    {
        entity.Equals(other).Should().BeFalse();
    }

    private static IEnumerable<TestCaseData> UnequalCases()
    {
        yield return new TestCaseData(new OrderEntity(EntityIds.First), new OrderEntity(EntityIds.Second))
            .SetName("differentId");
        yield return new TestCaseData(new OrderEntity(EntityIds.First), new CustomerEntity(EntityIds.First))
            .SetName("differentConcreteType");
        yield return new TestCaseData(new OrderEntity(EntityIds.First), null)
            .SetName("null");
    }

    [Test]
    public void hashCodeMatchesForEqualEntities()
    {
        var a = new OrderEntity(EntityIds.First);
        var b = new OrderEntity(EntityIds.First);

        a.GetHashCode().Should().Be(b.GetHashCode());
    }
}

file sealed class OrderEntity(int id) : Entity<int>(id);
file sealed class CustomerEntity(int id) : Entity<int>(id);
