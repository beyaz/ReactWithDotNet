using System.Globalization;
using System.Reflection;
using ReactWithDotNet.Tokenizing;
using ReactWithDotNet.UIDesigner;
using static ReactWithDotNet.Tokenizing.Lexer;
using static ReactWithDotNet.DesignerHelper.FP;

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

    public static Result<DesignerCode> ReadDesignerCode(string classDefinitionCode)
    {
        return ReadDesignerCodeTokens(classDefinitionCode)
            .Then(x => ReadDesignerValueFromTokens(x.tokens))
            .Then(ToDesignerCode);
    }

    internal static IReadOnlyList<Token> ClearSpaceTokens(IReadOnlyList<Token> tokens)
    {
        return tokens.Where(t => t.tokenType != TokenType.Space).ToList();
    }

    internal static Result<(MethodInfo methodInfo, object[] methodParameters)> ToModifier(Node node)
    {
        if (node.Name.HasValue())
        {
            if (node.Parameters is null || node.Parameters.Count is 0)
            {
                var propertyInfo = typeof(Mixin).GetProperty(node.Name);
                if (propertyInfo is not null)
                {
                    return (propertyInfo.GetMethod, []);
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
                        return (targetMethod, parameters);
                    }
                }
            }

            {
                var targetMethodInfo = typeof(Mixin).GetMethod(node.Name, [typeof(StyleModifier[])]);
                if (targetMethodInfo is not null)
                {
                    return node.Parameters.Select(ToModifier).Select(Compile).Fold().Then(modifiers =>
                    {
                        object[] prm = [modifiers.Select(x => (StyleModifier)x).ToArray()];

                        return (targetMethodInfo, prm);
                    });
                }
            }
        }

        return $"{node} not converted to modifier";

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

    static Modifier Compile((MethodInfo methodInfo, object[] methodParameters) tuple)
    {
        return (Modifier)tuple.methodInfo.Invoke(null, tuple.methodParameters);
    }

    static int FindIndex<T>(this IReadOnlyList<T> items, Predicate<T> match)
    {
        var i = 0;
        foreach (var item in items)
        {
            if (match(item))
            {
                return i;
            }

            i++;
        }

        return -1;
    }

    static Result<IReadOnlyList<T>> Fold<T>(this IEnumerable<Result<T>> items)
    {
        var resultList = new List<T>();

        foreach (var item in items)
        {
            if (item.Fail)
            {
                return item.Exception;
            }

            resultList.Add(item.Value);
        }

        return resultList;
    }

    internal static Maybe<(int startIndex, int endIndex)> ReadDesignerCodeWithRegions(string classDefinitionCode)
    {
        const string startLine = "#region Designer Code [Do not edit manually]";

        const string endLine = "#endregion";

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

        endIndex = classDefinitionCode.IndexOf(Environment.NewLine, endIndex,StringComparison.OrdinalIgnoreCase);

        return (startIndex, endIndex);
    }

    static string RemoveRegions(string csharpCode)
    {
        return 
        string.Join(Environment.NewLine, csharpCode.Split(Environment.NewLine)
                        .Where(line => line.Trim().StartsWith("#region", StringComparison.OrdinalIgnoreCase) ||
                                       line.Trim().StartsWith("#endregion", StringComparison.OrdinalIgnoreCase)));
    }
    
    static Maybe<(IReadOnlyList<Token> tokens, int startIndex, int endIndex)> ReadDesignerCodeTokens(string classDefinitionCode)
    {
        int startIndex;
        int endIndex;

        string csharpCode;
        {
            const string startLine = "#region Designer Code [Do not edit manually]";

            const string endLine = "#endregion";

            startIndex = classDefinitionCode.IndexOf(startLine, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return None;
            }

            endIndex = classDefinitionCode.IndexOf(endLine, StringComparison.OrdinalIgnoreCase);
            if (endIndex < 0)
            {
                return None;
            }

            csharpCode = classDefinitionCode.Substring(startIndex, endIndex - startIndex);
        }

        // remove region
        csharpCode = string.Join(Environment.NewLine, csharpCode.Split(Environment.NewLine).Skip(1));

        var (hasRead, _, tokens) = ParseTokens(csharpCode, 0);
        if (!hasRead)
        {
            return None;
        }

        return (ClearSpaceTokens(tokens), startIndex, endIndex);
    }

    static Result<IReadOnlyList<(long[] location, IReadOnlyList<Node> nodes)>> ReadDesignerValueFromTokens(IReadOnlyList<Token> tokens)
    {
        var returnList = new List<(long[] location, IReadOnlyList<Node> nodes)>();

        var tokenList = ClearSpaceTokens(tokens);

        var leftCurlyBracketIndex = tokenList.FindIndex(x => x.tokenType == TokenType.LeftCurlyBracket);
        if (leftCurlyBracketIndex < 0)
        {
            return returnList;
        }

        var i = leftCurlyBracketIndex;

        // readLeftCurlyBracket
        {
            var response = Reader.ReadToken(tokens, i, TokenType.LeftCurlyBracket);
            if (!response.Success)
            {
                return response.Exception;
            }

            i++;
        }

        while (i < tokens.Count)
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

            var entry = Reader.ReadElementEntry(tokens, i);
            if (!entry.Success)
            {
                return entry.Exception;
            }

            returnList.Add((entry.Value.location, entry.Value.nodes));

            i = entry.Value.lastUsedIndex + 1;
        }

        return returnList;
    }

    static IEnumerable<Result<B>> Select<A, B>(this IEnumerable<Result<A>> enumerable, Func<A, B> func)
    {
        foreach (var result in enumerable)
        {
            if (result.Fail)
            {
                yield return result.Exception;
            }

            yield return func(result.Value);
        }
    }

    static Result<DesignerCode> ToDesignerCode(this IReadOnlyList<(long[] location, IReadOnlyList<Node> nodes)> records)
    {
        var returnObject = new DesignerCode();

        foreach (var (location, nodes) in records)
        {
            var modifiers = nodes.Select(ToModifier).Select(Compile).Fold();
            if (modifiers.Fail)
            {
                return modifiers.Exception;
            }

            returnObject.Add(location.Select(Convert.ToInt32).ToArray(), modifiers.Value);
        }

        return returnObject;
    }

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

    static Result<long> TryParseLong(string x)
    {
        return Try(() => long.Parse(x));
    }

    public static class FP
    {
        internal static readonly NoneObj None = new();

        internal sealed class NoneObj;

        internal sealed record Result<TValue>
        {
            public TValue Value { get; init; }

            public Exception Exception { get; init; }

            public bool Success { get; init; }

            public bool Fail => !Success;

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
                return new() { Exception = new(errorMessage) };
            }

            public Result<T> Then<T>(Func<TValue, Result<T>> onSuccess)
            {
                if (Fail)
                {
                    return Exception;
                }

                return onSuccess(Value);
            }

            public Result<T> Then<T>(Func<TValue, T> onSuccess)
            {
                if (Fail)
                {
                    return Exception;
                }

                return onSuccess(Value);
            }
        }

        internal record Maybe<TValue>
        {
            public TValue Value { get; init; }

            public bool IsNone { get; init; }

            public static implicit operator Maybe<TValue>(NoneObj noneObj)
            {
                return new() { IsNone = true };
            }

            public static implicit operator Maybe<TValue>(TValue value)
            {
                return new() { Value = value };
            }

            public Result<B> Then<B>(Func<TValue, Result<B>> next)
            {
                var maybe = this;
                if (maybe.IsNone)
                {
                    return default(B);
                }

                return next(maybe.Value);
            }
        }
    }

    public static class NodeReader
    {
        public static Result<(int endIndex, Node node)> TryReadNode(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
        {
            var i = startIndex;

            var tokenAt0 = i + 0 < tokens.Count ? tokens[i + 0] : null;
            var tokenAt1 = i + 1 < tokens.Count ? tokens[i + 1] : null;
            var tokenAt2 = i + 2 < tokens.Count ? tokens[i + 2] : null;

            if (tokenAt0?.tokenType == TokenType.QuotedString)
            {
                return (endIndex: i, new()
                {
                    IsStringNode = true,
                    StringValue  = tokenAt0.value
                });
            }

            if (tokenAt0?.tokenType == TokenType.AlfaNumeric)
            {
                if (tokenAt1?.tokenType == TokenType.Dot)
                {
                    return (endIndex: i + 2, new()
                    {
                        IsDoubleNode = true,
                        DoubleValue  = double.Parse(tokenAt0.value + '.' + tokenAt2?.value)
                    });
                }

                if (tokenAt0.value.All(char.IsNumber))
                {
                    return (endIndex: i, new()
                    {
                        IsNumberNode = true,
                        NumberValue  = long.Parse(tokenAt0.value)
                    });
                }

                if (startIndex == endIndex)
                {
                    return (endIndex: i, new()
                    {
                        Name = tokenAt0.value
                    });
                }

                if (tokenAt1?.tokenType == TokenType.Comma)
                {
                    return (endIndex: i, new()
                    {
                        Name = tokens[i].value
                    });
                }

                if (tokenAt1?.tokenType == TokenType.LeftParenthesis)
                {
                    var (isFound, indexOfPair) = FindPair(tokens, i + 1, x => x.tokenType == TokenType.RightParenthesis);
                    if (isFound)
                    {
                        var response = TryReadNodes(tokens, i + 2, indexOfPair - 1);
                        if (response.Fail)
                        {
                            return response.Exception;
                        }

                        if (response.Value.endIndex == indexOfPair)
                        {
                            return (endIndex: indexOfPair, new()
                            {
                                Name       = tokens[i].value,
                                Parameters = response.Value.nodes
                            });
                        }
                    }
                }
            }

            return $"Node not resolved.{tokenAt0}";
        }

        public static Result<(IReadOnlyList<Node> nodes, int endIndex)> TryReadNodes(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
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

                var response = TryReadNode(tokens, i, endIndex);
                if (response.Fail)
                {
                    return response.Exception;
                }

                i = response.Value.endIndex + 1;

                nodes.Add(response.Value.node);
            }

            return (nodes, i);
        }
    }

    internal static class Reader
    {
        internal static Result<(long[] location, IReadOnlyList<Node> nodes, int lastUsedIndex)> ReadElementEntry(IReadOnlyList<Token> tokens, int i)
        {
            long[] location;
            IReadOnlyList<Node> nodes;

            {
                var response = ReadToken(tokens, i, TokenType.LeftCurlyBracket);
                if (!response.Success)
                {
                    return response.Exception;
                }

                i++;
            }

            {
                var response = ReadInt64Array(tokens, i);
                if (!response.Success)
                {
                    return response.Exception;
                }

                i = response.Value.lastUsedIndex;

                location = response.Value.value;

                i++;
            }

            {
                var response = ReadToken(tokens, i, TokenType.Comma);
                if (!response.Success)
                {
                    return response.Exception;
                }

                i++;
            }

            {
                var response = ReadToken(tokens, i, TokenType.LeftSquareBracket);
                if (!response.Success)
                {
                    return response.Exception;
                }

                i++;

                var (isFound, indexOfPair) = FindPair(tokens, i - 1, x => x.tokenType == TokenType.RightSquareBracket);
                if (!isFound)
                {
                    return nok($"Close pair not found. At: {tokens[i - 1].startIndex}");
                }

                var nodeListResponse = NodeReader.TryReadNodes(tokens, i, indexOfPair - 1);
                if (nodeListResponse.Fail)
                {
                    return nodeListResponse.Exception;
                }

                i = nodeListResponse.Value.endIndex + 1;

                nodes = nodeListResponse.Value.nodes;
            }

            {
                var response = ReadToken(tokens, i, TokenType.RightCurlyBracket);
                if (!response.Success)
                {
                    return response.Exception;
                }

                return (location, nodes, i);
            }

            static Result<(long[] location, IReadOnlyList<Node> nodes, int lastUsedIndex)> nok(string errorMessage)
            {
                return errorMessage;
            }
        }

        internal static Result<long> ReadInt64(IReadOnlyList<Token> tokens, int i)
        {
            return ReadToken(tokens, i, TokenType.AlfaNumeric).Then(t => TryParseLong(t.value));
        }

        internal static Result<(long[] value, int lastUsedIndex)> ReadInt64Array(IReadOnlyList<Token> tokens, int i)
        {
            var response = ReadToken(tokens, i, TokenType.LeftSquareBracket);
            if (response.Fail)
            {
                return response.Exception;
            }

            i++;

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
                    return (items.ToArray(), i);
                }

                if (token.tokenType == TokenType.Comma)
                {
                    i++;
                    continue;
                }

                var tokenReadResponse = ReadInt64(tokens, i);
                if (!tokenReadResponse.Success)
                {
                    return tokenReadResponse.Exception;
                }

                items.Add(tokenReadResponse.Value);

                i++;
            }
        }

        internal static Result<Token> ReadToken(IReadOnlyList<Token> tokens, int i, TokenType tokenType)
        {
            if (tokens.Count > i && i >= 0)
            {
                if (tokens[i].tokenType == tokenType)
                {
                    return tokens[i];
                }
            }

            return $"Expected token: {tokenType}";
        }
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
}