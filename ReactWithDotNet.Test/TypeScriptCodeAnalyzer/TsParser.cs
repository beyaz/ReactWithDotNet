using System.Diagnostics;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

record TsProperty(string comment, string propertyName, string propertyType);

class TsTypeReference
{
    public string Name { get; set; }
    public IReadOnlyList<Token> GenericArgumentsAsTokenList { get; set; }
}

class TsMemberInfo
{
    public string Comment { get; set; }
    public string Name { get; set; }
    public TsTypeReference PropertyType { get; set; }
    public bool IsNullable { get; set; }
}

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
    Assign
}

[DebuggerDisplay("{value}")]
record Token(int startIndex, int endIndex, TokenType tokenType, string value);


static class TsParser
{
    static (bool hasRead, IReadOnlyList<Token> readValues, int newIndex) TryReadWhile(IReadOnlyList<Token> tokens, int startIndex, Func<Token, bool> isOk)
    {
        var readValues = new List<Token>();

        var hasRead = false;

        var i = startIndex;

        while (tokens.Count > i && isOk(tokens[i]))
        {
            hasRead = true;

            readValues.Add(tokens[i]);

            i++;
        }

        return (hasRead, readValues, i);
    }

    public static (bool hasRead, IReadOnlyList<TsMemberInfo> members, int newIndex) TryReadMembers(IReadOnlyList<Token> tokens, int startIndex)
    {
        var i = startIndex;

        var members = new List<TsMemberInfo>();

        if (tokens[i].tokenType == TokenType.LeftBrace)
        {
            var (isFound, indexOfPair) = FindPair(tokens, i, x => x.tokenType == TokenType.RightBrace);
            if (isFound)
            {
                i++;

                skipSpaces();

                while (true)
                {
                    var (hasRead, memberInfo, newIndex) = TryReadMemberInfo(tokens, i);
                    if (hasRead)
                    {
                        members.Add(memberInfo);

                        i = newIndex;

                        continue;
                    }
                    break;
                }

                if (i == indexOfPair)
                {
                    i++;

                    return (members.Count > 0, members, i);
                }
            }
        }

        return /*None*/(hasRead: false, null, -1);

        void skipSpaces()
        {
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }
    }

    public static (bool hasRead, TsMemberInfo memberInfo, int newIndex) TryReadMemberInfo(IReadOnlyList<Token> tokens, int startIndex)
    {
        TsMemberInfo memberInfo = new TsMemberInfo();


        var i = startIndex;

        skipSpaces();

        if (tokens[i].tokenType == TokenType.Comment)
        {
            memberInfo.Comment = tokens[i].value;

            i++;
        }

        skipSpaces();

        if (tokens[i].tokenType == TokenType.AlfaNumeric)
        {
            memberInfo.Name = tokens[i].value;

            i++;

            if (tokens[i].tokenType == TokenType.QuestionMark)
            {
                memberInfo.IsNullable = true;

                i++;
            }

            skipSpaces();

            if (tokens[i].tokenType == TokenType.Colon)
            {
                i++;

                skipSpaces();

                var (hasRead, tsTypeReference, newIndex) = TryReadTypeReference(tokens, i);
                if (hasRead)
                {
                    memberInfo.PropertyType = tsTypeReference;

                    i = newIndex;

                    skipSpaces();

                    if (tokens.Count > i && tokens[i].tokenType == TokenType.SemiColon)
                    {
                        i++;
                    }

                    return (hasRead: true, memberInfo, i);
                }
            }
        }


        return (false, null, -1);

        void skipSpaces()
        {
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }
    }

