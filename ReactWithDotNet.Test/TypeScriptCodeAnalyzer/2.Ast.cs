using ReactWithDotNet.Exporting;
using ReactWithDotNet.Tokenizing;
using static ReactWithDotNet.Tokenizing.Lexer;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

class TsTypeReference
{
    public IReadOnlyList<Token> TokenListAsGenericArguments { get; set; }
    public string Name { get; set; }
    public IReadOnlyList<Token> TokenListAsUnionValues { get; set; }
    public IReadOnlyList<Token> TokenListAsObjectMap { get; set; }

    public bool IsUnionType { get; set; }
    public IReadOnlyList<TsTypeReference> UnionTypes { get; set; }
    public bool IsSimpleNamedType { get; set; }
    public bool IsStringValue { get; set; }
    public string StringValue { get; set; }

    public bool IsGeneric { get; set; }
    public IReadOnlyList<TsTypeReference> GenericArguments { get; set; }
    public IReadOnlyList<Token> Tokens { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}

sealed class TsMethodParameterInfo
{
    public TsTypeReference TypeReference { get; init; }
    public string ParameterName { get; init; }
}

class TsMemberInfo
{
    public string Comment { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<Token> RemainingPart { get; init; }
}

static class Ast
{
    public static Response<IReadOnlyList<IReadOnlyList<Token>>> ParseToMemberTokens(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
    {
        var returnValue = new List<IReadOnlyList<Token>>();

        var tokenList = tokens.ToList();

        var i = startIndex;
        var j = startIndex;

        while (j < endIndex)
        {
            // o b j e c t
            if (tokens[j].tokenType == TokenType.LeftBrace)
            {
                var (isFound, indexOfPair) = FindPair(tokens, j, x => x.tokenType == TokenType.RightBrace);
                if (!isFound)
                {
                    return Fail("Left brace pair nor found.");
                }

                j = indexOfPair + 1;
                continue;
            }

            if (tokens[j].tokenType == TokenType.SemiColon)
            {
                returnValue.Add(tokenList.GetRange(i, j - i));

                j++;

                i = j;
                continue;
            }

            j++;
        }


        return returnValue;
    }
    
    

  

    public static (bool hasRead, string value, int newIndex) TryReadAlfaNumericOrDotSeparetedAlfanumeric(IReadOnlyList<Token> tokens, int startIndex)
    {
        var (hasRead, readValues, newIndex) = TryReadWhile(tokens, startIndex, x => x.tokenType == TokenType.AlfaNumeric || x.tokenType == TokenType.Dot);

        return (hasRead, Lexer.ToString(readValues), newIndex);
    }



    public static (bool hasRead, IReadOnlyList<TsMemberInfo> members, int newIndex) TryReadMembers(IReadOnlyList<Token> tokens, int startIndex)
    {
        var i = startIndex;


        if (tokens[i].tokenType == TokenType.LeftBrace)
        {
            var (isFound, indexOfPair) = FindPair(tokens, i, x => x.tokenType == TokenType.RightBrace);
            if (isFound)
            {
                var response = ParseToMemberTokens(tokens, i+1, indexOfPair-1);
                if (response.IsSuccess)
                {
                    return (true, response.Select(Exporter.ParseMemberTokens).Value, indexOfPair + 1);
                }
              
            }
        }

        return default;
    }


    public static (bool hasRead, TsTypeReference tsTypeReference, int newIndex) TryReadOnlyOneTypeReference(IReadOnlyList<Token> tokens, int startIndex)
    {
        var i = startIndex;

        skipSpaces();

        if (i >= tokens.Count || tokens[i].tokenType == TokenType.QuotedString)
        {
            var tsTypeReference = new TsTypeReference
            {
                StringValue = tokens[i].value,
                IsStringValue = true,
                
                Tokens     = tokens,
                StartIndex = startIndex,
                EndIndex   = i-1
            };

            return (true, tsTypeReference, i+1);
        }
        
        var (hasRead, name, newIndex) = TryReadAlfaNumericOrDotSeparetedAlfanumeric(tokens, i);
        if (hasRead)
        {
            i = newIndex;

            skipSpaces();

            if (i >= tokens.Count || tokens[i].tokenType == TokenType.Union || tokens[i].tokenType == TokenType.SemiColon|| tokens[i].tokenType == TokenType.RightParenthesis)
            {
                var tsTypeReference = new TsTypeReference
                {
                    Name = name,
                    IsSimpleNamedType =  true,
                    Tokens = tokens,
                    StartIndex = startIndex,
                    EndIndex = i-1
                };

                return (true, tsTypeReference, i);
            }
        }
        

        return (false, null, -1);

        void skipSpaces()
        {
            if (i >= tokens.Count)
            {
                return;
            }
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }
    }

    public static (bool hasRead, TsTypeReference tsTypeReference, int newIndex) 
        TryReadUnionTypeReference(IReadOnlyList<Token> tokens, int startIndex)
    {
        var i = startIndex;

        skipSpaces();

        if (tokens[i].tokenType == TokenType.Union)
        {
            i++;
            skipSpaces();
        }

        var unionTypes = new List<TsTypeReference>();

        

        while (true)
        {
            
            var readResponse = TryReadOnlyOneTypeReference(tokens, i);
            if (readResponse.hasRead == false)
            {
                break;
            }

            unionTypes.Add(readResponse.tsTypeReference);

            i = readResponse.newIndex;

            skipSpaces();

            if (i < tokens.Count && tokens[i].tokenType == TokenType.Union)
            {
                i++;
                skipSpaces();
                continue;
            }

            break;
        }

        if (unionTypes.Count==0)
        {
            return (false, null, -1);
        }

        if (unionTypes.Count == 1)
        {
            return (true, unionTypes[0], i);
        }

        var tsTypeReference = new TsTypeReference
        {
            IsUnionType = true,
            UnionTypes  = unionTypes,
            Tokens      = tokens,
            StartIndex  = startIndex,
            EndIndex    = i-1
        };

        return (true, tsTypeReference, i);


        void skipSpaces()
        {
            if (i >= tokens.Count)
            {
                return;
            }
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }
    }
    public static (bool hasRead, TsTypeReference tsTypeReference, int newIndex) TryReadTypeReference(IReadOnlyList<Token> tokens, int startIndex)
    {
        var i = startIndex;

        skipSpaces();

        // read as union type
        {
            var (hasRead, tsTypeReference, newIndex) = TryReadUnionTypeReference(tokens, i);
            if (hasRead)
            {
                return (true, tsTypeReference, newIndex);
            }
        }

        if (isGenericType())
        {
            skipSpaces();

            var (hasRead, name, newIndex) = TryReadAlfaNumericOrDotSeparetedAlfanumeric(tokens, i);
            if (hasRead)
            {
                i = newIndex;

                skipSpaces();

                if (tokens[i].tokenType == TokenType.LessThan)
                {
                    var (isFound, indexOfPair) = FindPair(tokens, i, x => x.tokenType == TokenType.GreaterThan);
                    if (isFound)
                    {
                        var readResponse = TryReadTypeReference(tokens.ToList().GetRange(i+1, indexOfPair-i-1), 0);
                        if (readResponse.hasRead)
                        {
                            var tsTypeReference = new TsTypeReference
                            {
                                Name = name,
                                IsGeneric = true,
                                GenericArguments = readResponse.tsTypeReference.UnionTypes
                            };

                            return (true, tsTypeReference, indexOfPair + 1);
                        }
                        
                    }
                }
            }
        }
        
       

        {
            string name;
            // named type
            {

                (var hasRead, name, var newIndex) = TryReadAlfaNumericOrDotSeparetedAlfanumeric(tokens, i);
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
                            if (tokens[indexOfPair + 1].tokenType == TokenType.LeftBracket)// [..]
                            {
                                var (isFound2, indexOfPair2) = FindPair(tokens, indexOfPair + 1, x => x.tokenType == TokenType.RightBracket);
                                if (isFound2)
                                {
                                    indexOfPair = indexOfPair2;
                                }
                            }

                            var tsTypeReference = new TsTypeReference
                            {
                                Name = name,

                                TokenListAsGenericArguments = tokens.Take(new Range(i + 1, indexOfPair)).ToList()
                            };

                            return (true, tsTypeReference, indexOfPair + 1);
                        }
                    }

                    // number | undefined;
                    if (tokens[i].tokenType == TokenType.Union)
                    {
                        var unionTypes = new List<TsTypeReference>
                        {
                            new ()
                            {
                                Name = name
                            }
                        };

                        var tsTypeReference = new TsTypeReference
                        {
                            IsUnionType = true,
                            UnionTypes = unionTypes
                        };

                        while (true)
                        {
                            var readResponse = TryReadOnlyOneTypeReference(tokens, i + 1);
                            if (readResponse.hasRead == false)
                            {
                                break;
                            }

                            unionTypes.Add(readResponse.tsTypeReference);

                            i = readResponse.newIndex;
                        }


                        return (true, tsTypeReference, i + 1);
                    }

                    if (tokens[i].tokenType == TokenType.SemiColon)
                    {
                        var tsTypeReference = new TsTypeReference
                        {
                            Name = name
                        };

                        return (true, tsTypeReference, i + 1);
                    }

                    {
                        var tsTypeReference = new TsTypeReference
                        {
                            Name       = name,
                            Tokens     = tokens,
                            StartIndex = startIndex,
                            EndIndex   = i-1
                        };

                        return (true, tsTypeReference, i);
                    }
                }

            }

