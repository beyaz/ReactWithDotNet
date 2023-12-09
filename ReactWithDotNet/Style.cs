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
        
        return null;
    }
    
    static bool isEmpty(Style s)
    {
        return s.headNode  is null;
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
                        headNode = null;
                        return;
                    }
                    
                    node.Previous.Next = node.Next;
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
    
    static string getByName(Style s, ReadOnlySpan<char> name)
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

    static void setByName(Style s, string name, string value)
    {
        
        var nameInfo = TryFindNameInfoByName(s, name);
        if (nameInfo == null)
        {
            throw CssParseException(name.ToString());    
        }
        
        s.Set(nameInfo, value);
    }
    
    
    StyleAttributeValue headNode;

    

   

   
    



    static void transfer(Style source, Style target)
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

    static void visitNotNullValues(Style s, Action<string, string> action)
    {
        var currentNode = s.headNode;
        
        while (currentNode != null)
        {
            action(currentNode.NameInfo.NameInCamelCase, currentNode.Value);

            if (currentNode.Next == null)
            {
                break;
            }
            
            currentNode = currentNode.Next;
        }
    }

    public string accentColor2
    {
        set => Set(Names.@float, value);
        get => Get(Names.accentColor);
    }

}