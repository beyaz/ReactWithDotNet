using System;

namespace ReactWithDotNet.UIDesigner;

static class Extensions
{
    public static (T value, Exception exception) Try<T>(Func<T> func)
    {
        try
        {
            return (func(), null);
        }
        catch (Exception exception)
        {
            return (default, exception);
        }
    }

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

    public static string GetSvgUrl(string svgFileName) => $"wwwroot/integration/ReactWithDotNet-UIDesigner/{svgFileName}.svg";
}