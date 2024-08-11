using System.IO;
using System.Text;
using ReactWithDotNet.Tokenizing;

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

    public static bool StartsWith(this IReadOnlyList<Token> tokens, int startIndex, string value)
    {
        if (tokens is null || value is null)
        {
            return false;
        }

        if (tokens.Count <= 1)
        {
            return false;
        }
        
        var reactNode = Lexer.ParseTokens(value, 0);
        if (!reactNode.hasRead)
        {
            return false;
        }

        var valueAsTokens = reactNode.tokens;
        
        if (Lexer.FindMatch(tokens, startIndex, valueAsTokens).isFound)
        {
            return true;
        }

        return false;
    }

    public static bool FullMatch(this IReadOnlyList<Token> tokens, string value)
    {
        if (tokens is null || value is null)
        {
            return false;
        }

        tokens = tokens.Where(IsNotSpace).ToList();
        if (tokens.Count > 1)
        {
            var reactNode = Lexer.ParseTokens(value, 0);
            if (reactNode.hasRead)
            {
                var valueAsTokens = reactNode.tokens.Where(IsNotSpace).ToList();

                if (valueAsTokens.Count == tokens.Count)
                {
                    for (var i = 0; i < valueAsTokens.Count; i++)
                    {
                        if (!Lexer.IsEquals(valueAsTokens[i], tokens[tokens.Count - valueAsTokens.Count + i]))
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

    public static (bool success, Token, int tokenIndex) ReadToken(IReadOnlyList<Token> tokens, int index)
    {
        if (tokens is null || index >= tokens.Count ||  index < 0)
        {
            return default;
        }
        
        while ( index < tokens.Count && tokens[index].tokenType == TokenType.Space)
        {
            index++;
        }
        
        if (index >= tokens.Count)
        {
            return default;
        }

        return (true, tokens[index], index);
    }

    public static bool IsEquals(IReadOnlyList<Token> tokensA, int startIndexA, int endIndexA, IReadOnlyList<Token> tokensB, int startIndexB, int endIndexB )
    {
        if (tokensA is null || startIndexA >= tokensA.Count || endIndexA > tokensA.Count || startIndexA < 0)
        {
            return false;
        }
        
        if (tokensB is null || startIndexB >= tokensA.Count || endIndexB > tokensB.Count || startIndexB < 0)
        {
            return false;
        }


        while (true)
        {
            if (startIndexA == endIndexA+1 && startIndexB == endIndexB+1)
            {
                return true;
            }
            
            var (hasReadA, tokenA, tokenIndexA) = ReadToken(tokensA,startIndexA);
            if (hasReadA && tokenIndexA <= endIndexA)
            {
                startIndexA = tokenIndexA+1;
                
                var (hasReadB, tokenB, tokenIndexB) = ReadToken(tokensB,startIndexB);
                if (hasReadB && tokenIndexB <= endIndexB)
                {
                    startIndexB = tokenIndexB+1;
                    
                    if (Lexer.IsEquals(tokenA, tokenB))
                    {
                        continue;
                    }
                    
                    return false;
                }
            }

            if (hasReadA is false)
            {
                var (hasReadB, _, _) = ReadToken(tokensB,startIndexB);
                if (hasReadB is false)
                {
                    return true;
                }
            }
            
            return false;
            
        }

    }
    

    public static bool FullMatch(this IReadOnlyList<Token> tokens, params TokenMatch[] matchSteps)
    {
        tokens ??= new List<Token>();
        if (tokens.Count == matchSteps.Length)
        {
            for (var i = 0; i < matchSteps.Length; i++)
            {
                if (!matchSteps[i].IsMatch(tokens[tokens.Count - matchSteps.Length + i]))
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
            var reactNode = Lexer.ParseTokens(value, 0);
            if (reactNode.hasRead)
            {
                var valueAsTokens = reactNode.tokens.Where(IsNotSpace).Where(IsNotColon).ToList();

                if (valueAsTokens.Count <= tokens.Count)
                {
                    for (var i = 0; i < valueAsTokens.Count; i++)
                    {
                        if (!Lexer.IsEquals(valueAsTokens[i], tokens[tokens.Count - valueAsTokens.Count + i]))
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

sealed class TokenMatch
{
    Func<Token, bool> MatchFunc { get; init; }
    
    public static implicit operator TokenMatch(Func<Token, bool> matchFunc)
    {
        return new TokenMatch { MatchFunc = matchFunc };
    }

    public static implicit operator TokenMatch(string value)
    {
        return new TokenMatch { MatchFunc = t=>t.value == value };
    }
    
    public bool IsMatch(Token token)
    {
        return MatchFunc(token);
    }

    public static TokenMatch OnTokenMatched(Action<Token> onTrue)
    {
        return new TokenMatch { MatchFunc = matchFunc };

        bool matchFunc(Token token)
        {
            onTrue(token);
            return true;
        }
    }
}