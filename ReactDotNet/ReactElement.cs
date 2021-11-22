using Bridge;

namespace ReactDotNet
{
    [External]
    [ObjectLiteral]
    public class ReactElement
    {
        [Template("{0}.ToReactElement()")]
        public static implicit operator ReactElement(Element element)
        {
            return element.ToReactElement();
        }
    }
}