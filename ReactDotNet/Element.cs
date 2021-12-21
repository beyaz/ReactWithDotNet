using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using static ReactDotNet.UniqueKeyInitializer;

namespace ReactDotNet
{
    /// <summary>
    ///     The element
    /// </summary>
    public abstract class Element : IEnumerable<Element>
    {
        

        #region Fields
        /// <summary>
        ///     The children
        /// </summary>
        protected internal readonly List<Element> children = new List<Element>();
        #endregion

        #region Constructors
        /// <summary>
        ///     Initializes a new instance of the <see cref="Element" /> class.
        /// </summary>
        protected Element()
        {
            Margin  = new MarginThickness(style);
            Padding = new PaddingThickness(style);
        }
        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets or sets the name of the class.
        /// </summary>
        [React]
        public string className { get; set; }

        /// <summary>
        ///     Gets or sets the gravity.
        /// </summary>
        public int? gravity { get; set; }

        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        [React]
        public string key { get; set; }

        /// <summary>
        ///     Gets the margin.
        /// </summary>
        [JsonIgnore]
        public MarginThickness Margin { get; }

        /// <summary>
        ///     Gets or sets the on click.
        /// </summary>
        [React]
        public Action onClick { get; set; }

        /// <summary>
        ///     Gets the padding.
        /// </summary>
        [JsonIgnore]
        public PaddingThickness Padding { get; }

        /// <summary>
        ///     Gets the style.
        /// </summary>
        [React]
        public CSSStyleDeclaration style { get; } = new CSSStyleDeclaration();

        /// <summary>
        ///     'innerText' property of element.
        /// </summary>
        public string text { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Adds the specified element.
        /// </summary>
        public void Add(Element element)
        {
            children.Add(element);
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        public IEnumerator<Element> GetEnumerator()
        {
            return children.GetEnumerator();
        }
        #endregion

        #region Explicit Interface Methods
        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }
        #endregion
    }

    public abstract class HtmlElement:Element
    {
        public string Tag => GetType().Name.ToLower();
    }

    public abstract class ThirdPartyComponent: Element
    {
        public abstract IReadOnlyList<string> JsLocation { get; }
    }
}