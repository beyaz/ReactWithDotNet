using ReactWithDotNet.Tokenizing;
using static ReactWithDotNet.Tokenizing.Lexer;

namespace ReactWithDotNet;

public sealed class DesignerCode : List<(IReadOnlyList<int> VisualLocation, IReadOnlyList<Modifier> Modifiers)>
{
    public void Add(IReadOnlyList<int> VisualLocation, IReadOnlyList<Modifier> Modifiers)
    {
        Add((VisualLocation,Modifiers));
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
    
    
    public sealed class Node
    {
        public string Name { get; init; }
        
        public IReadOnlyList<Node> Parameters { get; init; }

        public required IReadOnlyList<Token> Tokens { get; init; }

        public int Start { get; init; }

        public int End { get; init; }
        
        public override string ToString()
        {
            if (Parameters?.Count > 0)
            {
                return $"{Name}({string.Join(", ", Parameters)})";
            }
            
            return Lexer.ToString(Tokens, Start, End);
        }
    }

  

    public static (bool success, Node node, int endIndex) TryReadNode(IReadOnlyList<Token> tokens, int startIndex, int endIndex)
    {
        var i = startIndex;

        var tokenAt0 = i + 0 < tokens.Count ? tokens[i + 0] : null;
        var tokenAt1 = i + 1 < tokens.Count ? tokens[i + 1] : null;
        var tokenAt2 = i + 2 < tokens.Count ? tokens[i + 2] : null;
        var tokenAt3 = i + 3 < tokens.Count ? tokens[i + 3] : null;

        if (tokenAt0?.tokenType == TokenType.QuotedString)
        {
            return (success: true, node: new()
                {
                    Tokens = tokens,
                    Start  = i,
                    End    = i
                },
                endIndex: i );
        }
        
        if (tokenAt0?.tokenType == TokenType.AlfaNumeric)
        {
            if (tokenAt1?.tokenType == TokenType.Dot)
            {
                return (success: true, node: new()
                    {
                        Tokens = tokens,
                        Start  = i,
                        End = i+2
                    },
                    endIndex: i + 2);
            }

            if (tokenAt0.value.All(char.IsNumber) || startIndex == endIndex)
            {
                return (success: true, node: new()
                    {
                        Tokens = tokens,
                        Start  = i,
                        End    = i
                    },
                    endIndex: i );
            }
            
            if (tokenAt1?.tokenType == TokenType.Comma)
            {
                return (success: true, new() { Name = tokens[i].value, Tokens = tokens, Start = i, End = i}, i);
            }
            
            if (i < tokens.Count && tokens[i + 1].tokenType == TokenType.LeftParenthesis)
            {
                var (isFound, indexOfPair) = FindPair(tokens, i + 1, x => x.tokenType == TokenType.RightParenthesis);
                if (isFound)
                {
                    var (success, parameterNodes, rightParenthesisIndex) = TryReadNodes(tokens,  i + 2, indexOfPair-1);
                    if (success)
                    {
                        if (rightParenthesisIndex == indexOfPair)
                        {
                            return (success: true, new()
                            {
                                Name = tokens[i].value, 
                                Tokens = tokens, 
                                Start = i,
                                End = indexOfPair,
                                Parameters = parameterNodes
                                
                            }, indexOfPair);        
                        }
                    }

                    
                }
            }
        }
        

        return default;
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

            i = newIndex+1;
            
            nodes.Add(node);

            
        }

        return (success: true, nodes, i);
    }
}