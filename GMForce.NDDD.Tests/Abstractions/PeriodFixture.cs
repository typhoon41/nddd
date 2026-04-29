namespace GMForce.NDDD.Tests.Abstractions;

internal sealed class PeriodFixture
{
    [Test]
    public void throwsWhenStartDateIsAfterEndDate()
    {
        Action act = () => new Period(Dates.Later, Dates.Earlier);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void createsWithValidDateRange()
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.StartDate.Should().Be(Dates.Earlier);
        period.EndDate.Should().Be(Dates.Later);
    }

    [TestCaseSource(nameof(ContainsCases))]
    public void containsReflectsDatePosition(DateTimeOffset date, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.Contains(date).Should().Be(expected);
    }

    private static IEnumerable<TestCaseData> ContainsCases()
    {
        yield return new TestCaseData(Dates.Before, false).SetName("beforeRange");
        yield return new TestCaseData(Dates.Earlier, true).SetName("onStartBoundary");
        yield return new TestCaseData(Dates.Between, true).SetName("withinRange");
        yield return new TestCaseData(Dates.Later, true).SetName("onEndBoundary");
        yield return new TestCaseData(Dates.After, false).SetName("afterRange");
    }

    [TestCaseSource(nameof(InFutureCases))]
    public void inFutureReflectsCurrentTime(DateTimeOffset now, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.InFuture(FakeTimeAt(now)).Should().Be(expected);
    }

    private static IEnumerable<TestCaseData> InFutureCases()
    {
        yield return new TestCaseData(Dates.Before, true).SetName("currentTimeBeforeStartIsInFuture");
        yield return new TestCaseData(Dates.Between, false).SetName("currentTimeAfterStartIsNotInFuture");
    }

    [TestCaseSource(nameof(InPastCases))]
    public void inPastReflectsCurrentTime(DateTimeOffset now, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.InPast(FakeTimeAt(now)).Should().Be(expected);
    }

    private static IEnumerable<TestCaseData> InPastCases()
    {
        yield return new TestCaseData(Dates.Between, false).SetName("currentTimeBeforeEndIsNotInPast");
        yield return new TestCaseData(Dates.After, true).SetName("currentTimeAfterEndIsInPast");
    }

    [TestCaseSource(nameof(InPresentCases))]
    public void inPresentReflectsCurrentTime(DateTimeOffset now, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.InPresent(FakeTimeAt(now)).Should().Be(expected);
    }

    private static IEnumerable<TestCaseData> InPresentCases()
    {
        yield return new TestCaseData(Dates.Before, false).SetName("currentTimeBeforeRangeIsNotInPresent");
        yield return new TestCaseData(Dates.Between, true).SetName("currentTimeWithinRangeIsInPresent");
        yield return new TestCaseData(Dates.After, false).SetName("currentTimeAfterRangeIsNotInPresent");
    }

    [Test]
    public void inMinutesCalculatesDuration()
    {
        var period = new Period(Dates.KnownStart, Dates.KnownEnd);

        period.InMinutes.Should().Be(Dates.KnownDurationMinutes);
    }

    [Test]
    public void toStringFormatsAsBracketedDateRange()
    {
        var period = new Period(Dates.KnownStart, Dates.KnownEnd);

        period.ToString().Should().Be($"[{period.StartDate:d}, {period.EndDate:d}]");
    }

    private static TimeProvider FakeTimeAt(DateTimeOffset at)
    {
        var timeProvider = A.Fake<TimeProvider>();
        A.CallTo(() => timeProvider.GetUtcNow()).Returns(at);
        return timeProvider;
    }
}
