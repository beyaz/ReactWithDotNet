using System;
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
}