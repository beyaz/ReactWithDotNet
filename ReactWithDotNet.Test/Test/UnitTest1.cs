using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.String;

namespace ReactWithDotNet.Test;

[TestClass]
//[Ignore]
public class UnitTest1
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
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name = "type",
                        Type = "string"
                    },
                    new AttributeInfo
                    {
                        Name = "disabled",
                        Type = "string"
                    }
                },
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
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name = "cellSpacing",
                        Type = "double?"
                    },
                    new AttributeInfo
                    {
                        Name = "cellPadding",
                        Type = "double?"
                    }
                },
                EnableCastFromString = false
            },

            new() { Tag = "thead", EnableCastFromString = false },
            new() { Tag = "tbody", EnableCastFromString = false },
            new() { Tag = "tfoot", EnableCastFromString = false },

            new()
            {
                Tag = "th",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name = "colSpan",
                        Type = "int?"
                    },
                    new AttributeInfo
                    {
                        Name = "rowSpan",
                        Type = "int?"
                    }
                },
                EnableCastFromString = false
            },
            new()
            {
                Tag = "td",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name = "colSpan",
                        Type = "int?"
                    },
                    new AttributeInfo
                    {
                        Name = "rowSpan",
                        Type = "int?"
                    }
                },
                EnableCastFromString = true
            },
            new()
            {
                Tag = "tr",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name = "colSpan",
                        Type = "int?"
                    },
                    new AttributeInfo
                    {
                        Name = "rowSpan",
                        Type = "int?"
                    }
                },
                EnableCastFromString = false
            },

            new()
            {
                Tag = "option",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name = "selected",
                        Type = "bool?"
                    },
                    new AttributeInfo
                    {
                        Name = "disabled",
                        Type = "string"
                    },
                    new AttributeInfo
                    {
                        Name = "value",
                        Type = "string"
                    }
                },
                EnableCastFromString = false
            },

            new()
            {
                Tag = "ellipse",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "cx",
                        Comment = "The x-coordinate of the center of the ellipse."
                    },
                    new AttributeInfo
                    {
                        Name    = "cy",
                        Comment = "The y-coordinate of the center of the ellipse."
                    },
                    new AttributeInfo
                    {
                        Name    = "rx",
                        Comment = "The radius of the ellipse along the x-axis."
                    },
                    new AttributeInfo
                    {
                        Name    = "ry",
                        Comment = "The radius of the ellipse along the y-axis."
                    },
                    new AttributeInfo
                    {
                        Name    = "fill",
                        Comment = "The fill color of the ellipse."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "The stroke color of the ellipse."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the stroke."
                    }
                },
                EnableCastFromString = false
            },

            new()
            {
                Tag = "line",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "x1",
                        Comment = "The x-coordinate of the start point of the line."
                    },
                    new AttributeInfo
                    {
                        Name    = "y1",
                        Comment = "The y-coordinate of the start point of the line."
                    },
                    new AttributeInfo
                    {
                        Name    = "x2",
                        Comment = "The x-coordinate of the end point of the line."
                    },
                    new AttributeInfo
                    {
                        Name    = "y2",
                        Comment = "The y-coordinate of the end point of the line."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "The stroke (outline) color of the line."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the line's outline."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-dasharray",
                        Comment = "Pattern of dashes and gaps used in the line's stroke."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-linecap",
                        Comment = "The style of the line's endpoints."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-linejoin",
                        Comment = "The style of the line's corners."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-opacity",
                        Comment = "The opacity (transparency) of the line's stroke."
                    }
                },
                EnableCastFromString = false
            },

            new()
            {
                Tag = "polyline",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "points",
                        Comment = "A list of points defining the vertices of the polyline."
                    },
                    new AttributeInfo
                    {
                        Name    = "fill",
                        Comment = "The fill color of the polyline."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "The stroke (outline) color of the polyline."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the polyline's outline."
                    }
                },
                EnableCastFromString = false
            },

            new()
            {
                Tag = "circle",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "cx",
                        Comment = "The x-coordinate of the center of the circle."
                    },
                    new AttributeInfo
                    {
                        Name    = "cy",
                        Comment = "The y-coordinate of the center of the circle."
                    },
                    new AttributeInfo
                    {
                        Name    = "r",
                        Comment = "The radius of the circle."
                    },
                    new AttributeInfo
                    {
                        Name    = "fill",
                        Comment = "The fill color of the circle."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "The stroke color of the circle."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the stroke of the circle."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "polygon",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "points",
                        Comment = "Specifies the coordinates of the polygon's vertices, in (x, y) pairs, separated by commas.",
                    },

                    new AttributeInfo
                    {
                        Name    = "fill",
                        Comment = "Specifies the fill color of the polygon.",
                    },

                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "Specifies the stroke color of the polygon.",
                    },

                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "Specifies the width of the polygon's stroke, in pixels.",
                    },

                    new AttributeInfo
                    {
                        Name    = "stroke-linecap",
                        Comment = "Specifies the type of line cap used for the polygon's stroke.",
                    },

                    new AttributeInfo
                    {
                        Name    = "stroke-linejoin",
                        Comment = "Specifies the type of line join used for the polygon's stroke.",
                    },

                    new AttributeInfo
                    {
                        Name    = "fill-rule",
                        Comment = "Specifies how the polygon is filled.",
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "rect",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "x",
                        Comment = "The x-coordinate of the top-left corner of the rectangle."
                    },
                    new AttributeInfo
                    {
                        Name    = "y",
                        Comment = "The y-coordinate of the top-left corner of the rectangle."
                    },
                    new AttributeInfo
                    {
                        Name    = "width",
                        Comment = "The width of the rectangle."
                    },
                    new AttributeInfo
                    {
                        Name    = "height",
                        Comment = "The height of the rectangle."
                    },
                    new AttributeInfo
                    {
                        Name    = "rx",
                        Comment = "The border radius of the rectangle on the horizontal axis."
                    },
                    new AttributeInfo
                    {
                        Name    = "ry",
                        Comment = "The border radius of the rectangle on the vertical axis."
                    },
                    new AttributeInfo
                    {
                        Name    = "fill",
                        Comment = "The fill color of the rectangle."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "The stroke color of the rectangle."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "The width of the rectangle's stroke."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-linecap",
                        Comment = "The linecap style of the rectangle's stroke."
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-linejoin",
                        Comment = "The linejoin style of the rectangle's stroke."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "radialGradient",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "cx",
                        Comment = "The x-coordinate of the center of the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "cy",
                        Comment = "The y-coordinate of the center of the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "fx",
                        Comment = "The x-coordinate of the focal point of the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "fy",
                        Comment = "The y-coordinate of the focal point of the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "r",
                        Comment = "The radius of the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "spreadMethod",
                        Comment = "The method used to spread the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "gradientUnits",
                        Comment = "The units used to specify the gradient."
                    },
                    new AttributeInfo
                    {
                        Name    = "gradientTransform",
                        Comment = "A transform to apply to the gradient."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "clipPath",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "clip-rule",
                        Comment = "Specifies the fill rule for the clipping path.",
                    },
                    new AttributeInfo
                    {
                        Name    = "clip-box",
                        Comment = "Specifies the reference box for the clipping path.",
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "path",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "d",
                        Comment = "Path data"
                    },
                    new AttributeInfo
                    {
                        Name    = "fill",
                        Comment = "Fill color"
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke",
                        Comment = "Stroke color"
                    },
                    new AttributeInfo
                    {
                        Name    = "stroke-width",
                        Comment = "Stroke width"
                    },
                    
                    new AttributeInfo
                    {
                        Name    = "fillRule"
                    },
                    new AttributeInfo
                    {
                        Name = "clipRule"
                    },
                    new AttributeInfo
                    {
                        Name = "strokeLinecap"
                    },
                    new AttributeInfo
                    {
                        Name = "strokeLinejoin"
                    },
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "g",
                Attributes = new[]
                {
                    
                    new AttributeInfo
                    {
                        Name    = "opacity"
                    },
                    
                    new AttributeInfo
                    {
                        Name = "clipPath"
                    },
                    new AttributeInfo
                    {
                        Name = "transform"
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "meta",
                Attributes = new[]
                {
                    
                    new AttributeInfo
                    {
                    Name  = "charset",
                    Comment = "Specifies the character encoding of the document."
                    },
                    new AttributeInfo
                    {
                        Name    = "http-equiv",
                        Comment = "Specifies the name of the HTTP header that the meta tag should be equivalent to."
                    },
                    new AttributeInfo
                    {
                        Name    = "name",
                        Comment = "Specifies the name of the metadata property."
                    },
                    new AttributeInfo
                    {
                        Name    = "content",
                        Comment = "Specifies the value of the metadata property."
                    },
                    new AttributeInfo
                    {
                        Name    = "scheme",
                        Comment = "Specifies the URL scheme for the content attribute of the meta tag."
                    },
                    new AttributeInfo
                    {
                        Name    = "itemprop",
                        Comment = "Specifies the Microdata item property that the meta tag represents."
                    },
                    new AttributeInfo
                    {
                        Name    = "property",
                        Comment = "Specifies the schema.org property that the meta tag represents."
                    },
                    new AttributeInfo
                    {
                        Name    = "src",
                        Comment = "Specifies the URL for a resource associated with the meta tag."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "body",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "background",
                        Comment = "Specifies the URL of a background image to be displayed behind the document's content."
                    },
                    new AttributeInfo
                    {
                        Name    = "bgcolor",
                        Comment = "Specifies the background color of the document's body."
                    },
                    new AttributeInfo
                    {
                        Name    = "link",
                        Comment = "Specifies the color of unvisited links in the document's body."
                    }
                },
                EnableCastFromString = false
            },
            new()
            {
                Tag = "script",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "async",
                        Comment = "Specifies that the script should be executed asynchronously. This means that the browser will not wait for the script to finish executing before continuing to parse the rest of the HTML."
                    },
                    new AttributeInfo
                    {
                        Name    = "defer",
                        Comment = "Specifies that the script should be executed after the browser has finished parsing the rest of the HTML. This is similar to async, but it ensures that scripts are executed in the order they are specified in the HTML."
                    },
                    new AttributeInfo
                    {
                        Name    = "integrity",
                        Comment = "Specifies a subresource integrity (SRI) hash for the script. This helps to protect against man-in-the-middle attacks."
                    },
                    new AttributeInfo
                    {
                        Name    = "language",
                        Comment = "Specifies the scripting language of the script. This is deprecated, but is still supported by most browsers."
                    },
                    new AttributeInfo
                    {
                        Name    = "nomodule",
                        Comment = "Specifies that the script should be ignored if the browser does not support modules."
                    },
                    new AttributeInfo
                    {
                        Name    = "src",
                        Comment = "Specifies the URL of an external script file."
                    },
                    new AttributeInfo
                    {
                        Name    = "type",
                        Comment = "Specifies the type of the script. The most common value is application/javascript."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "title",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "language",
                        Comment = "Specifies the language of the title."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag = "head",
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "profile",
                        Comment = "Provides a URL to a profile document for the current document.",
                    },
                   
                    new AttributeInfo
                    {
                        Name    = "link",
                        Comment = "Provides a link to an external resource, such as a stylesheet or script file.",
                    },
                    new AttributeInfo
                    {
                        Name    = "meta",
                        Comment = "Provides metadata about the document, such as the character encoding, author, and keywords.",
                    },
                   
                    new AttributeInfo
                    {
                        Name    = "script",
                        Comment = "Provides JavaScript code to be executed in the browser.",
                    },
                    new AttributeInfo
                    {
                        Name    = "noscript",
                        Comment = "Provides content to be displayed if the browser does not support JavaScript.",
                    },
                    //new AttributeInfo
                    //{
                    //    Name    = "base",
                    //    Comment = "Specifies the base URL for all relative URLs in the document.",
                    //}
                },
                EnableCastFromString = false
            },
            
            
            new()
            {
                Tag = "html",
                Attributes = new[]
                {
                   
                    new AttributeInfo
                    {
                        Name    = "hidden",
                        Comment = "Hides the element from display."
                    },
                    
                    new AttributeInfo
                    {
                        Name    = "manifest",
                        Comment = "Specifies the URL of a manifest file, which provides information about the web app."
                    },
                    new AttributeInfo
                    {
                        Name    = "xmlns",
                        Comment = "Specifies the namespace of the element.",
                        DefaultValue = "http://www.w3.org/1999/xhtml"
                    },
                    new AttributeInfo
                    {
                        Name    = "prefix",
                        Comment = "Specifies the prefix of the element."
                    },
                    new AttributeInfo
                    {
                        Name    = "version",
                        Comment = "Specifies the version of the HTML specification to which the element conforms."
                    }
                },
                EnableCastFromString = false
            },
            
            new()
            {
                Tag                  = "label",
                EnableCastFromString = true,
                Attributes = new[]
                {
                    new AttributeInfo
                    {
                        Name    = "htmlFor",
                        Comment = "Specifies which form element a label is bound to."
                    },
                    new AttributeInfo
                    {
                        Name    = "accesskey",
                        Comment = "Defines a keyboard shortcut to activate the element."
                    },
                   
                    new AttributeInfo
                    {
                        Name    = "contenteditable",
                        Comment = "Specifies whether the content of the element is editable."
                    },
                    
                    new AttributeInfo
                    {
                        Name    = "draggable",
                        Comment = "Specifies whether the element is draggable."
                    },
                    new AttributeInfo
                    {
                        Name    = "dropzone",
                        Comment = "Specifies whether the element is a drop target."
                    },
                    new AttributeInfo
                    {
                        Name    = "hidden",
                        Comment = "Hides the element from view."
                    },
                   
                    new AttributeInfo
                    {
                        Name    = "spellcheck",
                        Comment = "Specifies whether the element's content should be spellchecked."
                    },
                    
                    new AttributeInfo
                    {
                        Name    = "tabindex",
                        Comment = "Specifies the element's position in the tab order."
                    },
                    new AttributeInfo
                    {
                        Name    = "title",
                        Comment = "Adds a tooltip to the element."
                    },
                    new AttributeInfo
                    {
                        Name    = "translate",
                        Comment = "Specifies whether the element's content should be translated."
                    }
                },
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

            var partialModifier = "";
            if (item.CreateClassAsPartial)
            {
                partialModifier = " partial";
            }

            list.Add($"public sealed{partialModifier} class {item.Tag} : HtmlElement");
            list.Add("{");

            if (item.Attributes is not null)
            {
                foreach (var attribute in item.Attributes)
                {
                    if (IsNullOrWhiteSpace(attribute.Comment) == false)
                    {
                        list.Add($"{padding}/// <summary>");
                        list.Add($"{padding}///     {attribute.Comment}");
                        list.Add($"{padding}/// </summary>");
                    }

                    var partDefaultValueAssignment = "";
                    if (attribute.DefaultValue is not null)
                    {
                        partDefaultValueAssignment = $" = \"{attribute.DefaultValue}\";";
                    }
                    list.Add("    [ReactProp]");
                    list.Add($"    public {attribute.Type} {CamelCase(attribute.Name)} {{ get; set; }}{partDefaultValueAssignment}");
                    list.Add(Empty);
                }
            }

            addComment();
            list.Add($"    public {item.Tag}() {{ }}");

            list.Add(Empty);
            addComment();
            list.Add($"    public {item.Tag}(params IModifier[] modifiers) : base(modifiers) {{ }}");

            if (item.EnableCastFromString)
            {
                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(string innerText) : base(innerText) {{  }}");

                list.Add(Empty);
                addComment();
                list.Add($"    public static implicit operator {item.Tag}(string text) => new() {{ text = text }};");
            }

            list.Add(Empty);
            addComment();
            list.Add($"    public {item.Tag}(Style style) : base(style) {{ }}");

            list.Add("}");

            list.Add(Empty);

            void addComment(string space = "    ")
            {
                if (IsNullOrWhiteSpace(item.Comment) == false)
                {
                    list.Add($"{space}/// <summary>");
                    list.Add($"{space}///     {item.Comment}");
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
    }

    class AttributeInfo
    {
        public string Comment { get; init; }
        public string Name { get; init; }
        public string Type { get; init; } = "string";
        public string DefaultValue { get; set; }
    }

    class TagInfo
    {
        public IReadOnlyList<AttributeInfo> Attributes { get; init; }
        public string Comment { get; init; }
        public bool CreateClassAsPartial { get; init; }
        public bool EnableCastFromString { get; init; } = true;
        public string Tag { get; init; }
    }
}

// List attribute names of html 'ellipse' tag in this format
// new AttributeInfo
// {
//     Name    = "#name",
//     Comment = "#description"
// },