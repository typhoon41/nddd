namespace GMForce.NDDD.Tests.Abstractions;

internal static class PeriodSource
{
    public static IEnumerable<TestCaseData> ContainsCases()
    {
        yield return new TestCaseData(Dates.Before, false).SetName("beforeRange");
        yield return new TestCaseData(Dates.Earlier, true).SetName("onStartBoundary");
        yield return new TestCaseData(Dates.Between, true).SetName("withinRange");
        yield return new TestCaseData(Dates.Later, true).SetName("onEndBoundary");
        yield return new TestCaseData(Dates.After, false).SetName("afterRange");
    }

    public static IEnumerable<TestCaseData> InFutureCases()
    {
        yield return new TestCaseData(Dates.Before, true).SetName("currentTimeBeforeStartIsInFuture");
        yield return new TestCaseData(Dates.Between, false).SetName("currentTimeAfterStartIsNotInFuture");
    }

    public static IEnumerable<TestCaseData> InPastCases()
    {
        yield return new TestCaseData(Dates.Between, false).SetName("currentTimeBeforeEndIsNotInPast");
        yield return new TestCaseData(Dates.After, true).SetName("currentTimeAfterEndIsInPast");
    }

    public static IEnumerable<TestCaseData> InPresentCases()
    {
        yield return new TestCaseData(Dates.Before, false).SetName("currentTimeBeforeRangeIsNotInPresent");
        yield return new TestCaseData(Dates.Between, true).SetName("currentTimeWithinRangeIsInPresent");
        yield return new TestCaseData(Dates.After, false).SetName("currentTimeAfterRangeIsNotInPresent");
    }
}
