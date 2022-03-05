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
        protected internal virtual void BeforeSerialize()
        {
            children.RemoveAll(x => x is null);
            InitializeKeyIfNotExists(children);
        }

        #region Fields
        /// <summary>
        ///     The children
        /// </summary>
        public readonly List<Element> children = new List<Element>();

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

        [React]
        public string width { get; set; }

        [React]
        public string height { get; set; }

        [React]
        public string id { get; set; }
        

        
        

        /// <summary>
        ///     Gets or sets the on click.
        /// </summary>
        [React]
        public Action onClick { get; set; }


        #region Margin
        [JsonIgnore]
        public MarginThickness Margin { get; }

        [JsonIgnore]
        public double? MarginAll
        {
            set
            {
                Margin.Left   = value;
                Margin.Right  = value;
                Margin.Top    = value;
                Margin.Bottom = value;
            }
        }

        [JsonIgnore]
        public double? MarginLeftRight
        {
            set
            {
                Margin.Left  = value;
                Margin.Right = value;
            }
        }

        [JsonIgnore]
        public double? MarginTopBottom
        {
            set
            {
                Margin.Top    = value;
                Margin.Bottom = value;
            }
        }

        #endregion

        #region Padding
        [JsonIgnore]
        public PaddingThickness Padding { get; }

        [JsonIgnore]
        public double? PaddingAll
        {
            set
            {
                Padding.Left   = value;
                Padding.Right  = value;
                Padding.Top    = value;
                Padding.Bottom = value;
            }
        }

        [JsonIgnore]
        public double? PaddingLeftRight
        {
            set
            {
                Padding.Left  = value;
                Padding.Right = value;
            }
        }

        [JsonIgnore]
        public double? PaddingTopBottom
        {
            set
            {
                Padding.Top    = value;
                Padding.Bottom = value;
            }
        }
        #endregion



        /// <summary>
        ///     Gets the style.
        /// </summary>
        [React]
        public Style style { get; private set; } = new Style();

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


        public static Element operator +(Element parent, Element child)  
        {
            parent.children.Add(child);

            return parent;  
        }


        
        
        public static Element operator +(Element element, Style style)  
        {
            element.style = style;

            return element;  
        }
    }

    public abstract class HtmlElement : Element
    {
        public virtual string tagName => GetType().Name.ToLower();
    }

    public abstract class ThirdPartyComponent: Element
    {
        public abstract IReadOnlyList<string> jsLocation { get; }
    }
}