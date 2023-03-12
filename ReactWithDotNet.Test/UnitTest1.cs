using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.String;

namespace ReactWithDotNet.Test;

[TestClass]
[Ignore]
public class UnitTest1
{
    class TagInfo
    {
        public string Tag { get; set; }
        public string Comment { get; set; }
        public bool EnableStringIntegration { get; set; } = true;
        public bool CreateClassAsPartial { get; set; }
    }
    [TestMethod]
    public void ExportCommonHtmlElements()
    {
        TagInfo[] map = 
        {
            new() { Tag = "article", Comment = "Specifies independent, self-contained content." },

            new() { Tag = "div", Comment = Empty },

            new() { Tag = "p", Comment = "Defines a paragraph" },

            new() { Tag = "pre", Comment = "Preformatted text" },
            new() { Tag = "code", Comment = "Define some text as computer code in a document" },


            new() { Tag = "ol", Comment = "Ordered list" },

            new() { Tag = "ul", Comment = "Unordered (bulleted) list" },

            new() { Tag = "li", Comment = "List item" },

            new() { Tag = "label", CreateClassAsPartial = true},

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
                
            new() { Tag = "strong", Comment = "Important text" },

            new() { Tag = "section", Comment = "Section in a document", EnableStringIntegration = false },

            new() {Tag = "aside", EnableStringIntegration = false},

            new() {Tag = "fieldset", EnableStringIntegration = false},

            new() {Tag = "legend", EnableStringIntegration = false},

            new() {Tag = "nav", EnableStringIntegration = false},

            new() {Tag = "main", EnableStringIntegration = false},

            new() {Tag = "footer", EnableStringIntegration = false},

            new() {Tag = "figure", EnableStringIntegration = false},
                
            new() {Tag = "figcaption"}
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

            addComment();
            list.Add($"    public {item.Tag}() {{ }}");

            list.Add(Empty);
            addComment();
            list.Add($"    public {item.Tag}(params IModifier[] modifiers) : base(modifiers) {{ }}");

            if (item.EnableStringIntegration)
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
            
        File.WriteAllText(@"D:\work\git\ReactDotNet\ReactWithDotNet\CommonHtmlElements.cs", sb.ToString());
    }
}