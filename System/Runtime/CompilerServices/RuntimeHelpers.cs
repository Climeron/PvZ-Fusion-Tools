namespace System.Runtime.CompilerServices
{
    public static class RuntimeHelpers
    {
        public static T[] GetSubArray<T>(T[] array, Range range)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            (int start, int length) = range.GetOffsetAndLength(array.Length);
            T[] result = new T[length];
            Array.Copy(array, start, result, 0, length);
            return result;
        }
    }
}
