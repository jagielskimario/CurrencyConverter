namespace CurrencyService.Extensions
{
    public static class ArrayExtensions
    {
        public static void Deconstruct(this string[] array, out string first, out string second)
        {
            first = array.Length > 0 ? array[0] : string.Empty;
            second = array.Length > 1 ? array[1] : string.Empty;
        }
    }
}
