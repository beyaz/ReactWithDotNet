namespace ReactDotNet;

    static class TextExtensions
    {
        public static string Clear(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Replace(" ", "").Replace("\n", "").Replace("\r", "").Trim();
        }

    }
