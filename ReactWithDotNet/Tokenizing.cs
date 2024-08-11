using System.Collections.Generic;
using System.Diagnostics;

namespace ReactWithDotNet.Tokenizing;

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

static class Lexer
{
    public static string ToString(IReadOnlyList<Token> tokens)
    {
        return string.Join(string.Empty, tokens.Select(t => t.value));
    }
    
    public static string ToString(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
    {
        if (startIndex == endIndex)
        {
            return tokenToString(tokens[startIndex]);
        }
        return string.Join(string.Empty, tokens.Skip(startIndex).Take(endIndex - startIndex + 1).Select(tokenToString));

        static string tokenToString(Token token)
        {
            if (token.tokenType == TokenType.QuotedString)
            {
                return '"' + token.value + '"';
            }

            return token.value;
        }
    }
    
    public static (bool isFound, int indexOfPair) FindPair(IReadOnlyList<Token> tokens, int startIndex, Func<Token, bool> isPair)
    {
        var i = startIndex;

        var stack = new Stack<Token>();

        stack.Push(tokens[i]);

        i++;
        while (tokens.Count > i)
        {
            if (tokens[i].tokenType == tokens[startIndex].tokenType)
            {
                stack.Push(tokens[i]);
            }

            if (isPair(tokens[i]))
            {
                stack.Pop();
                if (stack.Count == 0)
                {
                    return (true, i);
                }
            }

            i++;
        }

        return (false, -1);
    }
    
    public static (bool isFound, int indexOfLastMatchedToken) FindMatch(IReadOnlyList<Token> tokens, int startIndex, IReadOnlyList<Token> searchTokens)
    {
        var i = startIndex;

        while (tokens.Count > i)
        {
            var isFound = true;

            var j = i;

            foreach (var searchToken in searchTokens)
            {
                if (searchToken.tokenType == TokenType.Space)
                {
                    continue;
                }

                while (tokens.Count > j && tokens[j].tokenType == TokenType.Space)
                {
                    j++;
                }

                if (tokens.Count == j)
                {
                    break;
                }

                if (!IsEquals(tokens[j], searchToken))
                {
                    isFound = false;
                    break;
                }

                j++;
            }

            if (isFound)
            {
                return (isFound: true, j - 1);
            }

            i++;
        }

        return (false, -1);
    }
    
    public static bool IsEquals(Token a, Token b) => a.tokenType == b.tokenType && a.value == b.value;
    
    public static (bool hasRead, int endIndex, IReadOnlyList<Token> tokens) ParseTokens(string content, int startIndex)
    {
        var tokens = new List<Token>();

        var i = startIndex;

        var totalLength = content.Length;

        while (totalLength > i)
        {
            var (hasRead, endIndex, token) = ReadNextToken(content, i);
            if (hasRead)
            {
                i = endIndex;

                tokens.Add(token);

                continue;
            }

            return (hasRead: false, default, default);
        }

        return (hasRead: true, i, tokens);
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

    static (bool hasRead, int endIndex, Token token) ReadNextToken(string content, int startIndex)
    {
        // Space
        {
            var (hasRead, endIndex, value) = TryReadSpaces(content, startIndex);
            if (hasRead)
            {
                return (hasRead: true, endIndex, new(startIndex, endIndex, TokenType.Space, value));
            }
        }

        // Quoted String
        {
            var (hasRead, endIndex, value) = ReadQuotedString(content, startIndex);
            if (hasRead)
            {
                return (hasRead: true, endIndex, new(startIndex, endIndex, TokenType.QuotedString, value));
            }
        }

        // comment
        {
            var (hasRead, endIndex, value) = TryReadComment(content, startIndex);
            if (hasRead)
            {
                return (hasRead: true, endIndex, new(startIndex, endIndex, TokenType.Comment, value));
            }
        }

        // =>
        {
            var (hasRead, endIndex, value) = HasMatch(content, startIndex, "=>");
            if (hasRead)
            {
                return (hasRead: true, endIndex, new(startIndex, endIndex, TokenType.Lambda, value));
            }
        }

        // | < > , ? :
        {
            const string specialCharachters = "|<>,?:;{}.*&()[]=-";

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

                if (tokenType != null)
                {
                    return (hasRead: true, endIndex, new(startIndex, endIndex, tokenType.Value, value.ToString()));
                }
            }
        }

        {
            var (hasRead, endIndex, value) = TryReadWhile(content, startIndex, c => char.IsLetterOrDigit(c) || c == '_');
            if (hasRead)
            {
                return (hasRead: true, endIndex, new(startIndex, endIndex, TokenType.AlfaNumeric, value));
            }
        }

        return (hasRead: false, default, default);
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

        static bool isStringStartOrEnd(char chr)
        {
            return chr == '"' || chr == "'"[0];
        }
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