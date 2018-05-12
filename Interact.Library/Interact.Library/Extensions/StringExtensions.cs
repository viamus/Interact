namespace Interact.Library
{
    public static class StringExtensions
    {
        public static bool NullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }
    }
}
