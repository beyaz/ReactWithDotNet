namespace ReactWithDotNet.react_xarrows
{
    public class Xarrow : ThirdPartyReactComponent
    {
        /// <summary>
        ///     required
        ///     can be a reference to a react ref to html element or string - an id of a DOM element.
        /// </summary>
        [ReactProp]
        public string start { get; set; }

        /// <summary>
        ///     required
        ///     can be a reference to a react ref to html element or string - an id of a DOM element.
        /// </summary>
        [ReactProp]
        public string end { get; set; }

        /// <summary>
        ///     "auto" | "middle" | "left" | "right" | "top" | "bottom"
        /// </summary>
        [ReactProp]
        public string startAnchor { get; set; }

        /// <summary>
        ///     "auto" | "middle" | "left" | "right" | "top" | "bottom"
        /// </summary>
        [ReactProp]
        public string endAnchor { get; set; }

        [ReactProp]
        public string labels { get; set; }

        /// <summary>
        ///     defines color to the entire arrow. lineColor,headColor and tailColor will override color specifically for line,tail
        ///     or head
        /// </summary>
        [ReactProp]
        public string color { get; set; }

        [ReactProp]
        public string headColor { get; set; }

        [ReactProp]
        public string tailColor { get; set; }

        [ReactProp]
        public string lineColor { get; set; }

        /// <summary>
        ///     defines the thickness of the entire arrow. headSize and tailSize defines how big will be the head or tail relative
        ///     to the strokeWidth
        /// </summary>
        [ReactProp]
        public double? strokeWidth { get; set; }

        [ReactProp]
        public int? headSize { get; set; }

        [ReactProp]
        public int? tailSize { get; set; }

        /// <summary>
        ///     can be one of: "smooth" | "grid" | "straight", and it controls the path arrow is drawn, exactly how their name
        ///     suggest
        /// </summary>
        [ReactProp]
        public string path { get; set; }

        /// <summary>
        ///     defines how much the lines curve. makes a difference only in path='smooth'
        /// </summary>
        [ReactProp]
        public double? curveness { get; set; }

        /// <summary>
        ///     can make the arrow dashed and can even animate. if true default values(for dashness) are chosen. if object is passed then default values are chosen except what passed.
        /// </summary>
        [ReactProp]
        public bool? dashness { get; set; }
        

        /// <summary>
        ///     can animate the drawing of the arrow using svg animation. type: boolean|number. if true animation duration is 1s.
        ///     if number is passed then animation duration is number's value in seconds
        ///     <example>animateDrawing={0.1} will animate the drawing of the arrow in 100 milliseconds.</example>
        /// </summary>
        [ReactProp]
        public double? animateDrawing { get; set; }

        /// <summary>
        /// you can customize the svg edges (head or tail) of the arrow. you can use predefined svg by passing string,one of "arrow1" | "circle" | "heart"
        /// </summary>
        [ReactProp]
        public string headShape { get; set; }

        /// <summary>
        /// you can customize the svg edges (head or tail) of the arrow. you can use predefined svg by passing string,one of "arrow1" | "circle" | "heart"
        /// </summary>
        [ReactProp]
        public string tailShape { get; set; }
    }
}