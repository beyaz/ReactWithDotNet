namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

record TsProperty(string comment, string propertyName, string propertyType);

class TsTypeReference
{
    public IReadOnlyList<Token> GenericArgumentsAsTokenList { get; set; }
    public string Name { get; set; }
}

class TsMemberInfo
{
    public string Comment { get; set; }
    public bool IsNullable { get; set; }
    public string Name { get; set; }
    public TsTypeReference PropertyType { get; set; }
}

static class TsParser
{
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

    public static (bool hasRead, string value, int newIndex) TryReadAlfaNumericOrDotSeparetedAlfanumeric(IReadOnlyList<Token> tokens, int startIndex)
    {
        var (hasRead, readValues, newIndex) = TryReadWhile(tokens, startIndex, x => x.tokenType == TokenType.AlfaNumeric || x.tokenType == TokenType.Dot);

        return (hasRead, ToString(readValues), newIndex);
    }

    public static (bool hasRead, TsMemberInfo memberInfo, int newIndex) TryReadMemberInfo(IReadOnlyList<Token> tokens, int startIndex)
    {
        var memberInfo = new TsMemberInfo();

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

                skipSpaces();

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

    static bool Equals(Token a, Token b) => a.tokenType == b.tokenType && a.value == b.value;

    static string ToString(this IReadOnlyList<Token> tokens)
    {
        return string.Join(string.Empty, tokens.Select(t => t.value));
    }

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
}