using System.Text;

namespace ReactWithDotNet;

[Serializable]
public sealed partial class Style
{

    sealed class StyleAttributeNameInfo
    {
        public readonly string NameInCamelCase;
        public readonly string NameInKebabCase;

        public StyleAttributeNameInfo(string nameInCamelCase, string nameInKebabCase)
        {
            NameInCamelCase      = nameInCamelCase;
            NameInKebabCase = nameInKebabCase;
        }
    }

    sealed class StyleAttributeValue
    {
        public readonly StyleAttributeNameInfo NameInfo;
        public string Value;

        public StyleAttributeValue(StyleAttributeNameInfo nameInfo)
        {
            NameInfo   = nameInfo;
        }

        public StyleAttributeValue Previous;
        public StyleAttributeValue Next;
    }


    static bool isEmpty(Style s)
    {
        return s.headNode  is null;
    }

    static StyleAttributeNameInfo TryFindNameInfoByName(Style s, ReadOnlySpan<char> name)
    {
        var allNames = Names.AllNames;
        var length = allNames.Length;
        
        for (int i = 0; i < length; i++)
        {
            if (name.Equals(allNames[i].NameInKebabCase,StringComparison.OrdinalIgnoreCase))
            {
                return allNames[i];
            }
        }

        return null;
    }
    
    static string getByName2(Style s, ReadOnlySpan<char> name)
    {
        var value = s.Get(name);
        if (value is not null)
        {
            return value;
        }
        
        var nameInfo = TryFindNameInfoByName(s, name);
        if (nameInfo is null)
        {
            throw CssParseException(name.ToString());    
        }
        
        return null;
    }

    static void setByName2(Style s, string name, string value)
    {
        
        var nameInfo = TryFindNameInfoByName(s, name);
        if (nameInfo == null)
        {
            throw CssParseException(name.ToString());    
        }
        
        s.Set(nameInfo, value);
    }
    
    
    StyleAttributeValue headNode;

    

    string Get(StyleAttributeNameInfo nameInfo)
    {
        var currentNode = headNode;
        
        while (currentNode != null)
        {
            // update
            if (ReferenceEquals(nameInfo, currentNode.NameInfo))
            {
                return currentNode.Value;
            }

            if (currentNode.Next == null)
            {
                break;
            }
            
            currentNode = currentNode.Next;
        }
        
        return null;
    }
    
    string Get(ReadOnlySpan<char> name)
    {
        var currentNode = headNode;
        
        while (currentNode != null)
        {
            if (name.Equals(currentNode.NameInfo.NameInCamelCase, StringComparison.OrdinalIgnoreCase))
            {
                return currentNode.Value;
            }

            if (currentNode.Next == null)
            {
                break;
            }
            
            currentNode = currentNode.Next;
        }
        
        return null;
    }

    void Set(StyleAttributeNameInfo nameInfo, string value)
    {
        var currentNode = headNode;
        
        // try remove
        if (value == null)
        {
            while (currentNode != null)
            {
                // remove node
                if (ReferenceEquals(nameInfo, currentNode.NameInfo))
                {
                    currentNode.Previous = currentNode.Next;
                    return;
                }

                // not found
                if (currentNode.Next == null)
                {
                    break;
                }
            
                currentNode = currentNode.Next;
            }
            
            // not found
            return;
        }
        
        if (currentNode == null)
        {
            headNode = new StyleAttributeValue(nameInfo) { Value = value };
            return;
        }

        
        while (currentNode != null)
        {
            // update
            if (ReferenceEquals(nameInfo, currentNode.NameInfo))
            {
                currentNode.Value = value;
                return;
            }

            if (currentNode.Next == null)
            {
                break;
            }
            
            currentNode = currentNode.Next;
        }

        currentNode.Next = new StyleAttributeValue(nameInfo)
        {
            Value = value,
            Previous = currentNode
        };
    }
    

    string ToCss2(bool isImportant)
    {
        var sb = new StringBuilder();

        var separator = isImportant ? " !important;" : ";";
        
        var currentNode = headNode;
        
        while (currentNode != null)
        {
            sb.Append(currentNode.NameInfo.NameInKebabCase);
            sb.Append(":");
            sb.Append(currentNode.Value);
            sb.Append(separator);

            if (currentNode.Next == null)
            {
                break;
            }
            
            currentNode = currentNode.Next;
        }

        if (sb.Length == 0)
        {
            return null;
        }

        return sb.ToString();
    }

    static void transfer2(Style source, Style target)
    {
        var nodeInSource = source.headNode;
        var nodeInTarget = target.headNode;
        
        if (nodeInTarget is null)
        {
            // fast clone to target

            target.headNode = FastCloneAll(nodeInSource);
            
            return;
        }
        
        
        while (nodeInSource != null)
        {
            target.Set(nodeInSource.NameInfo, nodeInSource.Value);
                
            nodeInSource = nodeInSource.Next;
        }
    }

    static StyleAttributeValue FastCloneAll(StyleAttributeValue headNode)
    {
        if (headNode is null)
        {
            return null;
        }

        var clonedHeadNode =  new StyleAttributeValue(headNode.NameInfo)
        {
            Value = headNode.Value
        };

        var nodeInTarget = clonedHeadNode;
        var nodeInSource = headNode;
        
        while (nodeInSource is not null)
        {
            nodeInTarget = new StyleAttributeValue(nodeInSource.NameInfo)
            {
                Value    = nodeInSource.Value,
                Previous = nodeInTarget
            };
            
            nodeInSource = nodeInSource.Next;
        }

        return clonedHeadNode;
    }

    internal void VisitNotNullValues2(Action<string, string> action)
    {
        
    }

    public string accentColor2
    {
        set => Set(Names.@float, value);
        get => Get(Names.accentColor);
    }

}