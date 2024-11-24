namespace System
{
    /// <summary>Структура, реализующая механизм обращения к элементам коллекций по диапазону.<br/>
    /// Например, [0..1] - Первые 2 элемента.</summary>
    public readonly struct Range
    {
        public Index Start { get; }
        public Index End { get; }

        public Range(Index start, Index end)
        {
            Start = start;
            End = end;
        }
        public static Range StartAt(Index start) => new Range(start, Index.FromEnd(0));
        public static Range EndAt(Index end) => new Range(Index.FromStart(0), end);
        public static Range All => new Range(Index.FromStart(0), Index.FromEnd(0));
        public override string ToString() => $"{Start}..{End}";
    }
    public static class RangeExtensions
    {
        public static (int Start, int Length) GetOffsetAndLength(this Range range, int arrayLength)
        {
            int start = range.Start.GetOffset(arrayLength);
            int end = range.End.GetOffset(arrayLength);
            return end < start
                ? throw new ArgumentOutOfRangeException(nameof(range), "End must not be less than start.")
                : (start, end - start);
        }
    }
}
