namespace GMForce.NDDD.Tests.Concepts;

internal sealed class ValueObjectFixture
{
    [TestCaseSource(nameof(EqualityCases))]
    public void equalityOperatorReflectsAtomicValues(ValueObject left, ValueObject right, bool expected)
    {
        (left == right).Should().Be(expected);
    }

    private static IEnumerable<TestCaseData> EqualityCases()
    {
        yield return new TestCaseData(
            new Money(Amounts.Hundred, Currencies.Eur),
            new Money(Amounts.Hundred, Currencies.Eur),
            true).SetName("equalWhenAllValuesMatch");
        yield return new TestCaseData(
            new Money(Amounts.Hundred, Currencies.Eur),
            new Money(Amounts.TwoHundred, Currencies.Eur),
            false).SetName("notEqualWhenAmountDiffers");
        yield return new TestCaseData(
            new Money(Amounts.Hundred, Currencies.Eur),
            new Money(Amounts.Hundred, Currencies.Usd),
            false).SetName("notEqualWhenCurrencyDiffers");
    }

    [Test]
    public void inequalityOperatorIsNegationOfEquality()
    {
        var a = new Money(Amounts.Hundred, Currencies.Eur);
        var b = new Money(Amounts.Hundred, Currencies.Usd);

        (a != b).Should().BeTrue();
    }

    [Test]
    public void nullEqualsNull()
    {
        ValueObject? a = null;
        ValueObject? b = null;

        (a! == b!).Should().BeTrue();
    }

    [Test]
    public void notEqualToNull()
    {
        ValueObject value = new Money(Amounts.Hundred, Currencies.Eur);

        (value == null!).Should().BeFalse();
    }
}

file sealed class Money(decimal amount, string currency) : ValueObject
{
    private decimal Amount { get; } = amount;
    private string Currency { get; } = currency;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
