using System;
using System.Collections.Generic;
using System.Linq;
using Bridge;

namespace ReactDotNet
{
    public static class ReactHelper
    {
        public static ReactElement CreateReactElement(string tag, ObjectLiteral attributes, string content)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            return Script.Write<ReactElement>("React.createElement(tag, attributes, content)");
        }

        public static ReactElement CreateReactElement(string tag, ObjectLiteral attributes)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            return Script.Write<ReactElement>("React.createElement(tag, attributes)");
        }

        public static ReactElement CreateReactElement(Type tag, ObjectLiteral attributes)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            return Script.Write<ReactElement>("React.createElement(tag, attributes)");
        }

        public static ReactElement CreateReactElement(string tag, ObjectLiteral attributes, IEnumerable<ReactElement> children)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            // ReSharper disable once UnusedVariable
            var childrenArray = children.ToArray();

            return Script.Write<ReactElement>("React.createElement(tag, attributes, childrenArray)");
        }

        public static ReactElement CreateReactElement(Type tag, ObjectLiteral attributes, ReactElement children)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            return Script.Write<ReactElement>("React.createElement(tag, attributes, children)");
        }

        public static ReactElement CreateReactElement(Type tag, ObjectLiteral attributes, IEnumerable<ReactElement> children)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            // ReSharper disable once UnusedVariable
            var childrenArray = children.ToArray();

            return Script.Write<ReactElement>("React.createElement(tag, attributes, childrenArray)");
        }

        public static void RenderReactElementIn<TComponent>(string domElementId) where TComponent : ReactComponentBase
        {
            // ReSharper disable once UnusedVariable
            var elementConstructor = typeof(TComponent);

            // ReSharper disable once UnusedVariable
            var reactElement = Script.Write<ReactElement>("React.createElement(elementConstructor)");

            Script.Write("ReactDOM.render(reactElement, document.getElementById(domElementId))");
        }
    }
}