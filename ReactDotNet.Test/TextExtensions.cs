using FluentAssertions;

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

    public static void ShouldBeSameAs(this string a, string b)
    {
        Clear(a).Should().Be(Clear(b));
    }
}