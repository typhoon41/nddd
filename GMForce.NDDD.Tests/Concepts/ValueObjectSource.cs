namespace GMForce.NDDD.Tests.Concepts;

internal static class ValueObjectSource
{
    public static IEnumerable<TestCaseData> EqualityCases()
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
}

internal sealed class Money(decimal amount, string currency) : ValueObject
{
    private decimal Amount { get; } = amount;
    private string Currency { get; } = currency;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
