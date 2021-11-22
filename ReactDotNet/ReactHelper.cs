using System;
using System.Collections.Generic;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
    [Name("ReactDOM")]
    [External]
    public static class ReactDOM
    {
        [Name("render")]
        public static extern void render(ReactElement element, HTMLElement container);

        [Name("render")]
        public static extern void render(ReactElement element, HTMLElement container, Action callback);

        [Name("unmountComponentAtNode")]
        public static extern bool unmountComponentAtNode(HTMLElement container);
    }

    [Name("React")]
    [External]
    public static class React
    {
        [Name("createElement")]
        public static extern ReactElement createElement(Type componentType);

        [Name("createElement")]
        public static extern ReactElement createElement(string tag, ObjectLiteral attributes, string content);

        [Name("createElement")]
        public static extern ReactElement createElement(string tag, ObjectLiteral attributes);

        [Name("createElement")]
        public static extern ReactElement createElement(Type tag, ObjectLiteral attributes);

        [Name("createElement")]
        public static extern ReactElement createElement(Type tag, ObjectLiteral attributes, ReactElement children);

        [Name("createElement")]
        public static extern ReactElement createElement(Type tag, ObjectLiteral attributes, ReactElement[] children);


        [Template("React.createElement({tag}, {attributes}, {children}.ToArray())")]
        public static extern ReactElement createElement(string tag, ObjectLiteral attributes, IEnumerable<ReactElement> children);
    }
}