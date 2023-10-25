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

            new()
            {
                Tag                  = "label",
                CreateClassAsPartial = true,
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
                    
                    list.Add("    [ReactProp]");
                    list.Add($"    public {attribute.Type} {CamelCase(attribute.Name)} {{ get; set; }}");
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

            void addComment(string padding = "    ")
            {
                if (IsNullOrWhiteSpace(item.Comment) == false)
                {
                    list.Add($"{padding}/// <summary>");
                    list.Add($"{padding}///     {item.Comment}");
                    list.Add($"{padding}/// </summary>");
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
                    
                return names[0] + string.Join(string.Empty, names.Skip(1).Select(name=>char.ToUpper(name[0], new CultureInfo("en-US")) + name.Substring(1)));
            }

            return str;
        }
    }

    class AttributeInfo
    {
        public string Name { get; init; }
        public string Comment { get; init; }
        public string Type { get; init; } = "string";
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
// }


