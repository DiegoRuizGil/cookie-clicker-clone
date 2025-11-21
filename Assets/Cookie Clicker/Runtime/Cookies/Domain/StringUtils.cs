namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public static class StringUtils
    {
        private static readonly string[] Suffixes = { "", "thousand", "million", "billion", "trillion", "quadrillion" };
        
        public static string FormatNumber(double value, bool withDecimals = false)
        {
            if (value < 1_000_000)
                return withDecimals ? value.ToString("N1") : value.ToString("N0");

            var suffixIndex = 0;
            while (value >= 1_000 && suffixIndex < Suffixes.Length - 1)
            {
                value /= 1_000;
                suffixIndex++;
            }

            return $"{value:N3} {Suffixes[suffixIndex]}";
        }
    }
}