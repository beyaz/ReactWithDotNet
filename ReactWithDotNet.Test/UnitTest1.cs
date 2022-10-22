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
        [TestMethod]
        public void ExportCommonHtmlElements()
        {
            var map = new[]
            {
                new { Tag = "div", Comment = Empty },

                new { Tag = "p", Comment = "Defines a paragraph" },

                new { Tag = "h1", Comment = Empty },
                new { Tag = "h2", Comment = Empty },
                new { Tag = "h3", Comment = Empty },
                new { Tag = "h4", Comment = Empty },
                new { Tag = "h5", Comment = Empty },
                new { Tag = "h6", Comment = Empty },

                new { Tag = "header", Comment = Empty },
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
                list.Add($"    public {item.Tag}(string innerText) : base(innerText) {{  }}");

                list.Add(Empty);
                addComment();
                list.Add($"    public {item.Tag}(params IModifier[] modifiers) : base(modifiers) {{ }}");

                list.Add(Empty);
                addComment();
                list.Add($"    public static implicit operator {item.Tag}(string text) => new() {{ text = text }};");

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
