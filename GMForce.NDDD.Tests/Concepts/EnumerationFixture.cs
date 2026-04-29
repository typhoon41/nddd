namespace GMForce.NDDD.Tests.Concepts;

internal sealed class EnumerationFixture
{
    [Test]
    public void toStringReturnsDisplayName()
    {
        Priority.Medium.ToString().Should().Be("Medium");
    }

    [TestCaseSource(nameof(CompareToOrdering))]
    public void compareToReflectsValueOrdering(Enumeration<int> left, Enumeration<int> right, int expectedSign)
    {
        Math.Sign(left.CompareTo(right)).Should().Be(expectedSign);
    }

    private static IEnumerable<TestCaseData> CompareToOrdering()
    {
        yield return new TestCaseData(Priority.Low, Priority.High, -1).SetName("lowerValueIsLess");
        yield return new TestCaseData(Priority.Medium, Priority.Medium, 0).SetName("equalValueIsZero");
        yield return new TestCaseData(Priority.High, Priority.Low, 1).SetName("higherValueIsGreater");
    }

    [Test]
    public void compareToThrowsForNull()
    {
        Action act = () => Priority.Low.CompareTo(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestCaseSource(nameof(EqualityCases))]
    public void equalityOperatorReflectsTypeAndValue(Enumeration<int> left, Enumeration<int> right, bool expected)
    {
        (left == right).Should().Be(expected);
    }

    private static IEnumerable<TestCaseData> EqualityCases()
    {
        yield return new TestCaseData(Priority.Medium, Priority.Medium, true).SetName("sameValueIsEqual");
        yield return new TestCaseData(Priority.Low, Priority.High, false).SetName("differentValueIsNotEqual");
    }

    [Test]
    public void notEqualToDifferentConcreteType()
    {
        var priority = new Priority(2, "Medium");
        var urgency = new Urgency(2, "Medium");

        priority.Equals(urgency).Should().BeFalse();
    }

    [Test]
    public void inequalityOperatorIsNegationOfEquality()
    {
        (Priority.Low != Priority.High).Should().BeTrue();
    }

    [Test]
    public void hashCodeMatchesForEqualInstances()
    {
        var a = new Priority(2, "Medium");
        var b = new Priority(2, "Medium");

        a.GetHashCode().Should().Be(b.GetHashCode());
    }
}

file sealed class Priority(int value, string displayName) : Enumeration<int>(value, displayName)
{
    public static Priority Low { get; } = new(1, "Low");
    public static Priority Medium { get; } = new(2, "Medium");
    public static Priority High { get; } = new(3, "High");
}

file sealed class Urgency(int value, string displayName) : Enumeration<int>(value, displayName);
