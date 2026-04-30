namespace GMForce.NDDD.Tests.Concepts;

internal sealed class ValueObjectFixture
{
    [TestCaseSource(typeof(ValueObjectSource), nameof(ValueObjectSource.EqualityCases))]
    public void EqualityOperatorReflectsAtomicValues(ValueObject left, ValueObject right, bool expected)
    {
        (left == right).ShouldBe(expected);
    }

    [Test]
    public void InequalityOperatorIsNegationOfEquality()
    {
        var a = new Money(Amounts.Hundred, Currencies.Eur);
        var b = new Money(Amounts.Hundred, Currencies.Usd);

        (a != b).ShouldBeTrue();
    }

    [Test]
    public void NullEqualsNull()
    {
        ValueObject? a = null;
        ValueObject? b = null;

        (a! == b!).ShouldBeTrue();
    }

    [Test]
    public void NotEqualToNull()
    {
        ValueObject value = new Money(Amounts.Hundred, Currencies.Eur);

        (value == null!).ShouldBeFalse();
    }

    [Test]
    public void HashCodeMatchesForEqualValueObjects()
    {
        var a = new Money(Amounts.Hundred, Currencies.Eur);
        var b = new Money(Amounts.Hundred, Currencies.Eur);

        a.GetHashCode().ShouldBe(b.GetHashCode());
    }
}
