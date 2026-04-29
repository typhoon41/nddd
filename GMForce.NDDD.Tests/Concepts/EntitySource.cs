namespace GMForce.NDDD.Tests.Concepts;

internal static class EntitySource
{
    public static IEnumerable<TestCaseData> UnequalCases()
    {
        yield return new TestCaseData(new OrderEntity(EntityIds.First), new OrderEntity(EntityIds.Second))
            .SetName("differentId");
        yield return new TestCaseData(new OrderEntity(EntityIds.First), new CustomerEntity(EntityIds.First))
            .SetName("differentConcreteType");
        yield return new TestCaseData(new OrderEntity(EntityIds.First), null)
            .SetName("null");
    }
}

internal sealed class OrderEntity(int id) : Entity<int>(id);
internal sealed class CustomerEntity(int id) : Entity<int>(id);
