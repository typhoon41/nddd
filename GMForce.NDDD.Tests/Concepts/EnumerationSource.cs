namespace GMForce.NDDD.Tests.Concepts;

internal static class EnumerationSource
{
    public static IEnumerable<TestCaseData> CompareToOrdering()
    {
        yield return new TestCaseData(Priority.Low, Priority.High, -1).SetName("lowerValueIsLess");
        yield return new TestCaseData(Priority.Medium, Priority.Medium, 0).SetName("equalValueIsZero");
        yield return new TestCaseData(Priority.High, Priority.Low, 1).SetName("higherValueIsGreater");
    }

    public static IEnumerable<TestCaseData> EqualityCases()
    {
        yield return new TestCaseData(Priority.Medium, Priority.Medium, true).SetName("sameValueIsEqual");
        yield return new TestCaseData(Priority.Low, Priority.High, false).SetName("differentValueIsNotEqual");
    }
}

internal sealed class Priority(int value, string displayName) : Enumeration<int>(value, displayName)
{
    public static Priority Low { get; } = new(1, "Low");
    public static Priority Medium { get; } = new(2, "Medium");
    public static Priority High { get; } = new(3, "High");
}

internal sealed class Urgency(int value, string displayName) : Enumeration<int>(value, displayName);
