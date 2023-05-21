namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact
{
    

    public enum ButtonPositionType
    {
        top, bottom, left, right
    }

    public class Button : ElementBase
    {
        [ReactProp]
        [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
        public Action<MouseEvent> onClick { get; set; }
        
        /// <summary>
        ///     Text of the button.
        /// </summary>
        [ReactProp]
        public string label { get; set; }

        [ReactProp]
        public string icon { get; set; }

        /// <summary>
        /// Position of the icon, valid values are "left", "right", "top" and "bottom".
        /// </summary>
        [ReactProp]
        public ButtonPositionType? iconPos { get; set; }

        /// <summary>
        ///Display loading icon of the button
        /// </summary>
        [ReactProp]
        public bool? loading { get; set; }

        [ReactProp]
        public bool? disabled { get; set; }
        

        /// <summary>
        /// Name of the loading icon or JSX.Element for loading icon.
        /// </summary>
        [ReactProp]
        public string loadingIcon { get; set; }

       



        [ReactProp]
        public string badge { get; set; }

        [ReactProp]
        public string badgeClassName { get; set; }

        [ReactProp]
        public bool? autoFocus { get; set; }

    }
}