using System.Reflection;

namespace ReactWithDotNet;


partial class ElementSerializer
{
    class Node
    {
        public bool ElementIsHtmlElement { get; set; }
        
        public bool ElementIsThirdPartyReactComponent{ get; set; }
        
        public bool ElementIsDotNetReactComponent { get; set; }
        
        public bool ElementIsFakeChild { get; set; }

        public bool ElementIsNull{ get; set; }

        public Element Element { get; set; }

        public IReadOnlyDictionary<string, object> ElementAsJsonMap { get; set; }

        public Node Parent { get; set; }

        public bool HasParent => Parent is not null;

        public Node NextSibling { get; set; }

        public bool HasNextSibling => NextSibling is not null;

        public Node FirstChild { get; set; }

        public bool HasFirstChild => FirstChild is not null;

        public bool IsCompleted { get; set; }

        public ElementSerializerContext SerializerContext { get; set; }
        
        public ThirdPartyReactComponent ElementAsThirdPartyReactComponent { get; set; }
        
        public ReactStatefulComponent ElementAsDotNetReactComponent { get; set; }
        
        public HtmlElement ElementAsHtmlElement { get; set; }
        
        public FakeChild ElementAsFakeChild { get; set; }

        public bool IsChildrenOpened { get; set; }
        public bool ElementIsHtmlTextElement { get; set; }
        public HtmlTextNode ElementasHtmlTextElement { get; set; }
        public bool IsAllChildrenCompleted { get; set; }
    }

    public static IReadOnlyDictionary<string, object> ToMap2(this Element element, ElementSerializerContext context)
    {

        var node = ConvertToNode(element, context);

        while (true)
        {
            if (node.IsCompleted)
            {
                if (node.HasNextSibling)
                {
                    node = node.NextSibling;
                    continue;
                }

                if (node.HasParent)
                {
                    node.Parent.IsAllChildrenCompleted = true;

                    node = node.Parent;
                    
                    continue;
                }

                // root
                break;
            }

            if (node.IsAllChildrenCompleted)
            {
                CompleteWithChildren(node, context);

                continue;
            }
            
            if (node.HasFirstChild)
            {
                node = node.FirstChild;
                continue;
            }

            if (node.ElementIsNull)
            {
                node.IsCompleted = true;
                continue;
            }

            if (node.ElementIsFakeChild)
            {
                node.IsCompleted      = true;
                node.ElementAsJsonMap = new Dictionary<string, object> { { "$FakeChild", node.ElementAsFakeChild.Index } };
                continue;
            }

            if (node.ElementIsHtmlTextElement)
            {
                node.IsCompleted = true;
                continue;
            }

            if (node.ElementIsHtmlElement || node.ElementIsThirdPartyReactComponent)
            {
                if (node.IsChildrenOpened is false)
                {
                    OpenChildren(node, context);
                }

                if (node.HasFirstChild)
                {
                    continue;
                }

                node.ElementAsJsonMap = LeafToMap(node.Element, context);

                node.IsCompleted = true;

                continue;
            }

            if (node.ElementIsDotNetReactComponent is false)
            {
                throw FatalError("traverse problem");
            }

            // process React dot net component
            {
                
            }
        }


        return node.ElementAsJsonMap;
    }

    static void CompleteWithChildren(Node node, ElementSerializerContext context)
    {
        var childElements = new List<object>();

        var child = node.FirstChild;

        while (child is not null)
        {
            if (child.ElementIsHtmlTextElement)
            {
                context.TryCallBeforeSerializeElementToClient(child.Element);

                childElements.Add(child.ElementasHtmlTextElement.innerText);
            }
            else
            {
                childElements.Add(child.ElementAsJsonMap);
            }

            child = child.NextSibling;
        }

        var map = LeafToMap(node.Element, context);

        map.Add("$children", childElements);

        node.ElementAsJsonMap = map;
        
        node.IsCompleted = true;
    }

    static Dictionary<string, object> LeafToMap(HtmlElement htmlElement, ElementSerializerContext context)
    {
        var map = new Dictionary<string, object>
        {
            { "$tag", htmlElement.Type }
        };

        AddReactAttributes(map,htmlElement,context);
       
        if (htmlElement.innerText is not null)
        {
            map.Add("$text", htmlElement.innerText);
        }

        return map;
    }

    static Dictionary<string, object> LeafToMap(ThirdPartyReactComponent thirdPartyReactComponent, ElementSerializerContext context)
    {
        var map = new Dictionary<string, object>
        {
            { "$tag", thirdPartyReactComponent.Type }
        };

        AddReactAttributes(map, thirdPartyReactComponent, context);

        return map;
    }

    static Dictionary<string, object> LeafToMap(Element element, ElementSerializerContext context)
    {
        if (element is HtmlElement htmlElement)
        {
            return LeafToMap(htmlElement, context);
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            return LeafToMap(thirdPartyReactComponent, context);
        }

        throw FatalError("Wrong Leaf");
    }

    static void AddReactAttributes(Dictionary<string, object> map, Element htmlElement, ElementSerializerContext context)
    {
        foreach (var propertyInfo in htmlElement.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
        {
            var (propertyValue, noNeedToExport) = getPropertyValue(htmlElement, propertyInfo, context);
            if (noNeedToExport)
            {
                continue;
            }

            map.Add(GetPropertyName(propertyInfo), propertyValue);
        }
    }

    static void OpenChildren(Node node, ElementSerializerContext context)
   {
       node.IsChildrenOpened = true;
       
        var children = node.ElementAsHtmlElement.children;

        Node child = null;
        
        foreach (var item in children)
        {
            var childNode = ConvertToNode(item, context);

            childNode.Parent = node;

            if (child == null)
            {
                node.FirstChild = childNode;
                
                child = childNode;
                
                continue;
            }

            child.NextSibling = childNode;

            child = childNode;
        }
    }

    static Node ConvertToNode(Element element, ElementSerializerContext elementSerializerContext)
    {
        var node = new Node
        {
            Element           = element,
            SerializerContext = elementSerializerContext
        };

        if (element is null)
        {
            node.ElementIsNull = true;
            return node;
        }
        
        if(element is FakeChild fakeChild)
        {
            node.ElementIsFakeChild = true;
            node.ElementAsFakeChild = fakeChild;
            return node;
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            node.ElementIsThirdPartyReactComponent = true;
            node.ElementAsThirdPartyReactComponent = thirdPartyReactComponent;
            return node;
        }

        if (element is ReactStatefulComponent dotNetComponent)
        {
            node.ElementIsDotNetReactComponent = true;
            node.ElementAsDotNetReactComponent = dotNetComponent;
            return node;
        }
        
        if (element is HtmlTextNode htmlTextNode)
        {
            node.ElementIsHtmlTextElement = true;
            node.ElementasHtmlTextElement = htmlTextNode;
            return node;
        }
        
        
        if (element is HtmlElement htmlElement)
        {
            node.ElementIsHtmlElement = true;
            node.ElementAsHtmlElement = htmlElement;
            return node;
        }

        throw FatalError("Node type not recognized");
    }

    static Exception FatalError(string message)
    {
        return new Exception(message);
    }

}