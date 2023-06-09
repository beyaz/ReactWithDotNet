using System.IO;
using System.Text;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

static class Mixin
{
    public static bool IsNotSpace(Token t)
    {
        return t.tokenType != TokenType.Space;
    }

    public static bool IsNotColon(Token t)
    {
        return t.tokenType != TokenType.Colon;
    }

    public static bool StartsWith(this IReadOnlyList<Token> tokens, string value)
    {
        tokens = tokens?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count > 1)
        {
            var reactNode = TsLexer.ParseTokens(value, 0);
            if (reactNode.hasRead)
            {
                if (TsParser.FindMatch(tokens, 0, reactNode.tokens).isFound)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool FullMatch(this IReadOnlyList<Token> tokens, string value)
    {
        tokens = tokens?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count > 1)
        {
            var reactNode = TsLexer.ParseTokens(value, 0);
            if (reactNode.hasRead)
            {
                var valueAsTokens = reactNode.tokens.Where(IsNotSpace).Where(IsNotColon).ToList();

                if (valueAsTokens.Count == tokens.Count)
                {
                    for (var i = 0; i < valueAsTokens.Count; i++)
                    {
                        if (!TsParser.Equals(valueAsTokens[i], tokens[tokens.Count - valueAsTokens.Count + i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        return false;
    }

    public static bool FullMatch(this IReadOnlyList<Token> tokens, params Func<Token,bool>[] matchSteps)
    {
        tokens ??= new List<Token>();
        if (tokens.Count == matchSteps.Length)
        {
            for (var i = 0; i < matchSteps.Length; i++)
            {
                if (!matchSteps[i](tokens[tokens.Count - matchSteps.Length + i]))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public static bool EndsWith(this IReadOnlyList<Token> tokens, string value)
    {
        tokens = tokens?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count > 1)
        {
            var reactNode = TsLexer.ParseTokens(value, 0);
            if (reactNode.hasRead)
            {
                var valueAsTokens = reactNode.tokens.Where(IsNotSpace).Where(IsNotColon).ToList();

                if (valueAsTokens.Count <= tokens.Count)
                {
                    for (var i = 0; i < valueAsTokens.Count; i++)
                    {
                        if (!TsParser.Equals(valueAsTokens[i], tokens[tokens.Count - valueAsTokens.Count + i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        return false;
    }

    public static IEnumerable<string> AsCSharpComment(string tsComment)
    {
        if (tsComment is null)
        {
            return Enumerable.Empty<string>();
        }

        var lines = new List<string>();

        var commentLines = tsComment.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        lines.Add("/// <summary>");

        var isFirst = true;

        foreach (var commentLine in commentLines)
        {
            var line = commentLine.Trim()
                .Trim(Environment.NewLine.ToCharArray()).RemoveFromStart("/**").RemoveFromStart("/*").RemoveFromEnd("*/")
                .Trim().RemoveFromStart("* ")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Trim();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (isFirst)
            {
                isFirst = false;
            }
            else
            {
                lines.Add("///     <br/>");
            }

            if (line.Trim() == "*")
            {
                line = "<br/>";
            }

            lines.Add("///     " + line);
        }

        lines.Add("/// </summary>");

        return lines;
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

    public static T GetValue<T>(this (Exception exception, T value) tuple)
    {
        if (tuple.exception != null)
        {
            throw tuple.exception;
        }

        return tuple.value;
    }

    public static string ToCSharpCode(this IReadOnlyList<string> lines)
    {
        var sb = new StringBuilder();

        lines.WriteLines(x => sb.AppendLine(x));

        return sb.ToString();
    }
}