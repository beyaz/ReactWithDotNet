using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Xml;

namespace ReactWithDotNet;

record TsProperty(string comment, string propertyName, string propertyType);

class TsTypeReference
{
    public string Name { get; set; }
    public bool HasUnionValues { get; set; }
    public List<string> OptionalValues { get; set; }
    public bool IsAlfaNumeric { get; set; }
    public bool IsQuotedString { get; set; }
    public IReadOnlyList<TsTypeReference> GenericArguments { get; set; }
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
record Token(int startIndex, int endIndex, TokenType tokenType, string value);

static class TsLexer
{
    public static (bool isFound, int indexOfPair) FindPair(IReadOnlyList<Token> tokens, int startIndex, Func<Token,bool> isPair)
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
                return (isFound: true, j-1);
            }

            i++;
        }

        return (false, -1);
    }

    public static (Exception exception, bool hasRead, int endIndex, IReadOnlyList<Token> tokens) ParseTokens(string content, int startIndex)
    {
        var tokens = new List<Token>();

        var i = startIndex;

        var totalLength = content.Length;

        while (totalLength > i)
        {
            var (exception, hasRead, endIndex, token) = ReadNextToken(content,i);
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
            var (exception,hasRead, endIndex, value) = TryReadComment(content, startIndex);
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
                return (true, i+1, content.Substring(startIndex+1, i-startIndex-1));
            }
        }

        return (false, 0, null);

        static bool isStringStartOrEnd(char chr) => chr == '"' || chr == "'"[0];
    }

    static (Exception exception, bool hasRead, int cursor, string comment) TryReadComment(string content, int cursor)
    {

        if (content.Length <= cursor + 2)
        {
            return (exception: null, hasRead: false,-1,null);
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
            var substring = content.Substring(startIndex,length);
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

static  class TsParser
{
   
    

    static (bool hasRead, int newIndex, string value) ReadQuotedString(string content, int startIndex)
    {
        var i = startIndex;
        
        if (isStringStartOrEnd(content[i]))
        {
            i++;

            var charList = new List<char>();

            while (content.Length > i)
            {
                var c = content[i];
                if (char.IsLetterOrDigit(c))
                {
                    charList.Add(c);
                    i++;
                    continue;
                }
                break;
            }

            if (content.Length > i && isStringStartOrEnd(content[i]))
            {
                i++;
                if (charList.Count > 0)
                {
                    return (true,i, new string(charList.ToArray()));
                }
            }
        }

        return (false,0,null);

        static bool isStringStartOrEnd(char chr) => chr == '"' || chr == "'"[0];
    }

    static (bool hasRead, int newCursor, IReadOnlyList<TsTypeReference> tsTypeReferences) TryReadGenericTypeReferenceArguments(string content, int startIndex)
    {
        var hasRead = false;

        var tsTypeReferences = new List<TsTypeReference>();

        var i = startIndex;

        while (i < content.Length)
        {
            var readTypeReferenceOutput = TryReadTypeReference(content, i);
            if (readTypeReferenceOutput.hasRead)
            {
                hasRead = true;
                
                i = readTypeReferenceOutput.newCursor;

                tsTypeReferences.Add(readTypeReferenceOutput.tsTypeReference);
                continue;
            }
            break;
        }

        return (hasRead, i, tsTypeReferences);


    }

    static (bool hasRead, int newCursor, TsTypeReference tsTypeReference) TryFullReadTypeReference(string content, int startIndex)
    {
        
        var i = startIndex;

        var readTypeReferenceOutput = TryReadTypeReference(content, i);
        if (readTypeReferenceOutput.hasRead)
        {
            
            var typeReference = readTypeReferenceOutput.tsTypeReference;
            
            i = readTypeReferenceOutput.newCursor;

            readSpaces();

            if (content.Length > i &&  content[i] == '<')
            {
                typeReference.HasUnionValues = true;
                i++;

                var tryReadGenericTypeReferenceArgumentsOutput = TryReadGenericTypeReferenceArguments(content, i);
                if (tryReadGenericTypeReferenceArgumentsOutput.hasRead)
                {
                     i = tryReadGenericTypeReferenceArgumentsOutput.newCursor;

                     typeReference.GenericArguments = tryReadGenericTypeReferenceArgumentsOutput.tsTypeReferences;
                }
            }

            if (content[i] == '>')
            {
                i++;
            }

            return (hasRead:true, i, typeReference);
        }

       

        return (false, 0, null);

       


        void readSpaces()
        {
            var (hasRead, cursor, _) = TryReadWhile(content, i, c => c == ' ');
            if (hasRead)
            {
                i = cursor;
            }
        }




        (bool hasRead, int cursor, string value) readAlfanumeric() => TryReadWhile(content, i, char.IsLetterOrDigit);

    }
    static (bool hasRead, int newCursor, TsTypeReference tsTypeReference) TryReadTypeReference(string content, int startIndex)
    {
        var i = startIndex;

        readSpaces();

        var readAlfaNumericOutput = TryReadWhile(content, i, char.IsLetterOrDigit);
        if (readAlfaNumericOutput.hasRead)
        {
            return (true, readAlfaNumericOutput.cursor, new TsTypeReference { Name = readAlfaNumericOutput.value, IsAlfaNumeric = true });
        }

        var readQuotedString = ReadQuotedString(content, i);
        if (readQuotedString.hasRead)
        {
            return (true, readQuotedString.newIndex, new TsTypeReference { Name = readQuotedString.value, IsQuotedString = true });
        }
        
        return (false, startIndex, null);


        void readSpaces()
        {
            var (hasRead, cursor, _) = TryReadWhile(content, i, c => c == ' ');
            if (hasRead)
            {
                i = cursor;
            }
        }

    }

   

   

    /// <summary>
    /// https://primereact.org/avatar/
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static (int cursor, IReadOnlyList<TsProperty> property) TryReadProperties(string content, int cursor)
    {
        var properties = new List<TsProperty>();


        var hasRead = true;
        while (hasRead)
        {
            var (cursorNext, property) = TryReadProperty(content,cursor);

            hasRead = property is not null;

            cursor = cursorNext;
            if (hasRead)
            {
                properties.Add(property);
            }
        }

        

        return (cursor, properties);
    }

   

    static (int cursor, TsProperty property) TryReadProperty(string content, int cursor)
    {
        var cursorInitialValue = cursor;
        
        cursor = TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ').cursor;

        var (endIndexOfComment, comment) = TryReadComment(content, cursor);

        cursor = endIndexOfComment;

        cursor = TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ').cursor;


        var (endIndexOfPropertyName, propertyName)=TryReadUntil(content, cursor, ':');

        cursor = endIndexOfPropertyName;


        cursor = TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ').cursor;

        var (endIndexOfPropertyType, propertyType) = TryReadUntil(content, cursor, ';');

        if (propertyName == null)
        {
            return (cursorInitialValue, null);
        }
        
        return (endIndexOfPropertyType, new (comment, propertyName, propertyType));
    }

    static (bool hasRead, int cursor, string spaceValue) TryReadSpaces(string content, int cursor)
    {
        return TryReadWhile(content,cursor, c=>c==' ');
    }

    static (bool hasRead, int cursor, string value) TryReadWhile(string content, int cursor, Func<char,bool> isOk)
    {
        var hasRead = false;
        
        var value = "";

        while (content.Length > cursor &&  isOk(content[cursor]))
        {
            hasRead =  true;
            
            value   += content[cursor];
            
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

        return (hasRead: false, cursor+1, content[cursor]);
    }


    static (int cursor, string comment) TryReadComment(string content, int cursor)
    {
        if (content.Length <= cursor + 3)
        {
            return (cursor, null);
        }
        if (content.Substring(cursor,3) =="/**")
        {
            var endIndex = content.IndexOf("*/", cursor+3, StringComparison.OrdinalIgnoreCase);
            if (endIndex > 0)
            {
                endIndex += 2;
                
                return (endIndex, content.Substring(cursor, endIndex-cursor));
            }
        }

        return (cursor, null);
    }

    static (int cursor, string name) TryReadPropertyName(string content, int cursor)
    {
        return TryReadUntil(content, cursor, ':');
    }

    static (int cursor, string name) TryReadUntil(string content, int cursor, char finishChar)
    {
        var endIndex = content.IndexOf(finishChar,startIndex: cursor);
        if (endIndex > cursor)
        {
            return (endIndex + 1, content.Substring(cursor, endIndex - cursor));
        }

        return (cursor, null);
    }
}