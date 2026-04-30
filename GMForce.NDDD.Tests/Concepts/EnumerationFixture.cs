namespace GMForce.NDDD.Tests.Concepts;

internal sealed class EnumerationFixture
{
    [Test]
    public void ToStringReturnsDisplayName()
    {
        Priority.Medium.ToString().ShouldBe("Medium");
    }

    [TestCaseSource(typeof(EnumerationSource), nameof(EnumerationSource.CompareToOrdering))]
    public void CompareToReflectsValueOrdering(Enumeration<int> left, Enumeration<int> right, int expectedSign)
    {
        Math.Sign(left.CompareTo(right)).ShouldBe(expectedSign);
    }

    [Test]
    public void CompareToThrowsForNull()
    {
        Action act = () => Priority.Low.CompareTo(null);

        Should.Throw<ArgumentNullException>(act);
    }

    [TestCaseSource(typeof(EnumerationSource), nameof(EnumerationSource.EqualityCases))]
    public void EqualityOperatorReflectsTypeAndValue(Enumeration<int> left, Enumeration<int> right, bool expected)
    {
        (left == right).ShouldBe(expected);
    }

    [Test]
    public void NotEqualToDifferentConcreteType()
    {
        var priority = new Priority(2, "Medium");
        var urgency = new Urgency(2, "Medium");

        priority.Equals(urgency).ShouldBeFalse();
    }

    [Test]
    public void InequalityOperatorIsNegationOfEquality()
    {
        (Priority.Low != Priority.High).ShouldBeTrue();
    }

    [Test]
    public void HashCodeMatchesForEqualInstances()
    {
        var a = new Priority(2, "Medium");
        var b = new Priority(2, "Medium");

        a.GetHashCode().ShouldBe(b.GetHashCode());
    }
}
