
using System;
using System.Collections.Generic;

namespace ReactDotNet
{
    [Serializable]
    public sealed class Node
    {
        public string Tag { get; set; }
        public IReadOnlyList<string> Path { get; set; }
        public string Text { get; set; }
        public IReadOnlyDictionary<string,object> Props { get; set; }
        public IReadOnlyList<Node> Children { get; set; }
    }

    public class ReactAttribute : Attribute
    {
    }

    public class Element
    {
        [React]
        public string className { get; set; }

        /// <summary>
        ///     'innerText' property of element.
        /// </summary>
        public string text { get; set; }

        public virtual Node Render()
        {
            var tag = GetType().Name.ToLower();

            return React.createElement(tag, attributes, text, children.Select(x => x.ToReactElement()));

            return new Node { Tag = tag, Text = text};
        }
    }

    public class div: Element
    {
        public div() { }

        public div(string className)
        {
            this.className = className;
        }
    }

}
