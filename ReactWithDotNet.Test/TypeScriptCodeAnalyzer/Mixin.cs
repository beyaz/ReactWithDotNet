using System.IO;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

static class Mixin
{
    public static string RemoveFromEnd(this string data, string value)
    {
        return data.RemoveFromEnd(value, StringComparison.OrdinalIgnoreCase);
    }

    public static string RemoveFromEnd(this string data, string value, StringComparison comparison)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }

    public static string RemoveFromStart(this string data, string value)
    {
        return data.RemoveFromStart(value, StringComparison.OrdinalIgnoreCase);
    }

    public static string RemoveFromStart(this string data, string value, StringComparison comparison)
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

    public static void WriteAllText(string path, string contents)
    {
        var directoryName = Path.GetDirectoryName(path);
        if (directoryName != null)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        File.WriteAllText(path, contents);
    }

    public static void WriteLines(this IReadOnlyList<string> lines, Action<string> appendLine)
    {
        const string padding = "    ";

        var currentPadding = string.Empty;

        foreach (var line in lines)
        {
            if (line == "{")
            {
                appendLine(currentPadding + line);
                currentPadding += padding;
                continue;
            }

            if (line == "}")
            {
                currentPadding = currentPadding.RemoveFromEnd(padding);
                appendLine(currentPadding + line);
                continue;
            }

            appendLine(currentPadding + line);
        }
    }
}