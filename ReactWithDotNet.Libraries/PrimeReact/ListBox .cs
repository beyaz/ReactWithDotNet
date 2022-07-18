using System;
using System.Collections;
using System.Collections.Generic;

namespace ReactWithDotNet.PrimeReact
{



    public class BlockUI : ElementBase
    {
        

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
        public IEnumerable options { get; set; }

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

        [React]
        [ReactTemplate]
        public Func<string, Element> itemTemplate { get; set; }


        internal List<KeyValuePair<object, object>> GetItemTemplates(Func<object, IReadOnlyDictionary<string, object>> toMap)
        {
            var map = new List<KeyValuePair<object, object>>();

            foreach (var option in options)
            {
                map.Add(new KeyValuePair<object, object>(option, toMap(option)));
            }

            return map;
        }


    }
}