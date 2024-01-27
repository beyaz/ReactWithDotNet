using System.Text.RegularExpressions;

namespace ReactWithDotNet.WebSite.HelperApps;

static class TextTransformer
{
    public static string Transform(string text)

    {
        var lines = text.Split(Environment.NewLine.ToCharArray()).ToList();

        var propertyNameList = new List<string>();

        foreach (var line in lines)

        {
            var propertyName = GetPropertyName(line);

            if (propertyName == null)

            {
                continue;
            }

            propertyNameList.Add(propertyName);
        }

        propertyNameList = propertyNameList.Distinct().ToList();

        var resultLines = propertyNameList.Select(x => $"{x} = source.{x}").ToList();

        return string.Join("," + Environment.NewLine, resultLines);
    }

    static string GetPropertyName(string line)
    {
        if (string.IsNullOrWhiteSpace(line))

        {
            return null;
        }

        line = Regex.Replace(line, @"\s+", " ").Trim();

        if (line.StartsWith("//") || line.StartsWith("#"))

        {
            return null;
        }

        var forbiddenStartsWith = new[]

        {
            "using ",

            "namespace ",

            "[",
            
            "class ",
            "public class ",
            "public sealed class ",
            "public static class ",
            "partial class "
        };

        if (forbiddenStartsWith.Any(x => line.StartsWith(x)))

        {
            return null;
        }

        if (line == "{" || line == "}" || line.Contains(" class "))

        {
            return null;
        }

        line = line.RemoveFromStart("public ", StringComparison.OrdinalIgnoreCase).Trim();
        
        line = line.RemoveFromStart("required ", StringComparison.OrdinalIgnoreCase).Trim();

        const string pattern = @"[A-Za-z\?\<\>,]* ([A-Za-z0-9_]*) .*";

        const string substitution = @"$1";

        var options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant;

        var regex = new Regex(pattern, options);

        line = regex.Replace(line, substitution);

        return line;
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
    static string RemoveFromStart(this string data, string value, StringComparison comparison)
    {
        if (data == null)
        {
            return null;
        }

        if (data.StartsWith(value, comparison))
        {
            return data.Substring(value.Length, data.Length - value.Length);
        }

        return data;
    }
}