namespace GMForce.NDDD.Tests.Abstractions;

internal sealed class PeriodFixture
{
    [Test]
    public void ThrowsWhenStartDateIsAfterEndDate()
    {
        Action act = () => new Period(Dates.Later, Dates.Earlier);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void CreatesWithValidDateRange()
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.StartDate.Should().Be(Dates.Earlier);
        period.EndDate.Should().Be(Dates.Later);
    }

    [TestCaseSource(typeof(PeriodSource), nameof(PeriodSource.ContainsCases))]
    public void ContainsReflectsDatePosition(DateTimeOffset date, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.Contains(date).Should().Be(expected);
    }

    [TestCaseSource(typeof(PeriodSource), nameof(PeriodSource.InFutureCases))]
    public void InFutureReflectsCurrentTime(DateTimeOffset now, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.InFuture(FakeTimeAt(now)).Should().Be(expected);
    }

    [TestCaseSource(typeof(PeriodSource), nameof(PeriodSource.InPastCases))]
    public void InPastReflectsCurrentTime(DateTimeOffset now, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.InPast(FakeTimeAt(now)).Should().Be(expected);
    }

    [TestCaseSource(typeof(PeriodSource), nameof(PeriodSource.InPresentCases))]
    public void InPresentReflectsCurrentTime(DateTimeOffset now, bool expected)
    {
        var period = new Period(Dates.Earlier, Dates.Later);

        period.InPresent(FakeTimeAt(now)).Should().Be(expected);
    }

    [Test]
    public void InMinutesCalculatesDuration()
    {
        var period = new Period(Dates.KnownStart, Dates.KnownEnd);

        period.InMinutes.Should().Be(Dates.KnownDurationMinutes);
    }

    [Test]
    public void ToStringFormatsAsBracketedDateRange()
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
