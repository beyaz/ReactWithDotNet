using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

[Serializable]
public sealed partial class Style
{
    StyleAttributeValue headNode;

    [JsonIgnore]
    public bool IsEmpty => headNode is null;

    static StyleAttributeValue FastCloneAll(StyleAttributeValue headNode)
    {
        var nodeInSource = headNode;

        if (nodeInSource is null)
        {
            return null;
        }

        StyleAttributeValue clonedHeadNode = null;

        StyleAttributeValue nodeInTarget = null;

        while (nodeInSource is not null)
        {
            var node = new StyleAttributeValue(nodeInSource.NameInfo)
            {
                Value    = nodeInSource.Value,
                Previous = nodeInTarget
            };

            if (clonedHeadNode is null)
            {
                nodeInTarget = clonedHeadNode = node;
            }
            else
            {
                nodeInTarget.Next = node;

                nodeInTarget = node;
            }

            nodeInSource = nodeInSource.Next;
        }

        return clonedHeadNode;
    }

    static void transfer(Style source, Style target)
    {
        var nodeInSource = source.headNode;
        var nodeInTarget = target.headNode;

        if (nodeInTarget is null)
        {
            target.headNode = FastCloneAll(nodeInSource);

            return;
        }

        while (nodeInSource != null)
        {
            target.Set(nodeInSource.NameInfo, nodeInSource.Value);

            nodeInSource = nodeInSource.Next;
        }
    }

    static StyleAttributeNameInfo TryFindNameInfoByName(ReadOnlySpan<char> name)
    {
        var allNames = Names.AllNames;

        var length = allNames.Length;

        for (var i = 0; i < length; i++)
        {
            if (name.Equals(allNames[i].NameInCamelCase, StringComparison.OrdinalIgnoreCase))
            {
                return allNames[i];
            }
        }

        return null;
    }
    
    internal void Write(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();

        var node = headNode;

        while (node != null)
        {
            writer.WritePropertyName(node.NameInfo.NameInCamelCase.AsSpan());

            writer.WriteStringValue(node.Value.AsSpan());

            node = node.Next;
        }
        
        writer.WriteEndObject();
    }

    string Get(StyleAttributeNameInfo nameInfo)
    {
        var node = headNode;

        while (node != null)
        {
            if (ReferenceEquals(node.NameInfo, nameInfo))
            {
                return node.Value;
            }

            node = node.Next;
        }

        return null;
    }

    string Get(ReadOnlySpan<char> name)
    {
        var node = headNode;

        while (node != null)
        {
            if (name.Equals(node.NameInfo.NameInCamelCase, StringComparison.OrdinalIgnoreCase))
            {
                return node.Value;
            }

            node = node.Next;
        }

        var nameInfo = TryFindNameInfoByName(name);
        if (nameInfo is null)
        {
            throw CssParseException(name.ToString());
        }

        return null;
    }

    void Set(ReadOnlySpan<char> name, string value)
    {
        var nameInfo = TryFindNameInfoByName(name);
        if (nameInfo == null)
        {
            throw CssParseException(name.ToString());
        }

        Set(nameInfo, value);
    }

    void Set(StyleAttributeNameInfo nameInfo, string value)
    {
        var node = headNode;

        // try remove
        if (value == null)
        {
            while (node != null)
            {
                // remove node
                if (ReferenceEquals(nameInfo, node.NameInfo))
                {
                    if (ReferenceEquals(node, headNode))
                    {
                        headNode = node.Next;
                        if (headNode is not null)
                        {
                            headNode.Previous = null;
                        }
                        return;
                    }

                    node.Previous.Next = node.Next;
                    if (node.Next is not null)
                    {
                        node.Next.Previous = node.Previous;
                    }
                    
                    return;
                }

                node = node.Next;
            }

            // not found
            return;
        }

        if (node == null)
        {
            headNode = new StyleAttributeValue(nameInfo) { Value = value };
            return;
        }

        while (node != null)
        {
            // modify
            if (ReferenceEquals(nameInfo, node.NameInfo))
            {
                node.Value = value;
                return;
            }

            if (node.Next == null)
            {
                break;
            }

            node = node.Next;
        }

        node.Next = new StyleAttributeValue(nameInfo)
        {
            Value    = value,
            Previous = node
        };
    }

    internal sealed class StyleAttributeNameInfo
    {
        public readonly string NameInCamelCase;
        public readonly string NameInKebabCase;

        public StyleAttributeNameInfo(string nameInCamelCase, string nameInKebabCase)
        {
            NameInCamelCase = nameInCamelCase;
            NameInKebabCase = nameInKebabCase;
        }
    }

    sealed class StyleAttributeValue
    {
        public readonly StyleAttributeNameInfo NameInfo;
        public StyleAttributeValue Next;

        public StyleAttributeValue Previous;
        public string Value;

        public StyleAttributeValue(StyleAttributeNameInfo nameInfo)
        {
            NameInfo = nameInfo;
        }
    }
}