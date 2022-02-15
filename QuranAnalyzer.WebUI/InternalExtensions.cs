namespace QuranAnalyzer.WebUI;

static class InternalExtensions
{
    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);
}