    public static (bool hasRead, TsTypeReference tsTypeReference, int newIndex) TryReadTypeReference(IReadOnlyList<Token> tokens, int startIndex)
    {
        var i = startIndex;



        var (hasRead, value, newIndex) = TryReadAlfaNumericOrDotSeparetedAlfanumeric(tokens, i);
        if (hasRead)
        {
            i = newIndex;

            skipSpaces();

            // Partial<....>;
            if (tokens[i].tokenType == TokenType.LessThan)
            {
                var (isFound, indexOfPair) = FindPair(tokens, i, x => x.tokenType == TokenType.GreaterThan);
                if (isFound)
                {
                    var tsTypeReference = new TsTypeReference
                    {
                        Name = value,

                        GenericArgumentsAsTokenList = tokens.Take(new Range(i + 1, indexOfPair)).ToList()
                    };

                    return (true, tsTypeReference, indexOfPair + 1);
                }
            }

            if (tokens[i].tokenType == TokenType.SemiColon)
            {
                var tsTypeReference = new TsTypeReference
                {
                    Name = value
                };

                return (true, tsTypeReference, i + 1);
            }

        }

        return (false, null, -1);

        void skipSpaces()
        {
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }
    }

    static string ToString(this IReadOnlyList<Token> tokens)
    {
        return string.Join(string.Empty, tokens.Select(t => t.value));
    }

    public static (bool hasRead, string value, int newIndex) TryReadAlfaNumericOrDotSeparetedAlfanumeric(IReadOnlyList<Token> tokens, int startIndex)
    {

        var (hasRead, readValues, newIndex) = TryReadWhile(tokens, startIndex, x => x.tokenType == TokenType.AlfaNumeric || x.tokenType == TokenType.Dot);

        return (hasRead, ToString(readValues), newIndex);


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

    static bool Equals(Token a, Token b) => a.tokenType == b.tokenType && a.value == b.value;

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

                if (!Equals(tokens[j], searchToken))
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
            var (exception, hasRead, endIndex, value) = TryReadComment(content, startIndex);
            if (exception is not null)
            {
                return (exception, hasRead: false, -1, null);
            }
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
            var specialCharachters = "|<>,?:;{}.*&()[]=";

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
                    _ => null
                };

                if (tokenType == null)
                {
                    return (exception: new Exception($"Token not recognized. @value:{value}"), hasRead: false, -1, null);
                }

                return (exception: null, hasRead: true, endIndex, new Token(startIndex, endIndex, tokenType.Value, value.ToString()));
            }
        }

        {
            var (hasRead, endIndex, value) = TryReadWhile(content, startIndex, char.IsLetterOrDigit);
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

    static (Exception exception, bool hasRead, int cursor, string comment) TryReadComment(string content, int cursor)
    {

        if (content.Length <= cursor + 2)
        {
            return (exception: null, hasRead: false, -1, null);
        }

        if (content.Substring(cursor, 2) == "/*")
        {
            var endIndex = content.IndexOf("*/", cursor + 2, StringComparison.OrdinalIgnoreCase);
            if (endIndex > 0)
            {
                endIndex += 2;

                return (exception: null, hasRead: true, endIndex, content.Substring(cursor, endIndex - cursor));
            }
        }

        if (content.Substring(cursor, 2) == "//")
        {
            var endIndex = content.IndexOf('\n', cursor + 2);
            if (endIndex > 0)
            {
                endIndex += 2;

                return (exception: null, hasRead: true, endIndex, content.Substring(cursor, endIndex - cursor));
            }
        }

        return (exception: null, hasRead: false, -1, null);
    }

    static (bool hasRead, int cursor, string value) HasMatch(string content, int startIndex, string value)
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

    static (bool hasRead, int cursor, string value) TryReadSpaces(string content, int cursor)
    {
        return TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ');
    }

    static (bool hasRead, int cursor, string value) TryReadWhile(string content, int cursor, Func<char, bool> isOk)
    {
        var hasRead = false;

        var value = "";

        while (content.Length > cursor && isOk(content[cursor]))
        {
            hasRead = true;

            value += content[cursor];

            cursor++;
        }

        return (hasRead, cursor, value);
    }

    static (bool hasRead, int cursor, char value) TryRead(string content, int cursor, Func<char, bool> isOk)
    {
        if (content.Length > cursor && isOk(content[cursor]))
        {
            return (hasRead: true, cursor + 1, content[cursor]);
        }

        return (hasRead: false, cursor + 1, content[cursor]);
    }
}