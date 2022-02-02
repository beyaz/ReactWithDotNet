using System;
using System.Linq;

namespace ReactDotNet.PrimeReact
{

    
       
    public class BlockUI : ElementBase
    {
        /// <summary>
        /// Unique identifier of the element.
        /// </summary>
        [React]
        public string id { get; set; }

        /// <summary>
        /// Controls the blocked state.
        /// </summary>
        [React]
        public bool blocked { get; set; }


        [React]
        public Element template { get; set; }
        

    }

    [Serializable]
    public class ListBoxChangeParams
    {
        public string value { get; set; }
    }

    public class ListBox : ElementBase
    {
        /// <summary>
        /// An array of objects to display as the available options.
        /// </summary>
        [React]
        public object[] options { get; set; }

        /// <summary>
        /// Selected value to display.
        /// </summary>
        [React]
        public object value { get; set; }

        /// <summary>
        /// Name of the label field of an option when an arbitrary objects instead of SelectItems are used as options.
        /// </summary>
        [React]
        public string optionLabel { get; set; }

        /// <summary>
        /// Name of the value field of an option when arbitrary objects are used as options instead of SelectItems.
        /// </summary>
        [React]
        public string optionValue { get; set; }

        /// <summary>
        /// When specified, displays a filter input at header.
        /// </summary>
        [React]
        public bool filter { get; set; }


        [React]
        public Action<ListBoxChangeParams> onChange { get; set; }

        /// <summary>
        ///     Inline style of inner list element.
        /// </summary>
        [React]
        public Style listStyle { get; } = new Style();
        


    }
}