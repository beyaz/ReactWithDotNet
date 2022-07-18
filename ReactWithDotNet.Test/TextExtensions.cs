using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FluentAssertions;

namespace ReactWithDotNet;

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

    public static void ShouldBe(this IReadOnlyDictionary<string, object> map, string testFileName)
    {
        var json = JsonSerializer.Serialize(map, new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = true });
        json.Should().Be(File.ReadAllText(testFileName));
    }
}