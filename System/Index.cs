namespace System
{
    /// <summary>Структура, реализующая механизм обращения к элементам коллекций по конечному индексу.<br/>
    /// Например, [-1] - последний элемент массива.</summary>
    public readonly struct Index
    {
        private readonly int _value;
        public int Value => _value < 0 ? ~_value : _value;
        public bool IsFromEnd => _value < 0;

        public Index(int value, bool fromEnd = false) => _value = fromEnd ? ~value : value;
        public static Index FromStart(int value) => new(value);
        public static Index FromEnd(int value) => new(~value);
        public int GetOffset(int length) => length < 0
                ? throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.")
                : IsFromEnd ? length - Value : Value;
        public override string ToString() => IsFromEnd ? $"^{Value}" : Value.ToString();
        public static implicit operator Index(int value) => FromStart(value);
    }
}