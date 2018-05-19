namespace Interact.Library
{
    public static class IntExtensions
    {
        public static bool ValidRange(this int integer, int min, int max)
        {
            return integer >= min && integer <= max;
        }
    }
}
