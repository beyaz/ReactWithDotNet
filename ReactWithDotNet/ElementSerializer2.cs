using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization;

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

        public Node ParentNode { get; set; }

        public Node NextSibling { get; set; }

        public bool IsCompleted { get; set; }

        public ElementSerializerContext SerializerContext { get; set; }
        
        public ThirdPartyReactComponent ElementAsThirdPartyReactComponent { get; set; }
        
        public ReactStatefulComponent ElementAsDotNetReactComponent { get; set; }
        
        public HtmlElement ElementAsHtmlElement { get; set; }
        
        public FakeChild ElementAsFakeChild { get; set; }
    }

    public static IReadOnlyDictionary<string, object> ToMap2(this Element element, ElementSerializerContext context)
    {

       
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