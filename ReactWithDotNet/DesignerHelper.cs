using System.Globalization;
using System.Reflection;
using ReactWithDotNet.Tokenizing;
using ReactWithDotNet.UIDesigner;
using static ReactWithDotNet.Tokenizing.Lexer;

namespace ReactWithDotNet;

public sealed class DesignerCode : List<(IReadOnlyList<int> VisualLocation, IReadOnlyList<Modifier> Modifiers)>
{
    public void Add(IReadOnlyList<int> VisualLocation, IReadOnlyList<Modifier> Modifiers)
    {
        Add((VisualLocation, Modifiers));
    }
}

static class DesignerHelper
{
    public static void Override(Element component, Element rootNode)
    {
        var designer = component.Designer;
        if (designer == null)
        {
            return;
        }

        foreach (var record in designer)
        {
            apply(record.VisualLocation, record.Modifiers, rootNode);
        }

        return;

        static void apply(IReadOnlyList<int> VisualLocation, IReadOnlyList<Modifier> Modifiers, Element rootNode)
        {
            if (Modifiers is null)
            {
                return;
            }

            var targetNode = findNode(VisualLocation, rootNode);
            if (targetNode is null)
            {
                return;
            }

            foreach (var modifier in Modifiers)
            {
                ModifyHelper.ProcessModifier(targetNode, modifier);
            }

            return;

            static Element findNode(IReadOnlyList<int> VisualLocation, Element rootNode)
            {
                var node = rootNode;
                var i = 1;
                var len = VisualLocation.Count;

                while (i < len)
                {
                    var offset = VisualLocation[i];

                    if (node is null)
                    {
                        break;
                    }

                    node = node._children[offset];

                    i++;
                }

                if (i < len)
                {
                    return null;
                }

                return node;
            }
        }
    }

    public static (bool success, MethodInfo methodInfo, object[] methodParameters) ToModifier(Node node)
    {
        if (node.Name.HasValue())
        {
            if (node.Parameters is null || node.Parameters.Count is 0)
            {
                var propertyInfo = typeof(Mixin).GetProperty(node.Name);
                if (propertyInfo is not null)
                {
                    return (true, propertyInfo.GetMethod, []);
                }

                return default;
            }

            if (node.Parameters.All(isNumberOrStringNode))
            {
                var namedMethods = typeof(Mixin).GetMethods().Where(m => m.Name == node.Name && m.GetParameters().Length == node.Parameters.Count).ToList();

                for (var i = 0; i < node.Parameters.Count; i++)
                {
                    var parameterNode = node.Parameters[i];

                    namedMethods = namedMethods.Where(m => hasMatch(parameterNode, m.GetParameters()[i])).ToList();
                }

                if (namedMethods.Count == 1)
                {
                    var targetMethod = namedMethods[0];

                    var (success, parameters) = calculateParameters(targetMethod, node.Parameters);
                    if (success)
                    {
                        return (success: true, targetMethod, parameters);
                    }
                }
            }

            {
                var targetMethodInfo = typeof(Mixin).GetMethod(node.Name, [typeof(StyleModifier[])]);
                if (targetMethodInfo is not null)
                {
                    var (success, values) = node.Parameters.Select(ToModifier).Select(Compile).Fold();
                    if (success)
                    {
                        return (success: true, targetMethodInfo, values.Select(x => (object)x).ToArray());
                    }
                }
            }
        }

        return default;

        static (bool success, object[] parameters) calculateParameters(MethodInfo methodInfo, IReadOnlyList<Node> parameterNodes)
        {
            var parameterInfoList = methodInfo.GetParameters();

            var parameters = new List<object>();

            foreach (var parameterInfo in parameterInfoList)
            {
                var (success, value) = tryConvertToTargetType(parameterNodes[parameterInfo.Position], parameterInfo);
                if (!success)
                {
                    return default;
                }

                parameters.Add(value);
            }

            return (true, parameters.ToArray());

            static (bool success, object value) tryConvertToTargetType(Node node, ParameterInfo parameterInfo)
            {
                if (node.IsStringNode)
                {
                    return (true, node.StringValue);
                }

                if (node.IsDoubleNode)
                {
                    return (true, node.DoubleValue);
                }

                if (node.IsNumberNode)
                {
                    if (parameterInfo.ParameterType == typeof(double))
                    {
                        return (true, Convert.ToDouble(node.NumberValue));
                    }

                    if (parameterInfo.ParameterType == typeof(int))
                    {
                        return (true, Convert.ToInt32(node.NumberValue));
                    }
                }

                return default;
            }
        }

        static bool hasMatch(Node node, ParameterInfo parameterInfo)
        {
            if (node.IsStringNode)
            {
                if (parameterInfo.ParameterType == typeof(string))
                {
                    return true;
                }

                return false;
            }

            if (node.IsNumberNode || node.IsDoubleNode)
            {
                if (parameterInfo.ParameterType == typeof(byte) ||
                    parameterInfo.ParameterType == typeof(short) ||
                    parameterInfo.ParameterType == typeof(int) ||
                    parameterInfo.ParameterType == typeof(long) ||
                    parameterInfo.ParameterType == typeof(double))
                {
                    return true;
                }
            }

            return false;
        }

        static bool isNumberOrStringNode(Node node)
        {
            return node.IsNumberNode || node.IsDoubleNode || node.IsStringNode;
        }
    }

