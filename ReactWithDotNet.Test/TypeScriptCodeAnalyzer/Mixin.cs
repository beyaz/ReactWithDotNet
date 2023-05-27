using System.IO;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

static class Mixin
{
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