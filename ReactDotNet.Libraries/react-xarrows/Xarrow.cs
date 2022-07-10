using ReactDotNet.Html5;

namespace ReactDotNet.react_xarrows
{
    public class Xarrow : ThirdPartyComponent
    {
        /// <summary>
        ///     required
        ///     can be a reference to a react ref to html element or string - an id of a DOM element.
        /// </summary>
        [React]
        public string start { get; set; }

        /// <summary>
        ///     required
        ///     can be a reference to a react ref to html element or string - an id of a DOM element.
        /// </summary>
        [React]
        public string end { get; set; }

        /// <summary>
        ///     "auto" | "middle" | "left" | "right" | "top" | "bottom"
        /// </summary>
        [React]
        public string startAnchor { get; set; }

        /// <summary>
        ///     "auto" | "middle" | "left" | "right" | "top" | "bottom"
        /// </summary>
        [React]
        public string endAnchor { get; set; }

        [React]
        public string labels { get; set; }

        /// <summary>
        ///     defines color to the entire arrow. lineColor,headColor and tailColor will override color specifically for line,tail
        ///     or head
        /// </summary>
        [React]
        public string color { get; set; }

        [React]
        public string headColor { get; set; }

        [React]
        public string tailColor { get; set; }

        [React]
        public string lineColor { get; set; }

        /// <summary>
        ///     defines the thickness of the entire arrow. headSize and tailSize defines how big will be the head or tail relative
        ///     to the strokeWidth
        /// </summary>
        [React]
        public int? strokeWidth { get; set; }

        [React]
        public int? headSize { get; set; }

        [React]
        public int? tailSize { get; set; }

        /// <summary>
        ///     can be one of: "smooth" | "grid" | "straight", and it controls the path arrow is drawn, exactly how their name
        ///     suggest
        /// </summary>
        [React]
        public string path { get; set; }

        /// <summary>
        ///     defines how much the lines curve. makes a difference only in path='smooth'
        /// </summary>
        [React]
        public double? curveness { get; set; }

        /// <summary>
        ///     can make the arrow dashed and can even animate. if true default values(for dashness) are chosen. if object is passed then default values are chosen except what passed.
        /// </summary>
        [React]
        public bool? dashness { get; set; }
        

        /// <summary>
        ///     can animate the drawing of the arrow using svg animation. type: boolean|number. if true animation duration is 1s.
        ///     if number is passed then animation duration is number's value in seconds
        ///     <example>animateDrawing={0.1} will animate the drawing of the arrow in 100 milliseconds.</example>
        /// </summary>
        [React]
        public double? animateDrawing { get; set; }

        /// <summary>
        /// you can customize the svg edges (head or tail) of the arrow. you can use predefined svg by passing string,one of "arrow1" | "circle" | "heart"
        /// </summary>
        [React]
        public string headShape { get; set; }

        /// <summary>
        /// you can customize the svg edges (head or tail) of the arrow. you can use predefined svg by passing string,one of "arrow1" | "circle" | "heart"
        /// </summary>
        [React]
        public string tailShape { get; set; }
    }
}