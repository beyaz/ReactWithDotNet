using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static ReactDotNet.UniqueKeyInitializer;

namespace ReactDotNet
{
    /// <summary>
    ///     The element
    /// </summary>
    public abstract class Element : IElement, IEnumerable<IElement>
    {
        #region Fields
        /// <summary>
        ///     The children
        /// </summary>
        protected readonly List<IElement> children = new List<IElement>();
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
        public MarginThickness Margin { get; }

        /// <summary>
        ///     Gets or sets the on click.
        /// </summary>
        [React]
        public Action onClick { get; set; }

        /// <summary>
        ///     Gets the padding.
        /// </summary>
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
        public void Add(IElement element)
        {
            children.Add(element);
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        public IEnumerator<IElement> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        /// <summary>
        ///     Converts to reactelement.
        /// </summary>
        public virtual ReactElement ToReactElement()
        {
            InitializeKeyIfNotExists(children);

            var tag = GetType().Name.ToLower();

            var attributes = this.CollectReactAttributedProperties();

            return new ReactElement
            {
                Tag = tag, 
                Props = attributes, 
                Text = text, 
                Children = children.Select(x => x.ToReactElement()).ToList()
            };
            
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
}