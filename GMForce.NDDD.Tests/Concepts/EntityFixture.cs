namespace GMForce.NDDD.Tests.Concepts;

internal sealed class EntityFixture
{
    [Test]
    public void EqualWhenSameTypeAndId()
    {
        var a = new OrderEntity(EntityIds.First);
        var b = new OrderEntity(EntityIds.First);

        a.Equals(b).ShouldBeTrue();
    }

    [TestCaseSource(typeof(EntitySource), nameof(EntitySource.UnequalCases))]
    public void NotEqualWhenTypeOrIdDiffers(Entity<int> entity, object? other)
    {
        entity.Equals(other).ShouldBeFalse();
    }

    [Test]
    public void HashCodeMatchesForEqualEntities()
    {
        var a = new OrderEntity(EntityIds.First);
        var b = new OrderEntity(EntityIds.First);

        a.GetHashCode().ShouldBe(b.GetHashCode());
    }
}
