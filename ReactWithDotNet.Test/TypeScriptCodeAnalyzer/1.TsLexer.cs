using System.Diagnostics;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

[DebuggerDisplay("{value}")]
record Token(int startIndex, int endIndex, TokenType tokenType, string value);

enum TokenType
{
    AlfaNumeric,
    Comment,
    Comma,
    LessThan,
    GreaterThan,
    Union,
    QuotedString,
    Space,
    QuestionMark,
    Colon,
    SemiColon,
    LeftBrace,
    RightBrace,
    Dot,
    Star,
    Ampersand,
    LeftParenthesis,
    RightParenthesis,
    LeftBracket,
    RightBracket,
    Lambda,
    Assign,
    Negative
}

static class TsLexer
{
    public static (Exception exception, bool hasRead, int endIndex, IReadOnlyList<Token> tokens) ParseTokens(string content, int startIndex)
    {
        var tokens = new List<Token>();

        var i = startIndex;

        var totalLength = content.Length;

        while (totalLength > i)
        {
            var (exception, hasRead, endIndex, token) = ReadNextToken(content, i);
            if (exception is not null)
            {
                return (exception, hasRead: false, startIndex, Enumerable.Empty<Token>().ToList());
            }

            if (hasRead)
            {
                i = endIndex;

                tokens.Add(token);

                continue;
            }

            return (exception: new Exception("Next token not recognized"), hasRead: false, -1, null);
        }

        return (exception: null, hasRead: true, i, tokens);
    }

    static (bool hasRead, int endIndex, string value) HasMatch(string content, int startIndex, string value)
    {
        var length = value.Length;

        if (content.Length > startIndex + length)
        {
            var substring = content.Substring(startIndex, length);
            if (substring == value)
            {
                return (hasRead: true, startIndex + length, value);
            }
        }

        return (hasRead: false, -1, null);
    }

    static (Exception exception, bool hasRead, int endIndex, Token token) ReadNextToken(string content, int startIndex)
    {
        // Space
        {
            var (hasRead, endIndex, value) = TryReadSpaces(content, startIndex);
            if (hasRead)
            {
                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, TokenType.Space, value));
            }
        }

        // Quoted String
        {
            var (hasRead, endIndex, value) = ReadQuotedString(content, startIndex);
            if (hasRead)
            {
                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, TokenType.QuotedString, value));
            }
        }

        // comment
        {
            var (hasRead, endIndex, value) = TryReadComment(content, startIndex);
            if (hasRead)
            {
                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, TokenType.Comment, value));
            }
        }

        // =>
        {
            var (hasRead, endIndex, value) = HasMatch(content, startIndex, "=>");
            if (hasRead)
            {
                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, TokenType.Lambda, value));
            }
        }

        // | < > , ? :
        {
            var specialCharachters = "|<>,?:;{}.*&()[]=-";

            var (hasRead, endIndex, value) = TryRead(content, startIndex, specialCharachters.Contains);
            if (hasRead)
            {
                TokenType? tokenType = value switch
                {
                    '|' => TokenType.Union,
                    '<' => TokenType.LessThan,
                    '>' => TokenType.GreaterThan,
                    '?' => TokenType.QuestionMark,
                    ':' => TokenType.Colon,
                    ';' => TokenType.SemiColon,
                    '{' => TokenType.LeftBrace,
                    '}' => TokenType.RightBrace,
                    '[' => TokenType.LeftBracket,
                    ']' => TokenType.RightBracket,
                    '.' => TokenType.Dot,
                    ',' => TokenType.Comma,
                    '*' => TokenType.Star,
                    '&' => TokenType.Ampersand,
                    '(' => TokenType.LeftParenthesis,
                    ')' => TokenType.RightParenthesis,
                    '=' => TokenType.Assign,
                    '-' => TokenType.Negative,
                    _   => null
                };

                if (tokenType == null)
                {
                    return (exception: new Exception($"Token not recognized. @value:{value}"), hasRead: false, -1, null);
                }

                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, tokenType.Value, value.ToString()));
            }
        }

        {
            var (hasRead, endIndex, value) = TryReadWhile(content, startIndex, c=>char.IsLetterOrDigit(c) || c=='_');
            if (hasRead)
            {
                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, TokenType.AlfaNumeric, value));
            }
        }

        return (exception: new Exception("Token not recognized"), hasRead: false, -1, null);
    }

    static (bool hasRead, int newIndex, string value) ReadQuotedString(string content, int startIndex)
    {
        var i = startIndex;

        if (isStringStartOrEnd(content[i]))
        {
            i++;

            var hasFinishFound = false;

            while (content.Length > i)
            {
                if (isStringStartOrEnd(content[i]))
                {
                    hasFinishFound = true;
                    break;
                }

                i++;
            }

            if (hasFinishFound)
            {
                return (true, i + 1, content.Substring(startIndex + 1, i - startIndex - 1));
            }
        }

        return (false, 0, null);

        static bool isStringStartOrEnd(char chr) => chr == '"' || chr == "'"[0];
    }

    static (bool hasRead, int endIndex, char value) TryRead(string content, int startIndex, Func<char, bool> isOk)
    {
        var i = startIndex;

        if (content.Length > i && isOk(content[i]))
        {
            return (hasRead: true, i + 1, content[i]);
        }

        return (hasRead: false, i + 1, content[i]);
    }

    static (bool hasRead, int endIndex, string comment) TryReadComment(string content, int startIndex)
    {
        var i = startIndex;

        if (content.Length <= i + 2)
        {
            return (hasRead: false, -1, null);
        }

        if (content.Substring(i, 2) == "/*")
        {
            var endIndex = content.IndexOf("*/", i + 2, StringComparison.OrdinalIgnoreCase);
            if (endIndex > 0)
            {
                endIndex += 2;

                return (hasRead: true, endIndex, content.Substring(i, endIndex - i));
            }
        }

        if (content.Substring(i, 2) == "//")
        {
            var endIndex = content.IndexOf('\n', i + 2);
            if (endIndex > 0)
            {
                endIndex += 2;

                return (hasRead: true, endIndex, content.Substring(i, endIndex - i));
            }
        }

        return (hasRead: false, -1, null);
    }

    static (bool hasRead, int endIndex, string value) TryReadSpaces(string content, int startIndex)
    {
        return TryReadWhile(content, startIndex, c => c == '\n' || c == '\r' || c == ' ');
    }

    static (bool hasRead, int endIndex, string value) TryReadWhile(string content, int startIndex, Func<char, bool> isOk)
    {
        var i = startIndex;

        var hasRead = false;

        var value = "";

        while (content.Length > i && isOk(content[i]))
        {
            hasRead = true;

            value += content[i];

            i++;
        }

        return (hasRead, i, value);
    }
}