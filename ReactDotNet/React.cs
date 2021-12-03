using System;
using System.Collections.Generic;
using Bridge;

namespace ReactDotNet
{
    [Name("React")]
    [External]
    public static class React
    {
        [Name("createElement")]
        public static extern ReactElement createElement(Type componentType);

        [Name("createElement")]
        public static extern ReactElement createElement(string tag, ObjectLiteral attributes, string content);

        [Name("createElement")]
        public static extern ReactElement createElement(Type tag, ObjectLiteral attributes, string content);

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


        [Template("React.createElement({constructorFn}, {attributes}, {children}.ToArray())")]
        public static extern ReactElement createElement(Type constructorFn, ObjectLiteral attributes, IEnumerable<ReactElement> children);


        [Template("React.createElement({tag}, {attributes}, {text}, {children}.ToArray())")]
        public static extern ReactElement createElement(string tag, ObjectLiteral attributes, string text, IEnumerable<ReactElement> children);
    }
}