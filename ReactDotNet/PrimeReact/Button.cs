using System;
using System.Linq;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet.PrimeReact
{
    public class PrimeElementBase : Element
    {

        public override ReactElement ToReactElement()
        {
            UniqueKeyInitializer.InitializeKeyIfNotExists(children);

            var tag = GetType().Name;

            var targetType = PrimeReact.Helper.FindPrimeType(tag);

            return React.createElement(targetType, this.CollectReactAttributedProperties(), children.Select(x => x.ToReactElement()));
        }
        
    }

    [Enum(Emit.StringNameLowerCase)]
    public enum ButtonPositionType
    {
        top, bottom, left, right
    }

    public class Button : PrimeElementBase
    {
        public Button()
        {
            
        }

        public Button(string className)
        {
            this.className = className;
        }

        [React]
        public new Action<SyntheticEvent<HTMLElement>> onClick { get; set; }

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
        public ButtonPositionType? iconPos { get; set; }

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



      
        
        [React]
        public string badge { get; set; }

        [React]
        public string badgeClassName { get; set; }

        [React]
        public string tooltip { get; set; }

        [React]
        public TooltipOptions tooltipOptions { get; } = new TooltipOptions();

    }
}