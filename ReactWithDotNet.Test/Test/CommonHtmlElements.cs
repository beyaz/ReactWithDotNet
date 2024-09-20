using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.String;

namespace ReactWithDotNet.Test;

[TestClass]
//[Ignore]
public class ExportingCommonHtmlElements
{
    [TestMethod]
    public void ExportCommonHtmlElements()
    {
        TagInfo[] map =
        {
            new()
            {
                Tag                  = "article",
                Comment              = "Specifies independent, self-contained content.",
                EnableCastFromString = true
            },

            new()
            {
                Tag = "button",
                Attributes =
                [
                    new ()
                    {
                        Name    = "type",
                        Type    = "string",
                        Comment = "Specifies the type of button. button, reset, submit"
                    },
                    new ()
                    {
                        Name    = "value",
                        Type    = "string",
                        Comment = "Specifies an initial value for the button"
                    },

                    new ()
                    {
                        Name    = "autofocus",
                        Comment = "Specifies that the button should have input focus when the page loads. Only one element in a document can have this attribute."
                    },
                    new ()
                    {
                        Name    = "disabled",
                        Comment = "Specifies that the button should be disabled. A disabled button cannot be clicked."
                    },
                    new ()
                    {
                        Name    = "form",
                        Comment = "Specifies the form that the button is associated with."
                    },
                    new ()
                    {
                        Name    = "formaction",
                        Comment = "Specifies the URL of the form action when the button is clicked."
                    },
                    new ()
                    {
                        Name    = "formenctype",
                        Comment = "Specifies the form encoding method (e.g., application/x-www-form-urlencoded, multipart/form-data) when the button is clicked."
                    },
                    new ()
                    {
                        Name    = "formmethod",
                        Comment = "Specifies the form submission method (e.g., GET, POST) when the button is clicked."
                    },
                    new ()
                    {
                        Name    = "formnovalidate",
                        Comment = "Specifies that the form should not be validated before submission when the button is clicked."
                    },
                    new ()
                    {
                        Name    = "name",
                        Comment = "Specifies a name for the button. The name attribute is used to reference form-data after the form has been submitted, or to reference the element in JavaScript."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag                  = "div",
                Comment              = Empty,
                EnableCastFromString = true
            },

            new()
            {
                Tag                  = "p",
                Comment              = "Defines a paragraph",
                EnableCastFromString = true
            },
            new()
            {
                Tag = "time",
                Attributes = 
                [
                    new ()
                    {
                        Name    = "datetime",
                        Comment = "Represent a machine-readable format of the 'time' element"
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag                  = "dl",
                Comment              = "Defines a description list."
            },
            new()
            {
                Tag                  = "dt",
                Comment              = "Defines a term/name in a description list.",
                EnableCastFromString = true
            },
            new()
            {
                Tag                  = "dd",
                Comment              = "Describe a term/name in a description list.",
                EnableCastFromString = true
            },
            new()
            {
                Tag                  = "kbd",
                Comment              = "tag is used to define keyboard input. The content inside is displayed in the browser's default monospace font.",
                EnableCastFromString = true
            },
            new()
            {
                Tag                  = "pre",
                Comment              = "Preformatted text",
                EnableCastFromString = true
            },
            new()
            {
                Tag                  = "code",
                Comment              = "Define some text as computer code in a document",
                EnableCastFromString = true
            },

            new()
            {
                Tag                  = "ol",
                Comment              = "Ordered list",
                EnableCastFromString = true
            },

            new()
            {
                Tag                  = "ul",
                Comment              = "Unordered (bulleted) list",
                EnableCastFromString = true
            },

            new()
            {
                Tag                  = "li",
                Comment              = "List item",
                EnableCastFromString = true
            },

            new() { Tag = "h1", Comment = Empty, EnableCastFromString = true },
            new() { Tag = "h2", Comment = Empty, EnableCastFromString = true },
            new() { Tag = "h3", Comment = Empty, EnableCastFromString = true },
            new() { Tag = "h4", Comment = Empty, EnableCastFromString = true },
            new() { Tag = "h5", Comment = Empty, EnableCastFromString = true },
            new() { Tag = "h6", Comment = Empty, EnableCastFromString = true },

            new() { Tag = "header", Comment = Empty, EnableCastFromString = true },

            new() { Tag = "span", Comment = "Inline container used to mark up a part of a text, or a part of a document.", EnableCastFromString = true },

            new() { Tag = "sup", Comment = "Superscript text", EnableCastFromString = true },

            new() { Tag = "sub", Comment = "Subscript text", EnableCastFromString = true },

            new() { Tag = "ins", Comment = "Inserted text", EnableCastFromString = true },

            new() { Tag = "del", Comment = "Deleted text", EnableCastFromString = true },

            new() { Tag = "small", Comment = "Smaller text", EnableCastFromString = true },

            new() { Tag = "mark", Comment = "Marked text", EnableCastFromString = true },

            new() { Tag = "em", Comment = "Emphasized text", EnableCastFromString = true },

            new() { Tag = "b", Comment = "Bold text", EnableCastFromString = true },

            new() { Tag = "i", Comment = "Italic text", EnableCastFromString = true },

            new()
            {
                Tag                  = "u",
                Comment              = "Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.",
                EnableCastFromString = true
            },

            new() { Tag = "strong", Comment = "Important text", EnableCastFromString = true },

            new() { Tag = "section", Comment = "Section in a document", EnableCastFromString = false },

            new() { Tag = "aside", EnableCastFromString = false },

            new() { Tag = "fieldset", EnableCastFromString = false },

            new() { Tag = "legend", EnableCastFromString = false },

            new() { Tag = "nav", EnableCastFromString = false },

            new() { Tag = "main", EnableCastFromString = false },

            new() { Tag = "footer", EnableCastFromString = false },

            new() { Tag = "figure", EnableCastFromString = false },

            new() { Tag = "hr", EnableCastFromString = false },

            new() { Tag = "figcaption", EnableCastFromString = true },

            new()
            {
                Tag = "table",
                Attributes =
                [
                    new ()
                    {
                        Name = "cellSpacing",
                        Type = "double?"
                    },
                    new ()
                    {
                        Name = "cellPadding",
                        Type = "double?"
                    }
                ],
                EnableCastFromString = false
            },

            new() { Tag = "thead", EnableCastFromString = false },
            new() { Tag = "tbody", EnableCastFromString = false },
            new() { Tag = "tfoot", EnableCastFromString = false },

            new()
            {
                Tag = "th",
                Attributes =
                [
                    new ()
                    {
                        Name = "colSpan",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "rowSpan",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "scope",
                        Type = "string",
                        Comment = "specifies whether a header cell is a header for a column, row, or group of columns or rows."
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag = "td",
                Attributes =
                [
                    new ()
                    {
                        Name = "colSpan",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "rowSpan",
                        Type = "int?"
                    }
                ],
                EnableCastFromString = true
            },
            new()
            {
                Tag = "tr",
                Attributes =
                [
                    new ()
                    {
                        Name = "colSpan",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "rowSpan",
                        Type = "int?"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "option",
                Attributes =
                [
                    new ()
                    {
                        Name = "selected",
                        Type = "bool?"
                    },
                    new ()
                    {
                        Name = "disabled",
                        Type = "string"
                    },
                    new ()
                    {
                        Name = "value",
                        Type = "string"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "ellipse",
                Attributes =
                [
                    new ()
                    {
                        Name    = "cx",
                        Comment = "The x-coordinate of the center of the ellipse."
                    },
                    new ()
                    {
                        Name    = "cy",
                        Comment = "The y-coordinate of the center of the ellipse."
                    },
                    new ()
                    {
                        Name    = "rx",
                        Comment = "The radius of the ellipse along the x-axis."
                    },
                    new ()
                    {
                        Name    = "ry",
                        Comment = "The radius of the ellipse along the y-axis."
                    },
                    new ()
                    {
                        Name    = "fill",
                        Comment = "The fill color of the ellipse."
                    },
                    new ()
                    {
                        Name    = "stroke",
                        Comment = "The stroke color of the ellipse."
                    },
                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the stroke.",
                        Type    = "UnionProp<string,double>"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "line",
                Attributes =
                [
                    new ()
                    {
                        Name    = "x1",
                        Comment = "The x-coordinate of the start point of the line.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "y1",
                        Comment = "The y-coordinate of the start point of the line.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "x2",
                        Comment = "The x-coordinate of the end point of the line.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "y2",
                        Comment = "The y-coordinate of the end point of the line.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "stroke",
                        Comment = "The stroke (outline) color of the line."
                    },
                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the line's outline.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "stroke-dasharray",
                        Comment = "Pattern of dashes and gaps used in the line's stroke."
                    },
                    new ()
                    {
                        Name    = "stroke-linecap",
                        Comment = "The style of the line's endpoints."
                    },
                    new ()
                    {
                        Name    = "stroke-linejoin",
                        Comment = "The style of the line's corners."
                    },
                    new ()
                    {
                        Name    = "stroke-opacity",
                        Comment = "The opacity (transparency) of the line's stroke."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "polyline",
                Attributes =
                [
                    new ()
                    {
                        Name    = "points",
                        Comment = "A list of points defining the vertices of the polyline."
                    },
                    new ()
                    {
                        Name    = "fill",
                        Comment = "The fill color of the polyline."
                    },
                    new ()
                    {
                        Name    = "stroke",
                        Comment = "The stroke (outline) color of the polyline."
                    },
                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the polyline's outline.",
                        Type    = "UnionProp<string,double>"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "circle",
                Attributes =
                [
                    new ()
                    {
                        Name    = "cx",
                        Comment = "The x-coordinate of the center of the circle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "cy",
                        Comment = "The y-coordinate of the center of the circle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "r",
                        Comment = "The radius of the circle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "fill",
                        Comment = "The fill color of the circle."
                    },
                    new ()
                    {
                        Name    = "stroke",
                        Comment = "The stroke color of the circle."
                    },
                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the stroke of the circle.",
                        Type = "UnionProp<string,double>"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "polygon",
                Attributes =
                [
                    new ()
                    {
                        Name    = "points",
                        Comment = "Specifies the coordinates of the polygon's vertices, in (x, y) pairs, separated by commas."
                    },

                    new ()
                    {
                        Name    = "fill",
                        Comment = "Specifies the fill color of the polygon."
                    },

                    new ()
                    {
                        Name    = "stroke",
                        Comment = "Specifies the stroke color of the polygon."
                    },

                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "Specifies the width of the polygon's stroke, in pixels.",
                        Type    = "UnionProp<string,double>"
                    },

                    new ()
                    {
                        Name    = "stroke-linecap",
                        Comment = "Specifies the type of line cap used for the polygon's stroke."
                    },

                    new ()
                    {
                        Name    = "stroke-linejoin",
                        Comment = "Specifies the type of line join used for the polygon's stroke."
                    },

                    new ()
                    {
                        Name    = "fill-rule",
                        Comment = "Specifies how the polygon is filled."
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag = "use",
                Attributes = [
                    new ()
                    {
                        Name    = "xlinkHref"
                    },
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag = "rect",
                Attributes =
                [
                    new ()
                    {
                        Name    = "x",
                        Comment = "The x-coordinate of the top-left corner of the rectangle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "y",
                        Comment = "The y-coordinate of the top-left corner of the rectangle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "width",
                        Comment = "The width of the rectangle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "height",
                        Comment = "The height of the rectangle.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "rx",
                        Comment = "The border radius of the rectangle on the horizontal axis.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "ry",
                        Comment = "The border radius of the rectangle on the vertical axis.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "fill",
                        Comment = "The fill color of the rectangle."
                    },
                    new ()
                    {
                        Name    = "stroke",
                        Comment = "The stroke color of the rectangle."
                    },
                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the rectangle's stroke.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "stroke-linecap",
                        Comment = "The linecap style of the rectangle's stroke."
                    },
                    new ()
                    {
                        Name    = "stroke-linejoin",
                        Comment = "The linejoin style of the rectangle's stroke."
                    }
                ],
                EnableCastFromString = false
            },
             
            new()
            {
                Tag = "marker",
                Attributes =
                [
                    new ()
                    {
                        Name    = "id",
                        Comment = "Defines a unique identifier (ID) for the marker element."
                    },
                    new ()
                    {
                        Name    = "markerHeight",
                        Comment = "Specifies the height of the marker viewport.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "markerUnits",
                        Comment = "Specifies the coordinate system for the marker width and height. Possible values are 'strokeWidth' or 'userSpaceOnUse'."
                    },
                    new ()
                    {
                        Name    = "markerWidth",
                        Comment = "Specifies the width of the marker viewport.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "orient",
                        Comment = "Defines the rotation angle for the marker, in degrees. Accepts 'auto', 'auto-start-reverse', or a specific angle."
                    },
                    new ()
                    {
                        Name    = "preserveAspectRatio",
                        Comment = "Indicates how the marker should scale its dimensions."
                    },
                    new ()
                    {
                        Name    = "refX",
                        Comment = "Defines the x-coordinate in the marker’s coordinate system.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "refY",
                        Comment = "Defines the y-coordinate in the marker’s coordinate system.",
                        Type    = "UnionProp<string,double>"
                    },
                    new ()
                    {
                        Name    = "viewBox",
                        Comment = "Specifies the position and dimension of the marker’s viewport."
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag = "radialGradient",
                Attributes =
                [
                    new ()
                    {
                        Name    = "cx",
                        Comment = "The x-coordinate of the center of the gradient."
                    },
                    new ()
                    {
                        Name    = "cy",
                        Comment = "The y-coordinate of the center of the gradient."
                    },
                    new ()
                    {
                        Name    = "fx",
                        Comment = "The x-coordinate of the focal point of the gradient."
                    },
                    new ()
                    {
                        Name    = "fy",
                        Comment = "The y-coordinate of the focal point of the gradient."
                    },
                    new ()
                    {
                        Name    = "r",
                        Comment = "The radius of the gradient."
                    },
                    new ()
                    {
                        Name    = "spreadMethod",
                        Comment = "The method used to spread the gradient."
                    },
                    new ()
                    {
                        Name    = "gradientUnits",
                        Comment = "The units used to specify the gradient."
                    },
                    new ()
                    {
                        Name    = "gradientTransform",
                        Comment = "A transform to apply to the gradient."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "clipPath",
                Attributes =
                [
                    new ()
                    {
                        Name    = "clip-rule",
                        Comment = "Specifies the fill rule for the clipping path."
                    },
                    new ()
                    {
                        Name    = "clip-box",
                        Comment = "Specifies the reference box for the clipping path."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "path",
                Attributes =
                [
                    new ()
                    {
                        Name    = "d",
                        Comment = "Path data"
                    },
                    new ()
                    {
                        Name    = "fill",
                        Comment = "Fill color"
                    },
                    new ()
                    {
                        Name    = "stroke",
                        Comment = "Stroke color"
                    },
                    new ()
                    {
                        Name    = "stroke-width",
                        Comment = "Stroke width",
                        Type    = "UnionProp<string,double>"
                    },

                    new ()
                    {
                        Name = "fillRule"
                    },
                    new ()
                    {
                        Name = "clipRule"
                    },
                    new ()
                    {
                        Name = "strokeLinecap"
                    },
                    new ()
                    {
                        Name = "strokeLinejoin"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "g",
                Attributes =
                [
                    new ()
                    {
                        Name = "opacity"
                    },

                    new ()
                    {
                        Name = "clipPath"
                    },
                    new ()
                    {
                        Name = "transform"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "mask",
                Attributes =
                [
                    new ()
                    {
                        Name    = "height",
                        Comment = "This attribute defines the height of the masking area. Value type: length ; Default value: 120%; Animatable: yes"
                    },

                    new ()
                    {
                        Name    = "maskContentUnits",
                        Comment = "This attribute defines the coordinate system for the contents of the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: userSpaceOnUse; Animatable: yes"
                    },
                    new ()
                    {
                        Name    = "maskUnits",
                        Comment = "This attribute defines the coordinate system for attributes x, y, width and height on the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: objectBoundingBox; Animatable: yes"
                    },

                    new ()
                    {
                        Name    = "x",
                        Comment = "This attribute defines the x-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes"
                    },
                    new ()
                    {
                        Name    = "y",
                        Comment = "This attribute defines the y-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes"
                    },
                    new ()
                    {
                        Name    = "width",
                        Comment = "This attribute defines the width of the masking area. Value type: 'length' ; Default value: 120%; Animatable: yes"
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "meta",
                Attributes =
                [
                    new ()
                    {
                        Name    = "charset",
                        Comment = "Specifies the character encoding of the document."
                    },
                    new ()
                    {
                        Name    = "http-equiv",
                        Comment = "Specifies the name of the HTTP header that the meta tag should be equivalent to."
                    },
                    new ()
                    {
                        Name    = "name",
                        Comment = "Specifies the name of the metadata property."
                    },
                    new ()
                    {
                        Name    = "content",
                        Comment = "Specifies the value of the metadata property."
                    },
                    new ()
                    {
                        Name    = "scheme",
                        Comment = "Specifies the URL scheme for the content attribute of the meta tag."
                    },
                    new ()
                    {
                        Name    = "itemprop",
                        Comment = "Specifies the Microdata item property that the meta tag represents."
                    },
                    new ()
                    {
                        Name    = "property",
                        Comment = "Specifies the schema.org property that the meta tag represents."
                    },
                    new ()
                    {
                        Name    = "src",
                        Comment = "Specifies the URL for a resource associated with the meta tag."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "body",
                Attributes =
                [
                    new ()
                    {
                        Name    = "background",
                        Comment = "Specifies the URL of a background image to be displayed behind the document's content."
                    },
                    new ()
                    {
                        Name    = "bgcolor",
                        Comment = "Specifies the background color of the document's body."
                    },
                    new ()
                    {
                        Name    = "link",
                        Comment = "Specifies the color of unvisited links in the document's body."
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag = "script",
                Attributes =
                [
                    new ()
                    {
                        Name    = "async",
                        Comment = "Specifies that the script should be executed asynchronously. This means that the browser will not wait for the script to finish executing before continuing to parse the rest of the HTML."
                    },
                    new ()
                    {
                        Name    = "defer",
                        Comment = "Specifies that the script should be executed after the browser has finished parsing the rest of the HTML. This is similar to async, but it ensures that scripts are executed in the order they are specified in the HTML."
                    },
                    new ()
                    {
                        Name    = "integrity",
                        Comment = "Specifies a subresource integrity (SRI) hash for the script. This helps to protect against man-in-the-middle attacks."
                    },
                    new ()
                    {
                        Name    = "language",
                        Comment = "Specifies the scripting language of the script. This is deprecated, but is still supported by most browsers."
                    },
                    new ()
                    {
                        Name    = "nomodule",
                        Comment = "Specifies that the script should be ignored if the browser does not support modules."
                    },
                    new ()
                    {
                        Name    = "src",
                        Comment = "Specifies the URL of an external script file."
                    },
                    new ()
                    {
                        Name    = "type",
                        Comment = "Specifies the type of the script. The most common value is application/javascript."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "title",
                Attributes =
                [
                    new ()
                    {
                        Name    = "language",
                        Comment = "Specifies the language of the title."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "head",
                Attributes =
                [
                    new ()
                    {
                        Name    = "profile",
                        Comment = "Provides a URL to a profile document for the current document."
                    },

                    new ()
                    {
                        Name    = "link",
                        Comment = "Provides a link to an external resource, such as a stylesheet or script file."
                    },
                    new ()
                    {
                        Name    = "meta",
                        Comment = "Provides metadata about the document, such as the character encoding, author, and keywords."
                    },

                    new ()
                    {
                        Name    = "script",
                        Comment = "Provides JavaScript code to be executed in the browser."
                    },
                    new ()
                    {
                        Name    = "noscript",
                        Comment = "Provides content to be displayed if the browser does not support JavaScript."
                    }
                    //new ()
                    //{
                    //    Name    = "base",
                    //    Comment = "Specifies the base URL for all relative URLs in the document.",
                    //}
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "html",
                Attributes =
                [
                    new ()
                    {
                        Name    = "hidden",
                        Comment = "Hides the element from display."
                    },

                    new ()
                    {
                        Name    = "manifest",
                        Comment = "Specifies the URL of a manifest file, which provides information about the web app."
                    },
                    new ()
                    {
                        Name         = "xmlns",
                        Comment      = "Specifies the namespace of the element.",
                        DefaultValue = "http://www.w3.org/1999/xhtml"
                    },
                    new ()
                    {
                        Name    = "prefix",
                        Comment = "Specifies the prefix of the element."
                    },
                    new ()
                    {
                        Name    = "version",
                        Comment = "Specifies the version of the HTML specification to which the element conforms."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag                  = "label",
                EnableCastFromString = true,
                Attributes =
                [
                    new ()
                    {
                        Name    = "htmlFor",
                        Comment = "Specifies which form element a label is bound to."
                    },

                    new ()
                    {
                        Name    = "dropzone",
                        Comment = "Specifies whether the element is a drop target."
                    },
                    new ()
                    {
                        Name    = "hidden",
                        Comment = "Hides the element from view."
                    },

                    new ()
                    {
                        Name    = "tabindex",
                        Comment = "Specifies the element's position in the tab order."
                    }
                ]
            },

            new()
            {
                Tag = "a",
                Attributes =
                [
                    new ()
                    {
                        Name    = "href",
                        Comment = "The URL of the linked resource."
                    },
                    new ()
                    {
                        Name    = "target",
                        Comment = "Specifies where the linked resource should be opened. Can be `_blank`, `_self`, `_parent`, or `_top`."
                    },
                    new ()
                    {
                        Name    = "rel",
                        Comment = "Specifies the relationship between the current document and the linked resource. Can be `alternate`, `author`, `bookmark`, `canonical`, `external`, `help`, `license`, `next`, `nofollow`, `noreferrer`, `noopener`, `prev`, `search`, `sponsored`, or `stylesheet`."
                    },
                    new ()
                    {
                        Name    = "type",
                        Comment = "Specifies the MIME type of the linked resource, if applicable."
                    },
                    new ()
                    {
                        Name    = "download",
                        Comment = "Specifies whether the linked resource should be downloaded or opened in a new browser tab."
                    },
                    new ()
                    {
                        Name    = "ping",
                        Comment = "A list of URLs to which a ping should be sent when the user clicks on the link."
                    },
                    new ()
                    {
                        Name    = "media",
                        Comment = "Specifies the media types for which the link is relevant."
                    },
                    new ()
                    {
                        Name    = "hreflang",
                        Comment = "Specifies the language of the linked resource."
                    },
                    new ()
                    {
                        Name    = "name",
                        Comment = "Specifies a name for the link. This can be used to target the link with JavaScript."
                    },

                    new ()
                    {
                        Name    = "tabindex",
                        Comment = "Specifies the tab order of the link."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag = "img",
                Attributes =
                [
                    new ()
                    {
                        Name    = "src",
                        Comment = "The URL of the image file."
                    },
                    new ()
                    {
                        Name    = "srcset",
                        Comment = "A list of image files to use in different situations, such as different screen sizes or device types."
                    },
                    new ()
                    {
                        Name    = "usemap",
                        Comment = "Specifies an image as a client-side image map."
                    },
                    new ()
                    {
                        Name    = "alt",
                        Comment = "An alternate text for the image, if the image for some reason cannot be displayed."
                    },
                    new ()
                    {
                        Name    = "width",
                        Comment = "The width of the image, in pixels.",
                        Type    = "UnionProp<string,double?>"
                    },
                    new ()
                    {
                        Name    = "height",
                        Comment = "The height of the image, in pixels.",
                        Type    = "UnionProp<string,double?>"
                    },
                    new ()
                    {
                        Name    = "ismap",
                        Comment = "A Boolean attribute that indicates whether the image is an image map."
                    },
                    new ()
                    {
                        Name    = "longdesc",
                        Comment = "A longer description of the image, for use by screen readers and other assistive technologies."
                    },
                    new ()
                    {
                        Name    = "crossorigin",
                        Comment = "A string that specifies the CORS setting for the image."
                    },
                    new ()
                    {
                        Name    = "loading",
                        Comment = "A string that specifies how the image should be loaded."
                    },
                    new ()
                    {
                        Name    = "decoding",
                        Comment = "A string that specifies how the image should be decoded."
                    },
                    new ()
                    {
                        Name    = "referrerpolicy",
                        Comment = "A string that specifies how much referrer information is sent with requests for the image."
                    }
                ],
                EnableCastFromString = false
            },

            new()
            {
                Tag                  = "svg",
                CreateClassAsPartial = true,
                Attributes =
                [
                    new ()
                    {
                        Name = "focusable"
                    },
                    new ()
                    {
                        Name = "xlinkHref"
                    },
                    new ()
                    {
                        Name = "xmlnsXlink"
                    },
                    new ()
                    {
                        Name    = "preserveAspectRatio",
                        Comment = "Specifies how the SVG element should be scaled and aligned to fit its viewport."
                    },

                    new ()
                    {
                        Name    = "width",
                        Comment = "The width of the SVG element in pixels."
                    },

                    new ()
                    {
                        Name    = "height",
                        Comment = "The height of the SVG element in pixels."
                    },

                    new ()
                    {
                        Name         = "xmlns",
                        Comment      = "The namespace URI for the SVG element.",
                        DefaultValue = "http://www.w3.org/2000/svg"
                    },

                    new ()
                    {
                        Name    = "version",
                        Comment = "The SVG version of the element."
                    },
                    new ()
                    {
                        Name = "viewBox"
                    },
                    new ()
                    {
                        Name = "fill"
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag                  = "symbol",
                CreateClassAsPartial = true,
                Attributes =
                [
                    new ()
                    {
                        Name    = "viewBox",
                        Comment = "Defines the position and dimension, in user space, of an SVG viewport."
                    },
                    new ()
                    {
                        Name    = "preserveAspectRatio",
                        Comment = "Indicates how an element should preserve its aspect ratio when the viewBox is different from the viewport."
                    },
                    new ()
                    {
                        Name    = "externalResourcesRequired",
                        Comment = "Specifies whether the rendering of the <symbol> element is dependent on external resources."
                    },
                    new ()
                    {
                        Name    = "transform",
                        Comment = "Applies a transformation to the element, such as scaling, rotating, or translating."
                    }
                    //new ()
                    //{
                    //    Name    = "xml:base",
                    //    Comment = "Specifies the base URI for resolving relative URIs within the element."
                    //},
                    //new ()
                    //{
                    //    Name    = "xml:lang",
                    //    Comment = "Defines the language of the content within the <symbol> element."
                    //},
                    //new ()
                    //{
                    //    Name    = "xml:space",
                    //    Comment = "Controls how white space is handled inside the element."
                    //}

                ],
                EnableCastFromString = false
            },
            
            new()
            {
                Tag                  = "video",
                CreateClassAsPartial = true,
                Attributes =
                [
                    new()
                    {
                        Name    = "src",
                        Comment = "Specifies the URL of the video file."
                    },
                    new()
                    {
                        Name    = "controls",
                        Comment = "Specifies that video controls (play, pause, etc.) should be displayed."
                    },
                    new()
                    {
                        Name    = "autoplay",
                        Comment = "Specifies that the video will start playing as soon as it is ready."
                    },
                    new()
                    {
                        Name    = "loop",
                        Comment = "Specifies that the video will start over again, every time it is finished."
                    },
                    new()
                    {
                        Name    = "muted",
                        Comment = "Specifies that the audio output of the video should be muted."
                    },
                    new()
                    {
                        Name    = "poster",
                        Comment = "Specifies an image to be shown while the video is downloading or until the user hits the play button."
                    },
                    new()
                    {
                        Name    = "preload",
                        Comment = "Specifies how the video should be loaded when the page loads. Values include 'auto', 'metadata', or 'none'."
                    },
                    new()
                    {
                        Name    = "width",
                        Comment = "Sets the width of the video player."
                    },
                    new()
                    {
                        Name    = "height",
                        Comment = "Sets the height of the video player."
                    },
                    new()
                    {
                        Name    = "playsinline",
                        Comment = "Specifies that the video should play inline on mobile devices instead of going fullscreen."
                    },
                    new()
                    {
                        Name    = "crossorigin",
                        Comment = "Specifies how the element handles cross-origin requests for the video."
                    }
                ],
                EnableCastFromString = false
            },
             new()
            {
                Tag                  = "source",
                CreateClassAsPartial = true,
                Attributes =
                [
                    new()
                    {
                        Name    = "src",
                        Comment = "Specifies the URL of the media file."
                    },
                    new()
                    {
                        Name    = "type",
                        Comment = "Specifies the MIME type of the media resource."
                    },
                    new()
                    {
                        Name    = "srcset",
                        Comment = "Specifies a list of image sources for responsive images."
                    },
                    new()
                    {
                        Name    = "sizes",
                        Comment = "Specifies the sizes of images for different viewport widths."
                    },
                    new()
                    {
                        Name    = "media",
                        Comment = "Specifies the media condition that must be met for the resource to be used."
                    },
                    new()
                    {
                        Name    = "width",
                        Comment = "Specifies the width of the image for the srcset attribute."
                    },
                    new()
                    {
                        Name    = "height",
                        Comment = "Specifies the height of the image for the srcset attribute."
                    },
                    new()
                    {
                        Name    = "keytype",
                        Comment = "Specifies the type of key for media encryption (used in some DRM scenarios)."
                    },
                    new()
                    {
                        Name    = "referrerpolicy",
                        Comment = "Specifies the referrer information to send when fetching the resource."
                    }

                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag                  = "stop",
                CreateClassAsPartial = false,
                Attributes =
                [
                    new ()
                    {
                        Name = "offset"
                    },
                    new ()
                    {
                        Name = "stopColor"
                    },
                    new ()
                    {
                        Name = "stopOpacity"
                    }
                ],
                EnableCastFromString = false
            },
            
            new()
            {
                Tag                  = "linearGradient",
                CreateClassAsPartial = false,
                Attributes = [],
                EnableCastFromString = false
            },
            new()
            {
                Tag                  = "noscript",
                CreateClassAsPartial = false,
                Attributes           = [],
                EnableCastFromString = false
            },
            new()
            {
                Tag                  = "defs",
                CreateClassAsPartial = false,
                Attributes           = [],
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "form",
                CreateClassAsPartial = false,
                Attributes =
                [
                    new ()
                    {
                        Name    = "action",
                        Comment = "Specifies the URL of the page where the form data will be submitted."
                    },

                    new ()
                    {
                        Name    = "method",
                        Comment = "Specifies how the form data will be sent to the server. Possible values are 'get' and 'post'."
                    },

                    new ()
                    {
                        Name    = "enctype",
                        Comment = "Specifies the encoding type for form data. Possible values are 'application/x-www-form-urlencoded' and 'multipart/form-data'."
                    },

                    new ()
                    {
                        Name    = "target",
                        Comment = "Specifies the name of the frame where the form will be submitted. The default value is '_self', which means the form will be submitted in the current frame."
                    },

                    new ()
                    {
                        Name    = "name",
                        Comment = "Specifies a name for the form. This name is used to reference the form in JavaScript or to reference form data after a form is submitted."
                    },

                    new ()
                    {
                        Name    = "novalidate",
                        Comment = "Disables form validation. This attribute is useful when you want to submit the form without validating the user input."
                    },

                    new ()
                    {
                        Name    = "autocomplete",
                        Comment = "Specifies whether the browser should automatically fill in form fields based on the user's past input."
                    }
                ],
                EnableCastFromString = false
            },
            
             new()
            {
                Tag = "textarea",
                Attributes =
                [
                    new ()
                    {
                        Name    = "name",
                        Comment = "Specifies a name for the textarea element."
                    },
                    new ()
                    {
                        Name    = "cols",
                        Comment = "Specifies the visible width of the textarea element in characters.",
                        Type = "UnionProp<string,int?>"
                    },
                    new ()
                    {
                        Name    = "rows",
                        Comment = "Specifies the number of visible lines in the textarea element.",
                        Type    = "UnionProp<string,int?>"
                    },
                    new ()
                    {
                        Name    = "placeholder",
                        Comment = "Specifies a short hint that describes the expected value of the textarea element."
                    },
                    new ()
                    {
                        Name    = "readOnly",
                        Comment = "Disables user input in the textarea element.",
                        Type    = "UnionProp<string,bool>"
                    },
                    new ()
                    {
                        Name    = "required",
                        Comment = "Indicates that the textarea element must be filled out before the form is submitted."
                    },
                    new ()
                    {
                        Name    = "autofocus",
                        Comment = "Automatically gives focus to the textarea element when the page loads."
                    },
                    new ()
                    {
                        Name    = "autocomplete",
                        Comment = "Specifies that the user's browser should automatically complete the textarea element's value."
                    },
                    new ()
                    {
                        Name    = "dirname",
                        Comment = "Specifies the directory to use as the default value for the 'file' input type."
                    },
                    new ()
                    {
                        Name    = "form",
                        Comment = "Specifies the ID of the form that the textarea element belongs to."
                    },
                    new ()
                    {
                        Name    = "maxlength",
                        Comment = "Specifies the maximum number of characters that can be entered into the textarea element."
                    },
                    new ()
                    {
                        Name    = "minlength",
                        Comment = "Specifies the minimum number of characters that must be entered into the textarea element."
                    },
                    new ()
                    {
                        Name    = "wrap",
                        Comment = "Specifies whether the text in the textarea element should wrap to the next line when it reaches the end of the visible area."
                    },
                    new ()
                    {
                        Name    = "defaultValue",
                        Comment = "A string. Specifies the initial value for a text area."
                    },
                    new ()
                    {
                        Name    = "value"
                    },
                    new ()
                    {
                        Name = "disabled"
                    },
                    
                    new ()
                    {
                        Name                              = "onBlur",
                        Type                              = "FocusEventHandler",
                        IsIsVoidTaskDelegate              = true,
                        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticFocusEventArguments",
                        Comment                           = "Occurs when an element loses focus."
                    },
                    
                    new ()
                    {
                        Name                = "valueBind",
                        Type                = "Expression<Func<InputValueBinder>>",
                        IsBindingExpression = true,
                        TransformValueInClient = "ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull",
                        Bind =new ()
                        {
                            targetProp = "value", jsValueAccess = "e.target.value", eventName = "onChange"
                        }
                    },
                    
                    new ()
                    {
                        Name = "valueBindDebounceTimeout",
                        Type = "int?",
                        Comment ="""
                                 if you want to handle when user iteraction finished see example below
                                 component.valueBind = ()=>state.UserInfo.Name
                                 component.valueBindDebounceTimeout = 600 // milliseconds
                                 component.valueBindDebounceHandler = OnUserIterationFinished
                                 """,
                        JsonIgnore = true
                    },
                    
                    new ()
                    {
                        Name = "valueBindDebounceHandler",
                        Type = "Func<Task>",
                        Comment ="""
                                 if you want to handle when user iteraction finished see example below
                                 component.valueBind = ()=>state.UserInfo.Name
                                 component.valueBindDebounceTimeout = 600 // milliseconds
                                 component.valueBindDebounceHandler = OnUserIterationFinished
                                 """ ,
                        JsonIgnore = true
                    }
                ],
                EnableCastFromString = false
            },
             
            new()
            {
                Tag                  = "link",
                CreateClassAsPartial = false,
                Attributes           = [
                    new ()
                    {
                        Name = "href"
                    },
                    new ()
                    {
                        Name = "media"
                    },
                    new ()
                    {
                        Name = "rel"
                    },
                    new ()
                    {
                        Name = "sizes"
                    },
                    new ()
                    {
                        Name = "type"
                    },
                    new ()
                    {
                        Name = "@as"
                    },
                    new ()
                    {
                        Name = "integrity"
                    },
                    new ()
                    {
                        Name = "crossOrigin"
                    },
                    new ()
                    {
                        Name = "referrerpolicy"
                    }
                ],
                EnableCastFromString = false
            },
            
            new()
            {
                Tag                  = "iframe",
                CreateClassAsPartial = false,
                Attributes = [
                    new ()
                    {
                        Name    = "src",
                        Comment = "Specifies the URL of the document to embed in the iframe"
                    },
                    new ()
                    {
                        Name    = "srcDoc",
                        Comment = "Provides the HTML content to be embedded directly within the iframe"
                    },
                    new ()
                    {
                        Name    = "name",
                        Comment = "Assigns a name to the iframe, which can be used for targeting links"
                    },
                    new ()
                    {
                        Name    = "width",
                        Comment = "Defines the width of the iframe"
                    },
                    new ()
                    {
                        Name    = "height",
                        Comment = "Defines the height of the iframe"
                    },
                    new ()
                    {
                        Name    = "sandbox",
                        Comment = "Enables an extra set of restrictions for the content in the iframe"
                    },
                    new ()
                    {
                        Name    = "allow",
                        Comment = "Controls features within the iframe, such as fullscreen, camera, microphone, etc."
                    },
                    new ()
                    {
                        Name    = "allowFullScreen",
                        Comment = "Allows the iframe to display content in fullscreen mode"
                    },
                    new ()
                    {
                        Name    = "referrerPolicy",
                        Comment = "Controls how much referrer information should be included with requests"
                    },
                    new ()
                    {
                        Name    = "loading",
                        Comment = "Specifies whether the iframe should be loaded lazily or eagerly"
                    },
                   
                    
                    new ()
                    {
                        Name    = "csp",
                        Comment = "Adds a Content Security Policy for the iframe's content"
                    },
                    new ()
                    {
                        Name    = "title",
                        Comment = "Provides advisory information about the content of the iframe"
                    },
                    new ()
                    {
                        Name    = "hidden",
                        Comment = "Hides the iframe"
                    }

                ],
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "select",
                
                CreateClassAsPartial = false,
                Attributes = [
                    new ()
                    {
                        Name = "value"
                    },
                    new ()
                    {
                        Name = "disabled"
                    },
                    new ()
                    {
                        Name                = "valueBind",
                        Type                = "Expression<Func<InputValueBinder>>",
                        IsBindingExpression = true,
                        Bind =new ()
                        {
                            targetProp = "value", jsValueAccess = "e.target.value", eventName = "onChange"
                        }
                    },
                    
                    new ()
                    {
                        Name                              = "onChange",
                        Type                              = "ChangeEventHandler",
                        IsIsVoidTaskDelegate              = true,
                        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments"
                    },
                    
                    new ()
                    {
                        Name = "valueBindDebounceTimeout",
                        Type = "int?",
                        Comment ="""
                                 if you want to handle when user iteraction finished see example below
                                 component.valueBind = ()=>state.UserInfo.Name
                                 component.valueBindDebounceTimeout = 600 // milliseconds
                                 component.valueBindDebounceHandler = OnUserIterationFinished
                                 """ ,
                        JsonIgnore = true
                    },
                    
                    new ()
                    {
                        Name = "valueBindDebounceHandler",
                        Type = "Func<Task>",
                        Comment ="""
                                 if you want to handle when user iteraction finished see example below
                                 component.valueBind = ()=>state.UserInfo.Name
                                 component.valueBindDebounceTimeout = 600 // milliseconds
                                 component.valueBindDebounceHandler = OnUserIterationFinished
                                 """ ,
                        JsonIgnore = true
                    }
                ],
                EnableCastFromString = false
            },
            new()
            {
                Tag = "input",
                
                Attributes = [
                    new ()
                    {
                        Name = "required"
                    },
                    new ()
                    {
                        Name = "autoComplete"
                    },
                    new ()
                    {
                        Name = "@checked",
                        Type = "bool?"
                    },
                    new ()
                    {
                        Name = "defaultChecked",
                        Type = "bool?"
                    },
                    new ()
                    {
                    Name = "defaultValue"
                    },
                    new ()
                    {
                        Name = "disabled",
                        Type = "bool?"
                    },
                    new ()
                    {
                        Name = "autoFocus",
                        Type = "bool?",
                        Comment = "Element must automatically get focus when the page loads."
                    },
                    new ()
                    {
                        Name = "name"
                    },
                    new ()
                    {
                        Name = "placeholder"
                    },
                    new ()
                    {
                        Name = "readOnly",
                        Type = "bool?"
                    },
                    new ()
                    {
                        Name = "type"
                    },
                    new ()
                    {
                        Name = "max",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "maxLength",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "min",
                        Type = "int?"
                    },
                    new ()
                    {
                        Name = "step",
                        Type = "int?"
                    },
                    
                    new ()
                    {
                        Name                   = "valueBind",
                        Type                   = "Expression<Func<InputValueBinder>>",
                        IsBindingExpression    = true,
                        TransformValueInClient = "ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull",
                        Bind =new ()
                        {
                            targetProp = "value", jsValueAccess = "e.target.value", eventName = "onChange"
                        }
                    },
                    
                    new ()
                    {
                        Name                   = "valueBindDebounceTimeout",
                        Type                   = "int?",
                        Comment ="""
                                 if you want to handle when user iteraction finished see example below
                                 component.valueBind = ()=>state.UserInfo.Name
                                 component.valueBindDebounceTimeout = 600 // milliseconds
                                 component.valueBindDebounceHandler = OnUserIterationFinished
                                 """ ,
                        JsonIgnore = true
                    },
                    
                    new ()
                    {
                        Name = "valueBindDebounceHandler",
                        Type = "Func<Task>",
                        Comment ="""
                                 if you want to handle when user iteraction finished see example below
                                 component.valueBind = ()=>state.UserInfo.Name
                                 component.valueBindDebounceTimeout = 600 // milliseconds
                                 component.valueBindDebounceHandler = OnUserIterationFinished
                                 """ ,
                        JsonIgnore = true
                    },
                    
                    new ()
                    {
                        Name                = "checkedBind",
                        Type                = "Expression<Func<bool>>",
                        IsBindingExpression = true,
                        Bind =new ()
                        {
                            targetProp = "checked", jsValueAccess = "e.target.value", eventName = "onChange"
                        }
                    },

                    
                    new ()
                    {
                        Name                   = "value",
                        Type                   = "string",
                        TransformValueInClient = "ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull"
                    },
                    
                    new ()
                    {
                        Name                 = "onBlur",
                        Type                 = "FocusEventHandler",
                        IsIsVoidTaskDelegate = true,
                        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticFocusEventArguments",
                        Comment = "Occurs when an element loses focus."
                    },
                    new ()
                    {
                        Name                              = "onChange",
                        Type                              = "ChangeEventHandler",
                        IsIsVoidTaskDelegate              = true,
                        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments"
                    },
                    new ()
                    {
                        Name                              = "onFocus",
                        Type                              = "FocusEventHandler",
                        IsIsVoidTaskDelegate              = true,
                        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticFocusEventArguments"
                    }
                ],
                EnableCastFromString = false
            },
            
            
            new()
            {
                Tag = "HtmlElement",
                
                CreateClassAsPartial = true,
                Attributes = [
                    new ()
                    {
                        Name = "accesskey"
                    },
                    new ()
                    {
                        Name = "draggable"
                    },
                    new ()
                    {
                        Name = "contentEditable",
                        Type = "UnionProp<string,bool>"
                    },
                    new ()
                    {
                        Name = "suppressContentEditableWarning",
                        Type = "bool?"
                    },
                    
                    new ()
                    {
                        Name = "className"
                    },
                    new ()
                    {
                        Name = "dangerouslySetInnerHTML",
                        Type = "dangerouslySetInnerHTML"
                    },
                    new ()
                    {
                        Name = "dir"
                    },
                    new ()
                    {
                        Name = "id"
                    },
                    new ()
                    {
                        Name = "lang"
                    },
                    new ()
                    {
                        Name = "part"
                    },
                    new ()
                    {
                        Name = "role"
                    },
                    new ()
                    {
                        Name = "spellcheck"
                    },
                    new ()
                    {
                        Name = "tabIndex"
                    },
                    new ()
                    {
                        Name = "title"
                    },
                    new ()
                    {
                        Name = "translate"
                    },
                    new ()
                    {
                        Name                              = "onClick",
                        Type                              = "MouseEventHandler",
                        GrabEventArgumentsByUsingFunction ="ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments",
                        IsIsVoidTaskDelegate              = true,
                    },
                    new ()
                    {
                        Name                              = "onMouseEnter",
                        Type                              = "MouseEventHandler",
                        GrabEventArgumentsByUsingFunction ="ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments",
                        IsIsVoidTaskDelegate              = true,
                    },
                    new ()
                    {
                        Name                              = "onMouseLeave",
                        Type                              = "MouseEventHandler",
                        GrabEventArgumentsByUsingFunction ="ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments",
                        IsIsVoidTaskDelegate              = true,
                    },
                    new ()
                    {
                        Name = "onScroll",
                        Type = "ScrollEventHandler",
                        IsIsVoidTaskDelegate = true
                    },
                    new ()
                    {
                        Name                 = "onKeyDown",
                        Type                 = "KeyboardEventHandler",
                        IsIsVoidTaskDelegate = true,
                    },
                    new ()
                    {
                        Name                              = "onInput",
                        Type                              = "ChangeEventHandler",
                        IsIsVoidTaskDelegate              = true,
                        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments"
                    }
                ],
                EnableCastFromString = false
            },
        };

        var list = new List<string>
        {
            "namespace ReactWithDotNet;"
        };

        const string padding = "    ";

        foreach (var item in map)
        {
            addComment(null);

            var isRootHtmlElement = item.Tag == "HtmlElement";
            
            var partialModifier = "";
            if (item.CreateClassAsPartial)
            {
                partialModifier = " partial";
            }

            var inheritPart = " : HtmlElement";
            
            if (isRootHtmlElement)
            {
                inheritPart = "";
            }

            var sealedModifier = "sealed";
            if (isRootHtmlElement)
            {
                sealedModifier = "";
            }
            
            
            list.Add($"public {sealedModifier}{partialModifier} class {item.Tag}{inheritPart}");
            list.Add("{");

            if (item.Attributes is not null)
            {
                foreach (var attribute in item.Attributes)
                {
                    
                    list.Add($"{padding}#region string {CamelCase(attribute.Name)}");
                    list.Add($"{padding}PropertyValueNode<{attribute.Type}> _{CamelCase(attribute.Name).RemoveFromStart("@")};");
                    
                    
                    list.Add($"{padding}static readonly PropertyValueDefinition _{CamelCase(attribute.Name).RemoveFromStart("@")}_ = new()");
                    list.Add($"{padding}{{");
                    list.Add($"{padding}{padding}name = nameof({CamelCase(attribute.Name)})");
                    
                    if (attribute.GrabEventArgumentsByUsingFunction is not null)
                    {
                        list[^1] += ",";
                        list.Add($"{padding}{padding}GrabEventArgumentsByUsingFunction = \"{attribute.GrabEventArgumentsByUsingFunction}\"");
                    }
                    if (attribute.IsIsVoidTaskDelegate)
                    {
                        list[^1] += ",";
                        list.Add($"{padding}{padding}isIsVoidTaskDelegate = true");
                    }
                    if (attribute.IsBindingExpression)
                    {
                        list[^1] += ",";
                        list.Add($"{padding}{padding}isBindingExpression = true");
                    }
                    if (attribute.TransformValueInClient is not null)
                    {
                        list[^1] += ",";
                        list.Add($"{padding}{padding}transformValueInClient = \"{attribute.TransformValueInClient}\"");
                    }
                    
                    if (attribute.Bind is not null)
                    {
                        list[^1] += ",";
                        list.Add($"{padding}{padding}bind = new(){{ targetProp = \"{attribute.Bind.targetProp}\", jsValueAccess = \"{attribute.Bind.jsValueAccess}\", eventName = \"{attribute.Bind.eventName}\" }}");
                    }
                    
                    if (attribute.JsonIgnore)
                    {
                        list[^1] += ",";
                        list.Add($"{padding}{padding}jsonIgnore = true");
                    }
                    
                    
                    
                    
                    list.Add($"{padding}}};");
                    
                    
                    
                    
                    if (IsNullOrWhiteSpace(attribute.Comment) == false)
                    {
                        list.Add($"{padding}/// <summary>");
                        
                        foreach (var line in attribute.Comment.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries))
                        {
                            list.Add($"{padding}///     {line}");
                        }
                        
                        list.Add($"{padding}/// </summary>");
                    }

                    // todo: default value only xmlns fix in constructor
                    //var partDefaultValueAssignment = "";
                    //if (attribute.DefaultValue is not null)
                    //{
                    //    partDefaultValueAssignment = $" ?? \"{attribute.DefaultValue}\";";
                    //}

                    list.Add($"{padding}public {attribute.Type} {CamelCase(attribute.Name)}");
                    list.Add($"{padding}{{");
                    list.Add($"{padding}{padding}get => _{CamelCase(attribute.Name).RemoveFromStart("@")}?.value;");
                    list.Add($"{padding}{padding}set => SetValue(_{CamelCase(attribute.Name).RemoveFromStart("@")}_, ref _{CamelCase(attribute.Name).RemoveFromStart("@")}, value);");
                    list.Add($"{padding}}}");
                    list.Add($"{padding}#endregion");
                    list.Add(Empty);
                    list.Add(Empty);
                    
                    
                    
                    
                    
                    // O L D
                    //list.Add("    [ReactProp]");

                    //if (attribute.GrabEventArgumentsByUsingFunction is not null)
                    //{
                    //    list.Add($"    [ReactGrabEventArgumentsByUsingFunction(\"{attribute.GrabEventArgumentsByUsingFunction}\")]");
                    //}
                    
                    //list.Add($"    public {attribute.Type} {CamelCase(attribute.Name)} {{ get; set; }}{partDefaultValueAssignment}");
                    //list.Add(Empty);
                }
            }



            if (item.Tag !="HtmlElement")
            {
                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(string className) : base(className) {{  }}");
                
                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(string className, params Modifier[] modifiers) : base(className, modifiers) {{  }}");    
            }
            

            if (item.EnableCastFromString)
            {
                list.Add(Empty);
                addComment();
                list.Add($"    public static implicit operator {item.Tag}(string text) => new() {{ text = text }};");
            }
            
            if (!isRootHtmlElement)
            {
                addComment();
                list.Add($"    public {item.Tag}() {{ }}");

                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(params Modifier[] modifiers) : base(modifiers) {{ }}");
                
                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(Style style) : base(style) {{ }}");

                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(StyleModifier[] styleModifiers) : base(styleModifiers) {{ }}");
            }

           

            list.Add(Empty);
            list.Add($"    public static HtmlElementModifier Modify(Action<{item.Tag}> modifyAction) => CreateHtmlElementModifier(modifyAction);");

            if (item.Attributes is not null)
            {
                foreach (var attribute in item.Attributes)
                {
                    if (IsNullOrWhiteSpace(attribute.Comment) == false)
                    {
                        list.Add($"{padding}/// <summary>");
                        list.Add($"{padding}///     {CamelCase(attribute.Name)} = <paramref name=\"value\"/>");
                        list.Add($"{padding}/// <br/>");
                        
                        foreach (var line in attribute.Comment.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries))
                        {
                            list.Add($"{padding}///     {line}");
                        }
                        
                        list.Add($"{padding}/// </summary>");
                    }

                    list.Add($"    public static HtmlElementModifier {UpperCaseFirstChar(CamelCase(attribute.Name.RemoveFromStart("@")))}({attribute.Type} value) => Modify(x => x.{CamelCase(attribute.Name)} = value);");
                    list.Add(Empty);
                }
            }

            list.Add("}");

            list.Add(Empty);

            void addComment(string space = "    ")
            {
                if (IsNullOrWhiteSpace(item.Comment) == false)
                {
                    list.Add($"{space}/// <summary>");
                    
                    foreach (var line in item.Comment.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries))
                    {
                        list.Add($"{space}///     {line}");
                    }
                    
                    list.Add($"{space}/// </summary>");
                }
            }
        }

        var sb = new StringBuilder();

        foreach (var item in list)
        {
            sb.AppendLine(item);
        }

        File.WriteAllText(@"C:\github\ReactWithDotNet\ReactWithDotNet\CommonHtmlElements.cs", sb.ToString());

        static string CamelCase(string str)
        {
            if (str.IndexOf('-') > 0)
            {
                var names = str.Split("-");

                return names[0] + Join(Empty, names.Skip(1).Select(name => char.ToUpper(name[0], new CultureInfo("en-US")) + name.Substring(1)));
            }

            return str;
        }

        static string UpperCaseFirstChar(string str)
        {
            return char.ToUpper(str[0], new CultureInfo("en-US")) + str.Substring(1);
        }
    }

    sealed class AttributeInfo
    {
        public string Comment { get; init; }
        public string DefaultValue { get; init; }
        public string Name { get; init; }
        public string Type { get; init; } = "string";
        public string GrabEventArgumentsByUsingFunction { get; init; }
        public bool IsIsVoidTaskDelegate { get; init; }
        public bool IsBindingExpression { get; init; }
        public string TransformValueInClient { get; init; }
        public ReactBindAttribute Bind{ get; init; }
        public bool JsonIgnore { get; init; }
    }

    sealed class TagInfo
    {
        public IReadOnlyList<AttributeInfo> Attributes { get; init; }
        public string Comment { get; init; }
        public bool CreateClassAsPartial { get; init; }
        public bool EnableCastFromString { get; init; } = true;
        public string Tag { get; init; }
    }
}

// List attribute names of html 'ellipse' tag in this format
// new ()
// {
//     Name    = "#name",
//     Comment = "#description",
//     Type = "#valueType"
// },



/*
  #region string href
   PropertyValueNode<string> __href;
   
   static readonly PropertyValueDefinition _href = new() { name = nameof(href) };
   
   /// <summary>
   ///     The URL of the linked resource.
   /// </summary>
   public string href
   {
       get => __href?.value;
       set => SetValue(ref __href, _href, value);
   }
   #endregion
 */