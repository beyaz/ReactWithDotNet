using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
    public abstract class Element : IElement, IEnumerable<IElement>
    {
        protected Element()
        {
            Margin  = new MarginThickness(style);
            Padding = new PaddingThickness(style);
        }

        public int? gravity { get; set; }

        public MarginThickness Margin { get; }
        public PaddingThickness Padding { get; }

        [React]
        public string key { get; set; }

        [React]
        public string className { get; set; }

        [React]
        public CSSStyleDeclaration style { get; } = ObjectLiteral.Create<CSSStyleDeclaration>();

       

        protected readonly List<IElement> children = new List<IElement>();

        public IEnumerator<IElement> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }

        public void Add(IElement element)
        {
            children.Add(element);
        }

        public string text { get; set; }
        public virtual ReactElement ToReactElement()
        {
            UniqueKeyInitializer.InitializeKeyIfNotExists(children);

            var tag = GetType().Name;


            return React.createElement(tag, this.CollectReactAttributedProperties(), children.Select(x => x.ToReactElement()));
        }
    }
}