            // o b j e c t
            if (tokens[i].tokenType == TokenType.LeftBrace)
            {
                var (isFound, indexOfPair) = FindPair(tokens, i, x => x.tokenType == TokenType.RightBrace);
                if (isFound)
                {
                    var tsTypeReference = new TsTypeReference
                    {
                        Name = name,

                        TokenListAsObjectMap = tokens.Take(new Range(i + 1, indexOfPair)).ToList()
                    };

                    return (true, tsTypeReference, indexOfPair + 1);
                }
            }
        }
       
       

        // union string sample |'left' | 'right'
        if (tokens[i].tokenType == TokenType.Union ||
            tokens[i].tokenType == TokenType.LeftBracket ||
            tokens[i].tokenType == TokenType.QuotedString)
        {
            var (hasRead, readValues, newIndex) = TryReadWhile(tokens, i, x => x.tokenType != TokenType.SemiColon);
            if (hasRead)
            {
                var tsTypeReference = new TsTypeReference
                {
                    TokenListAsUnionValues = readValues
                };

                return (true, tsTypeReference, newIndex + 1);
            }
        }

        return (false, null, -1);

        void skipSpaces()
        {
            if (i >= tokens.Count)
            {
                return;
            }
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }

        bool isGenericType()
        {
            var current = i;

            skipSpaces();

            var (hasRead, _, newIndex) = TryReadAlfaNumericOrDotSeparetedAlfanumeric(tokens, i);
            if (hasRead)
            {
                i = newIndex;

                skipSpaces();

                if (tokens[i].tokenType == TokenType.LessThan)
                {
                    i = current;
                    return true;
                }
            }

            i = current;

            return false;
        }
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

