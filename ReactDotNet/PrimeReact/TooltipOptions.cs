using System;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet.PrimeReact
{
    [Enum(Emit.StringNameLowerCase)]
    public enum TooltipPositionType
    {
        top,
        bottom,
        left,
        right
    }

    [Enum(Emit.StringNameLowerCase)]
    public enum TooltipEventType
    {
        hover,
        focus
    }

    [External]
    [ObjectLiteral]
    public sealed class TooltipEventParams
    {
        public SyntheticEvent<HTMLElement> originalEvent;
        public HTMLElement target;
    }

    [ObjectLiteral]
    public sealed class TooltipOptions
    {
        /// <summary>
        /// Style class of the tooltip.
        /// </summary>
        public string className { get; set; }

        /// <summary>
        /// Position of the tooltip, valid values are right, left, top and bottom.
        /// <para>Default: right</para>
        /// </summary>
        public TooltipPositionType position { get; set; } = TooltipPositionType.right;

        /// <summary>
        /// Style of the tooltip.
        /// </summary>
        public CSSStyleDeclaration style { get; } = ObjectLiteral.Create<CSSStyleDeclaration>();

        /// <summary>
        /// Defines which position on the tooltip being positioned to align with the target element.
        /// </summary>
        public string my { get; set; }

        /// <summary>
        /// Defines which position on the target element to align the positioned tooltip.
        /// </summary>
        public string at { get; set; }

        /// <summary>
        /// Event to show the tooltip, valid values are hover and focus.
        /// </summary>
        public TooltipEventType @event { get; set; }

        /// <summary>
        /// Event to show the tooltip if the event property is empty.
        /// </summary>
        public string showEvent { get; set; }

        public string hideEvent { get; set; }

        public bool autoZIndex { get; set; }

        public int baseZIndex { get; set; }

        public bool mouseTrack { get; set; }

        public int mouseTrackTop { get; set; }
        public int mouseTrackLeft { get; set; }
        public int showDelay { get; set; }
        public int updateDelay { get; set; }
        public int hideDelay { get; set; }

        public bool autoHide { get; set; } = true;

        public bool disabled { get; set; }
        public bool showOnDisabled { get; set; }

        public Action<TooltipEventParams> onBeforeShow { get; set; }
        public Action<TooltipEventParams> onBeforeHide { get; set; }
        public Action<TooltipEventParams> onShow { get; set; }
        public Action<TooltipEventParams> onHide { get; set; }
    }
}