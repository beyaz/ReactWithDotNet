using System;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet.PrimeReact
{
    [Enum(Emit.StringNameLowerCase)]
    public enum ButtonPositionType
    {
        top, bottom, left, right
    }

    public class Button : ElementBase
    {
        [React]
        public Action<SyntheticEvent<HTMLElement>> onClick { get; set; }

        /// <summary>
        ///     Text of the button.
        /// </summary>
        [React]
        public string label { get; set; }

        [React]
        public string icon { get; set; }

        /// <summary>
        /// Position of the icon, valid values are "left", "right", "top" and "bottom".
        /// </summary>
        [React]
        public ButtonPositionType iconPos { get; set; }

        /// <summary>
        ///Display loading icon of the button
        /// </summary>
        [React]
        public bool? loading { get; set; }

        /// <summary>
        /// Name of the loading icon or JSX.Element for loading icon.
        /// </summary>
        [React]
        public string loadingIcon { get; set; }
    
    }
}