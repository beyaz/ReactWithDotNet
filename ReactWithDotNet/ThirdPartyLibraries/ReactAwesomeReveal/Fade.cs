namespace ReactWithDotNet.ThirdPartyLibraries.ReactAwesomeReveal;

public class RevealBase : ThirdPartyReactComponent
{
    /// <summary>
    ///     Specifies if the animation should make element(s) disappear.<br />
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? reverse { get; set; }
    
    /// <summary>
    ///     <br />Stagger its children animations.
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? cascade { get; set; }

    /// <summary>
    ///     <br /> Class names to add to the child element.
    /// </summary>
    [ReactProp]
    public string childClassName { get; set; }

    /// <summary>
    ///     <br /> Inline styles to add to the child element.
    /// </summary>
    [ReactProp]
    public Style childStyle { get; set; }

    /// <summary>
    ///     <br /> Class names to add to the container element.
    /// </summary>
    [ReactProp]
    public string className { get; set; }

    /// <summary>
    ///     <br /> Factor that affects the delay that each animated element in a cascade animation will be assigned.
    ///     <br />  @default 0.5
    /// </summary>
    [ReactProp]
    public double? damping { get; set; }

    /// <summary>
    ///     <br /> Initial delay, in milliseconds.
    ///     <br /> @default 0
    /// </summary>
    [ReactProp]
    public double? delay { get; set; }

    /// <summary>
    ///     <br /> Animation duration, in milliseconds.
    ///     <br /> @default 1000
    /// </summary>
    [ReactProp]
    public double? duration { get; set; }

    /// <summary>
    ///     <br /> Float number between 0 and 1 indicating how much the element should be in viewport before the animation is
    ///     triggered.
    ///     <br /> @default 0
    /// </summary>
    [ReactProp]
    public double? fraction { get; set; }

    /// <summary>
    ///     <br /> Specifies if the animation should run only once or everytime the element enters/exits/re-enters the
    ///     viewport.
    ///     <br /> @default false
    /// </summary>
    [ReactProp]
    public bool? triggerOnce { get; set; }
}

public class AttentionSeeker : RevealBase
{
    /// <summary>
    ///     The animation effect to use for this attention seeker.
    ///     <br />
    ///     <br />"bounce" | "flash" | "headShake" | "heartBeat" | "jello" | "pulse" | "rubberBand" | "shake" | "shakeX" |
    ///     "shakeY" | "swing" | "tada" | "wobble"
    ///     <br />  @default "bounce"
    /// </summary>
    [ReactProp]
    public string effect { get; set; }
}

public class Bounce : RevealBase
{
    /// <summary>
    ///     <br /> Origin of the animation.
    ///     <br />"down" | "left" | "right" | "up"
    ///     <br />  @default undefined
    /// </summary>
    [ReactProp]
    public string direction { get; set; }

    
}

public class Fade : RevealBase
{
    
    /// <summary>
    ///     <br /> Causes the animation to start farther. Only works with "down", "left", "right" and "up" directions.
    ///     <br /> @default false
    /// </summary>
    [ReactProp]
    public bool? big { get; set; }

    /// <summary>
    ///     <br /> Origin of the animation.
    ///     <br /> @default undefined
    ///     <br /> "bottom-left" | "bottom-right" | "down" | "left" | "right" | "top-left" | "top-right" | "up"
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
}

public class Flip : RevealBase
{
    /// <summary>
    ///     <br /> Origin of the animation.
    ///     <br /> @default undefined
    ///     <br /> "horizontal" | "vertical"
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
    
}

public class Hinge : RevealBase
{
}

public class JackInTheBox : RevealBase
{
}

public class Rotate : RevealBase
{
    /// <summary>
    ///     <br /> Origin of the animation.
    ///     <br /> @default undefined
    ///     <br />  "bottom-left" | "bottom-right" | "top-left" | "top-right"
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
    
}

public class Slide : RevealBase
{
    /// <summary>
    ///     <br /> Origin of the animation.
    ///     <br /> @default undefined
    ///     <br />  "down" | "left" | "right" | "up"
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
    
}

public class Zoom : RevealBase
{
    /// <summary>
    ///     <br /> Origin of the animation.
    ///     <br /> @default undefined
    ///     <br />  "down" | "left" | "right" | "up"
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
    
}