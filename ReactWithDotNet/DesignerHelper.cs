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
            
            if (node.Parameters.Count > 0 && node.Parameters.All(isNumberOrStringNode))
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
        //var tokenAt3 = i + 3 < tokens.Count ? tokens[i + 3] : null;

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

    public static (bool success, IReadOnlyList<Node> nodes, int i) TryReadNodes(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
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