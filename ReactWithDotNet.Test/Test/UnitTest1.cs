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
            new() { Tag = "article", Comment = "Specifies independent, self-contained content." },
            
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
                }
            },
            
            new() { Tag = "div", Comment = Empty },

            new() { Tag = "p", Comment = "Defines a paragraph" },

            new() { Tag = "pre", Comment  = "Preformatted text" },
            new() { Tag = "code", Comment = "Define some text as computer code in a document" },

            new() { Tag = "ol", Comment = "Ordered list" },

            new() { Tag = "ul", Comment = "Unordered (bulleted) list" },

            new() { Tag = "li", Comment = "List item" },

            new() { Tag = "label", CreateClassAsPartial = true },

            new() { Tag = "h1", Comment = Empty },

            new() { Tag = "h2", Comment = Empty },
            new() { Tag = "h3", Comment = Empty },
            new() { Tag = "h4", Comment = Empty },
            new() { Tag = "h5", Comment = Empty },
            new() { Tag = "h6", Comment = Empty },

            new() { Tag = "header", Comment = Empty },

            new() { Tag = "span", Comment = "Inline container used to mark up a part of a text, or a part of a document." },

            new() { Tag = "sup", Comment = "Superscript text" },

            new() { Tag = "sub", Comment = "Subscript text" },

            new() { Tag = "ins", Comment = "Inserted text" },

            new() { Tag = "del", Comment = "Deleted text" },

            new() { Tag = "small", Comment = "Smaller text" },

            new() { Tag = "mark", Comment = "Marked text" },

            new() { Tag = "em", Comment = "Emphasized text" },

            new() { Tag = "b", Comment = "Bold text" },

            new() { Tag = "i", Comment = "Italic text" },
            
            new() { Tag = "u", Comment = "Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline." },

            new() { Tag = "strong", Comment = "Important text" },

            new() { Tag = "section", Comment = "Section in a document", EnableCastFromString = false },

            new() { Tag = "aside", EnableCastFromString = false },

            new() { Tag = "fieldset", EnableCastFromString = false },

            new() { Tag = "legend", EnableCastFromString = false },

            new() { Tag = "nav", EnableCastFromString = false },

            new() { Tag = "main", EnableCastFromString = false },

            new() { Tag = "footer", EnableCastFromString = false },

            new() { Tag = "figure", EnableCastFromString = false },

            new() { Tag = "hr", EnableCastFromString = false },

            new() { Tag = "figcaption" },
            
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
                }
            },
            
            new() { Tag = "thead" },
            new() { Tag = "tbody" },
            new() { Tag = "tfoot" },
            
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
                }
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
                }
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
                }
            },
        };

        var list = new List<string>
        {
            "namespace ReactWithDotNet;"
        };

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
                    list.Add( "    [ReactProp]");
                    list.Add($"    public {attribute.Type} {attribute.Name} {{ get; set; }}");
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
    }

    class TagInfo
    {
        public string Comment { get; init; }
        public bool CreateClassAsPartial { get; init; }
        public bool EnableCastFromString { get; init; } = true;
        public string Tag { get; init; }
        
        public IReadOnlyList<AttributeInfo> Attributes { get; init; }
    }

    class AttributeInfo
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}