    public static Response<(IReadOnlyList<TsMethodParameterInfo> parameters, int newIndex)> TryReadFunctionParameters(IReadOnlyList<Token> tokens, int startIndex)
    {

        var parameters = new List<TsMethodParameterInfo>();
        
        
        var i = startIndex;

        skipSpaces();

        if (tokens[i].tokenType == TokenType.LeftParenthesis)
        {
            i++;
            skipSpaces();

            var allParametersReadSuccessfully = false;
            
            while (true)
            {
                if (tokens[i].tokenType == TokenType.RightParenthesis) // end of parameters
                {
                    i++;
                    allParametersReadSuccessfully = true;
                    break;
                }
                
                if (tokens[i].tokenType == TokenType.Comma)
                {
                    i++;
                }
                
                if (tokens[i].tokenType == TokenType.AlfaNumeric)
                {
                    var parameterName = tokens[i].value;
                    
                    i++;
                    skipSpaces();
                    
                    if (tokens[i].tokenType == TokenType.Colon)
                    {
                        i++;
                    }
                    skipSpaces();
                    
                    var (hasRead, tsTypeReference, newIndex) = TryReadTypeReference(tokens, i);
                    if (hasRead)
                    {
                        parameters.Add( new TsMethodParameterInfo{ TypeReference = tsTypeReference, ParameterName = parameterName});
                        i = newIndex;
                        continue;
                    }
                }
                
                break;
            }

            
            if (allParametersReadSuccessfully)
            {
                return (parameters, i);
            }
        }
        
        return None;
        
        void skipSpaces()
        {
            if (i >= tokens.Count)
            {
                return;
            }
            if (tokens[i].tokenType == TokenType.Space)
            {
                i++;
            }
        }
    }
}