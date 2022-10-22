using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.String;

namespace ReactWithDotNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        class TagInfo
        {
            public string Tag { get; set; }
            public string Comment { get; set; }
            public bool EnableStringIntegration { get; set; } = true;
        }
        [TestMethod]
        public void ExportCommonHtmlElements()
        {
            TagInfo[] map = 
            {
                new TagInfo  { Tag = "article", Comment = "Specifies independent, self-contained content." },

                new TagInfo { Tag = "div", Comment = Empty },

                new TagInfo { Tag = "p", Comment = "Defines a paragraph" },

                new TagInfo { Tag = "pre", Comment = "Preformatted text" },


                new TagInfo { Tag = "ol", Comment = "Ordered list" },

                new TagInfo { Tag = "ul", Comment = "Unordered (bulleted) list" },

                new TagInfo { Tag = "li", Comment = "List item" },


                new TagInfo { Tag = "h1", Comment = Empty },
                new TagInfo { Tag = "h2", Comment = Empty },
                new TagInfo { Tag = "h3", Comment = Empty },
                new TagInfo { Tag = "h4", Comment = Empty },
                new TagInfo { Tag = "h5", Comment = Empty },
                new TagInfo { Tag = "h6", Comment = Empty },

                new TagInfo { Tag = "header", Comment = Empty },

                new TagInfo { Tag = "span", Comment = "Inline container used to mark up a part of a text, or a part of a document." },

                new TagInfo { Tag = "sup", Comment = "Superscript text" },
                
                new TagInfo { Tag = "sub", Comment = "Subscript text" },
                
                new TagInfo { Tag = "ins", Comment = "Inserted text" },
                
                new TagInfo { Tag = "del", Comment = "Deleted text" },
                
                new TagInfo { Tag = "small", Comment = "Smaller text" },
                
                new TagInfo { Tag = "mark", Comment = "Marked text" },
                
                new TagInfo { Tag = "em", Comment = "Emphasized text" },
                
                new TagInfo { Tag = "b", Comment = "Bold text" },
                
                new TagInfo { Tag = "i", Comment = "Italic text" },
                
                new TagInfo { Tag = "strong", Comment = "Important text" },

                new TagInfo { Tag = "section", Comment = "Section in a document", EnableStringIntegration = false },

                new TagInfo{Tag = "aside", EnableStringIntegration = false},

                new TagInfo{Tag = "fieldset", EnableStringIntegration = false},

                new TagInfo{Tag = "legend", EnableStringIntegration = false},

                new TagInfo{Tag = "nav", EnableStringIntegration = false},

                new TagInfo{Tag = "main", EnableStringIntegration = false},

                new TagInfo{Tag = "footer", EnableStringIntegration = false}
            };

            var list = new List<string>
            {
                "namespace ReactWithDotNet;"
            };

            foreach (var item in map)
            {
                addComment();
                list.Add($"public sealed class {item.Tag} : HtmlElement");
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

                list.Add("}");
                
                list.Add(Empty);

                void addComment()
                {
                    if (IsNullOrWhiteSpace(item.Comment) == false)
                    {
                        list.Add($"    /// <summary>");
                        list.Add($"    ///     {item.Comment}");
                        list.Add($"    /// </summary>");
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
}
