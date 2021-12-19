using System;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet.PrimeReact
{
    [External]
    public class AutoCompleteChangeTargetOptions
    {
        public string name;
        public string  id;
        public object value;
    }
    [External]
    public class AutoCompleteChangeParams
    {
        public SyntheticEvent<HTMLElement> originalEvent;
        public object value;
        public extern void stopPropagation();
        public extern void preventDefault();
        public AutoCompleteChangeTargetOptions target;
    }


    [External]
    public class AutoCompleteCompleteMethodParams
    {
        public SyntheticEvent<HTMLElement> originalEvent;
        public string query;
    }

    
    public class AutoComplete : PrimeElementBase
    {
        /// <summary>
        ///     Value of the component.
        ///     <para>default: null</para>
        /// </summary>
        [React]
        public object value { get; set; }

        /// <summary>
        ///     An array of suggestions to display.
        ///     <para>default: null</para>
        /// </summary>
        [React]
        public object[] suggestions { get; set; }

        /// <summary>
        ///     Field of a suggested object to resolve and display.
        ///     <para>default: null</para>
        /// </summary>
        [React]
        public string field { get; set; }

        /// <summary>
        /// Callback to invoke when autocomplete value changes.
        /// </summary>
        [React]
        public Action<AutoCompleteChangeParams> onChange { get; set; }


        /// <summary>
        /// Callback to invoke to search for suggestions.
        /// </summary>
        [React]
        public Action<AutoCompleteCompleteMethodParams> completeMethod { get; set; }

    }
}