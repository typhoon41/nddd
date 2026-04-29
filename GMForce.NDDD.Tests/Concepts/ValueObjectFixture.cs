namespace GMForce.NDDD.Tests.Concepts;

internal sealed class ValueObjectFixture
{
    [TestCaseSource(typeof(ValueObjectSource), nameof(ValueObjectSource.EqualityCases))]
    public void EqualityOperatorReflectsAtomicValues(ValueObject left, ValueObject right, bool expected)
    {
        (left == right).Should().Be(expected);
    }

    [Test]
    public void InequalityOperatorIsNegationOfEquality()
    {
        var a = new Money(Amounts.Hundred, Currencies.Eur);
        var b = new Money(Amounts.Hundred, Currencies.Usd);

        (a != b).Should().BeTrue();
    }

    [Test]
    public void NullEqualsNull()
    {
        ValueObject? a = null;
        ValueObject? b = null;

        (a! == b!).Should().BeTrue();
    }

    [Test]
    public void NotEqualToNull()
    {
        ValueObject value = new Money(Amounts.Hundred, Currencies.Eur);

        (value == null!).Should().BeFalse();
    }
}
