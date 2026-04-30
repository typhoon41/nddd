namespace GMForce.NDDD.Tests.Codebooks;

internal static class Dates
{
    public static readonly DateTimeOffset Before = new(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
    public static readonly DateTimeOffset Earlier = new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
    public static readonly DateTimeOffset Between = new(2025, 6, 1, 0, 0, 0, TimeSpan.Zero);
    public static readonly DateTimeOffset Later = new(2026, 12, 31, 0, 0, 0, TimeSpan.Zero);
    public static readonly DateTimeOffset After = new(2028, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public static readonly DateTimeOffset KnownStart = new(2024, 6, 1, 10, 0, 0, TimeSpan.Zero);
    public static readonly DateTimeOffset KnownEnd = new(2024, 6, 1, 11, 0, 0, TimeSpan.Zero);
    public const int KnownDurationMinutes = 60;
}
