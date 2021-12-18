using System;
using System.Collections.Generic;

namespace ReactDotNet
{
    [Serializable]
    public sealed class ReactElement
    {

        public string Tag { get; set; }
        public IReadOnlyList<string> Path { get; set; }
        public string Text { get; set; }
        public IReadOnlyDictionary<string, object> Props { get; set; }
        public IReadOnlyList<ReactElement> Children { get; set; }

        public static implicit operator ReactElement(Element element)
        {
            return element.ToReactElement();
        }
    }
}