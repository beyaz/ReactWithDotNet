namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact
{
    /// <summary>
    ///     The tooltip position type
    /// </summary>
    public enum TooltipPositionType
    {
        /// <summary>
        ///     The top
        /// </summary>
        top,

        /// <summary>
        ///     The bottom
        /// </summary>
        bottom,

        /// <summary>
        ///     The left
        /// </summary>
        left,

        /// <summary>
        ///     The right
        /// </summary>
        right
    }

    /// <summary>
    ///     The tooltip event type
    /// </summary>
    public enum TooltipEventType
    {
        /// <summary>
        ///     The hover
        /// </summary>
        hover,

        /// <summary>
        ///     The focus
        /// </summary>
        focus
    }

   

    /// <summary>
    ///     The tooltip options
    /// </summary>
    public sealed class TooltipOptions
    {
        /// <summary>
        ///     Style class of the tooltip.
        /// </summary>
        public string className { get; set; }

        /// <summary>
        ///     Position of the tooltip, valid values are right, left, top and bottom.
        ///     <para>Default: right</para>
        /// </summary>
        public TooltipPositionType? position { get; set; } 

        /// <summary>
        ///     Style of the tooltip.
        /// </summary>
        public readonly Style style = new Style();

        /// <summary>
        ///     Defines which position on the tooltip being positioned to align with the target element.
        /// </summary>
        public string my { get; set; }

        /// <summary>
        ///     Defines which position on the target element to align the positioned tooltip.
        /// </summary>
        public string at { get; set; }

        /// <summary>
        ///     Event to show the tooltip, valid values are hover and focus.
        /// </summary>
        public TooltipEventType? @event { get; set; }

        /// <summary>
        ///     Event to show the tooltip if the event property is empty.
        ///     <para>Default mouseenter</para>
        /// </summary>
        public string showEvent { get; set; }

        /// <summary>
        ///     Event to hide the tooltip if the event property is empty.
        ///     <para>Default mouseleave</para>
        /// </summary>
        public string hideEvent { get; set; }

        /// <summary>
        ///     Whether to automatically manage layering.
        /// </summary>
        public bool? autoZIndex { get; set; } 

        /// <summary>
        ///     Base zIndex value to use in layering.
        /// </summary>
        public int? baseZIndex { get; set; }

        /// <summary>
        ///     Whether the tooltip will follow the mouse.
        /// </summary>
        public bool? mouseTrack { get; set; }

        /// <summary>
        ///     Defines top position of the tooltip in relation to the mouse when the mouseTrack is enabled.
        /// </summary>
        public int? mouseTrackTop { get; set; }

        /// <summary>
        ///     Defines top position of the tooltip in relation to the mouse when the mouseTrack is enabled.
        /// </summary>
        public int? mouseTrackLeft { get; set; }

        /// <summary>
        ///     Delay to show the tooltip in milliseconds.
        /// </summary>
        public int? showDelay { get; set; }

        /// <summary>
        ///     Delay to update the tooltip in milliseconds.
        /// </summary>
        public int? updateDelay { get; set; }

        /// <summary>
        ///     Delay to hide the tooltip in milliseconds.
        /// </summary>
        public int? hideDelay { get; set; }

        /// <summary>
        ///     Whether to hide tooltip when hovering over tooltip content.
        /// <para>Default true</para>
        /// </summary>
        public bool? autoHide { get; set; } 

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="TooltipOptions" /> is disabled.
        /// </summary>
        public bool? disabled { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [show on disabled].
        /// </summary>
        public bool? showOnDisabled { get; set; }

    }
}