    public static (bool success, Node node, int endIndex) TryReadNode(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
    {
        var i = startIndex;

        var tokenAt0 = i + 0 < tokens.Count ? tokens[i + 0] : null;
        var tokenAt1 = i + 1 < tokens.Count ? tokens[i + 1] : null;
        var tokenAt2 = i + 2 < tokens.Count ? tokens[i + 2] : null;

        if (tokenAt0?.tokenType == TokenType.QuotedString)
        {
            return ok(endIndex: i, new()
            {
                IsStringNode = true,
                StringValue  = tokenAt0.value
            });
        }

        if (tokenAt0?.tokenType == TokenType.AlfaNumeric)
        {
            if (tokenAt1?.tokenType == TokenType.Dot)
            {
                return ok(endIndex: i + 2, new()
                {
                    IsDoubleNode = true,
                    DoubleValue  = double.Parse(tokenAt0.value + '.' + tokenAt2?.value)
                });
            }

            if (tokenAt0.value.All(char.IsNumber))
            {
                return ok(endIndex: i, new()
                {
                    IsNumberNode = true,
                    NumberValue  = long.Parse(tokenAt0.value)
                });
            }

            if (startIndex == endIndex)
            {
                return ok(endIndex: i, new()
                {
                    Name = tokenAt0.value
                });
            }

            if (tokenAt1?.tokenType == TokenType.Comma)
            {
                return ok(endIndex: i, new()
                {
                    Name = tokens[i].value
                });
            }

            if (tokenAt1?.tokenType == TokenType.LeftParenthesis)
            {
                var (isFound, indexOfPair) = FindPair(tokens, i + 1, x => x.tokenType == TokenType.RightParenthesis);
                if (isFound)
                {
                    var (success, parameterNodes, rightParenthesisIndex) = TryReadNodes(tokens, i + 2, indexOfPair - 1);
                    if (success)
                    {
                        if (rightParenthesisIndex == indexOfPair)
                        {
                            return ok(endIndex: indexOfPair, new()
                            {
                                Name       = tokens[i].value,
                                Parameters = parameterNodes
                            });
                        }
                    }
                }
            }
        }

        return default;

        static (bool success, Node node, int endIndex) ok(int endIndex, Node node)
        {
            return (true, node, endIndex);
        }
    }

    public static (bool success, IReadOnlyList<Node> nodes, int endIndex) TryReadNodes(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
    {
        var nodes = new List<Node>();

        var i = startIndex;

        while (i <= endIndex)
        {
            if (tokens[i].tokenType == TokenType.Comma)
            {
                i++;
                continue;
            }

            var (success, node, newIndex) = TryReadNode(tokens, i, endIndex);
            if (!success)
            {
                return (success: false, nodes, i);
            }

            i = newIndex + 1;

            nodes.Add(node);
        }

        return (success: true, nodes, i);
    }

    static (bool success, Modifier value) Compile((bool success, MethodInfo methodInfo, object[] methodParameters) response)
    {
        if (!response.success)
        {
            return default;
        }

        var value = (Modifier)response.methodInfo.Invoke(null, response.methodParameters);

        return (success: true, value);
    }

    static Result<B> Then<A,B>(this Maybe<A> maybe, Func<A,Result<B>> next)
    {
        if (maybe.IsNone)
        {
            return default(B);
        }

        return  next(maybe.Value);
    }
    static (bool success, IReadOnlyList<T> values) Fold<T>(this IEnumerable<(bool success, T value)> items)
    {
        var resultList = new List<T>();

        foreach (var (success, value) in items)
        {
            if (!success)
            {
                return default;
            }

            resultList.Add(value);
        }

        return (true, resultList);
    }

    public sealed class Node
    {
        public double DoubleValue { get; init; }
        public bool IsDoubleNode { get; init; }
        public bool IsNumberNode { get; init; }

        public bool IsStringNode { get; init; }

        public string Name { get; init; }
        public long NumberValue { get; init; }

        public IReadOnlyList<Node> Parameters { get; init; }

        public string StringValue { get; init; }

        public override string ToString()
        {
            if (IsStringNode)
            {
                return '"' + StringValue + '"';
            }

            if (IsDoubleNode)
            {
                return DoubleValue.ToString(CultureInfo.InvariantCulture);
            }

            if (IsNumberNode)
            {
                return NumberValue.ToString(CultureInfo.InvariantCulture);
            }

            if (Parameters?.Count > 0)
            {
                return $"{Name}({string.Join(", ", Parameters)})";
            }

            return Name;
        }
    }

    public static Maybe<IReadOnlyList<Token>> ReadDesignerCodeTokens(string classDefinitionCode)
    {
        string csharpCode;
        {
            const string startLine = "#region Designer Code [Do not edit manually]";

            const string endLine = "#endregion Designer Code [Do not edit manually]";

            var startIndex = classDefinitionCode.IndexOf(startLine, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return None;
            }

            var endIndex = classDefinitionCode.IndexOf(endLine, StringComparison.OrdinalIgnoreCase);
            if (endIndex < 0)
            {
                return None;
            }

            csharpCode = classDefinitionCode.Substring(startIndex, endIndex - startIndex);
        }

        // remove region
        csharpCode = string.Join(Environment.NewLine, csharpCode.Split(Environment.NewLine).Skip(1));

        {
            var (hasRead, _, tokens) = ParseTokens(csharpCode, 0);
            if (!hasRead)
            {
                return None;
            }

            var tokenList = tokens.Where(t => t.tokenType != TokenType.Space).ToList();


            return tokenList;
            

            
            

        }
        
        
    }

    public static Result<DesignerCode> ReadDesignerCode(string classDefinitionCode)
    {
        return ReadDesignerCodeTokens(classDefinitionCode).Then(ReadDesignerValueFromTokens);
    }
    
    public static Result<DesignerCode> ReadDesignerValueFromTokens(IReadOnlyList<Token> tokens)
    {
        var tokenList = tokens.Where(t => t.tokenType != TokenType.Space).ToList();

        var leftCurlyBracketIndex  = tokenList.FindIndex(x => x.tokenType == TokenType.LeftCurlyBracket);
        if (leftCurlyBracketIndex < 0)
        {
            return default;
        }

        var i = leftCurlyBracketIndex;

        // readLeftCurlyBracket
        {
            var response = ReadToken(tokens, i,TokenType.LeftCurlyBracket);
            if (!response.Success)
            {
                return response.ErrorMessage;
            }
        
            i = response.NewIndex;
        }
        
      

        while (i<tokens.Count)
        {
            var token = tokens[i];
            
            if (token.tokenType == TokenType.Comma)
            {
                i++;
                continue;
            }
            
            if (token.tokenType == TokenType.RightCurlyBracket)
            {
                break;
            }

            var entry = ReadElementEntry(tokens, i);
            if (!entry.Success)
            {
                
            }
        }
        
        return default;

        
        
        
        
        
        
        

        
        
        
    }
    
    internal static TokenReadResponse<long[]> ReadInt64Array(IReadOnlyList<Token> tokens, int i)
    {
        var response = ReadToken(tokens, i,TokenType.LeftSquareBracket);
        if (!response.Success)
        {
            return response.ErrorMessage;
        }
        
        i = response.NewIndex;

        var items = new List<long>();
        
        while (true)
        {
            if (tokens.Count <= i)
            {
                return "Expected ] charachter";
            }
            
            var token = tokens[i];
            if (token.tokenType == TokenType.RightSquareBracket)
            {
                return (i, items.ToArray());
            }

            if (token.tokenType == TokenType.Comma)
            {
                i++;
                continue;
            }

            var tokenReadResponse = ReadInt64(tokens, i);
            if (!tokenReadResponse.Success)
            {
                return tokenReadResponse.ErrorMessage;
            }
            
            items.Add(tokenReadResponse.Value);

            i++;
        }
    }
    
    internal static TokenReadResponse<long> ReadInt64(IReadOnlyList<Token> tokens, int i)
    {
        return ReadToken(tokens, i, TokenType.AlfaNumeric).Then(t => TryParseLong(t.value));
    }

    
    static Result<(long[] location, IReadOnlyList<Node> nodes, int newIndex)> ReadElementEntry(IReadOnlyList<Token> tokens, int i)
    {

        long[] location;
        IReadOnlyList<Node> nodes;
        
        // readLeftCurlyBracket
        {
            var response = ReadToken(tokens, i,TokenType.LeftCurlyBracket);
            if (!response.Success)
            {
                return nok(response.ErrorMessage);
            }
        
            i = response.NewIndex;
        }

        {
            var response = ReadInt64Array(tokens, i);
            if (!response.Success)
            {
                return nok(response.ErrorMessage);
            }
        
            i = response.NewIndex;

            location = response.Value;
        }
        
        {
            var response = ReadToken(tokens, i,TokenType.Comma);
            if (!response.Success)
            {
                return nok(response.ErrorMessage);
            }
        
            i = response.NewIndex;
        }
            
        {
            var response = ReadToken(tokens, i,TokenType.LeftSquareBracket);
            if (!response.Success)
            {
                return nok(response.ErrorMessage);
            }
        
            i = response.NewIndex;
            
            var (isFound, indexOfPair) = Lexer.FindPair(tokens, i-1, x => x.tokenType == TokenType.RightSquareBracket);
            if (!isFound)
            {
                return nok($"Close pair not found. At: {tokens[i-1].startIndex}");
            }

            var (success, nodeList, i1) = TryReadNodes(tokens, i, indexOfPair - 1);
            if (!success)
            {
                return nok("todo");
            }

            i = i1;

            nodes = nodeList;
        }

        {
            var response = ReadToken(tokens, i,TokenType.RightCurlyBracket);
            if (!response.Success)
            {
                return nok(response.ErrorMessage);
            }
        
            i = response.NewIndex;

            return (location, nodes, i);
        }
        
        

       
            
            
        static Result<(long[] location, IReadOnlyList<Node> nodes, int newIndex)> nok(string errorMessage)
        {
            return errorMessage;
        }
            
           
    }
    
    static Result<long> TryParseLong(string x) => Try(()=>long.Parse(x));

    static Result<T> Try<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }
    
    internal static TokenReadResponse ReadToken(IReadOnlyList<Token> tokens, int i, TokenType tokenType)
    {
        if (tokens.Count > i && i >= 0)
        {
            if (tokens[i].tokenType == tokenType)
            {
                return (i + 1, tokens[i]);
            }
        }

        return $"Expected token:{tokenType}";
    }
    
    internal static class Reader
    {
        
    }
    

    internal record TokenReadResponse
    {
        public bool Success { get; init; }
        
        public string ErrorMessage { get; init; }
        
        public IReadOnlyList<Token> Tokens { get; init; }

        public int NewIndex { get; init; }

        public static implicit operator TokenReadResponse(string errorMessage)
        {
            return new() { ErrorMessage = errorMessage };
        }
        
        public static implicit operator TokenReadResponse((int newIndex,  IReadOnlyList<Token> tokens) tuple)
        {
            return new() { Success = true, NewIndex = tuple.newIndex, Tokens = tuple.tokens };
        }
        
        public static implicit operator TokenReadResponse((int newIndex, Token token) tuple)
        {
            return new() { Success = true, NewIndex = tuple.newIndex, Tokens = [tuple.token] };
        }
    }

    internal sealed record Result<TValue>
    {
        public TValue Value { get; init; }
        
        public Exception Exception { get; init; }

        public bool Success { get; init; }

        public bool Fail =>!Success;

        public static implicit operator Result<TValue>(Exception exception)
        {
            return new() { Exception = exception };
        }

        public static implicit operator Result<TValue>(TValue value)
        {
            return new() { Value = value, Success = true };
        }

        public static implicit operator Result<TValue>(string errorMessage)
        {
            return new() { Exception = new (errorMessage) };
        }
    }

    internal sealed class NoneObj;

    internal static readonly NoneObj None = new();
    
    internal record Maybe<TValue>
    {
        public TValue Value { get; init; }

        public bool IsNone { get; init; }
        
        public static implicit operator Maybe<TValue>(NoneObj noneObj)
        {
            return new() { IsNone = true};
        }

        public static implicit operator Maybe<TValue>(TValue value)
        {
            return new() { Value = value };
        }
    }
    internal record TokenReadResponse<TValue>
    {
        public bool Success { get; init; }
        
        public string ErrorMessage { get; init; }
        
        public TValue Value { get; init; }

        public int NewIndex { get; init; }

        public static implicit operator TokenReadResponse<TValue>(string errorMessage)
        {
            return new() { ErrorMessage = errorMessage };
        }
        
        public static implicit operator TokenReadResponse<TValue>(Exception exception)
        {
            return new() { ErrorMessage = exception.Message };
        }
        
        public static implicit operator TokenReadResponse<TValue>((int newIndex,  TValue value) tuple)
        {
            return new() { Success = true, NewIndex = tuple.newIndex, Value = tuple.value };
        }
    }

    static TokenReadResponse<T> Then<T>(this TokenReadResponse response, Func<Token,Result<T>> convert)
    {
        if (!response.Success)
        {
            return response.ErrorMessage;
        }

        var result = convert(response.Tokens[0]);
        if (result.Success)
        {
            return (response.NewIndex, result.Value);
        }

        return result.Exception;
    }
    
}