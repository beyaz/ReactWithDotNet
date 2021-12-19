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

        public Dictionary<string, object> Props { get; set; } = new Dictionary<string, object>();

        public IReadOnlyList<ReactElement> Children { get; set; }

        public static implicit operator ReactElement(Element element)
        {
            return element.ToReactElement();
        }

        public string FullName { get; set; }
        public object State { get; set; }
        public ReactElement RootElement { get; set; }
    }
}