using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.Mixin;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

[TestClass]
public class MuiExporter
{
    [TestMethod]
    public void PaperClasses()
    {
        var lines = CalculatePaperClasses();

        var sb = new StringBuilder();

        lines.WriteLines(x => sb.AppendLine(x));

        WriteAllText(@"D:\work\git\ReactDotNet\ReactWithDotNet.Libraries\mui\material\Paper\PaperClasses.cs", sb.ToString());
    }
    [TestMethod]
    public void Paper()
    {
        var lines = CalculatePaper();

        var sb = new StringBuilder();

        lines.WriteLines(x => sb.AppendLine(x));

        WriteAllText(@"D:\work\git\ReactDotNet\ReactWithDotNet.Libraries\mui\material\Paper\Paper.cs", sb.ToString());
    }
    static List<string> CalculatePaper()
    {

        var content = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/mui/material-ui/master/packages/mui-material/src/Paper/Paper.d.ts").GetAwaiter().GetResult();

        var (exception, hasRead, endIndex, tokens) = TsLexer.ParseTokens(content, 0);
        if (hasRead)
        {
            var (isFound, indexOfLastMatchedToken) = TsParser.FindMatch(tokens, 0, TsLexer.ParseTokens("props: P & {", 0).tokens);
            if (isFound)
            {
                (hasRead, var members, var newIndex) = TsParser.TryReadMembers(tokens, indexOfLastMatchedToken);
                if (hasRead)
                {
                    var lines = new List<string>();

                    lines.Add("namespace ReactWithDotNet.Libraries.mui.material;");
                    lines.Add(string.Empty);

                    lines.Add("partial class Paper");
                    lines.Add("{");

                    var isFirstMember = true;

                    foreach (var tsMemberInfo in members)
                    {
                        if (tsMemberInfo.Name == "children")
                        {
                            continue;
                        }
                        
                        if (!isFirstMember)
                        {
                            lines.Add(string.Empty);
                        }

                        isFirstMember = false;

                        lines.AddRange(AsCSharpMember(tsMemberInfo));
                    }

                    lines.Add("}");

                    return lines;
                }

            }
        }

        return null;
    }
    
    static List<string> CalculatePaperClasses()
    {

        var content = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/mui/material-ui/master/packages/mui-material/src/Paper/paperClasses.ts").GetAwaiter().GetResult();

        var (exception, hasRead, endIndex, tokens) = TsLexer.ParseTokens(content, 0);
        if (hasRead)
        {
            var (isFound, indexOfLastMatchedToken) = TsParser.FindMatch(tokens, 0, TsLexer.ParseTokens("export interface PaperClasses {", 0).tokens);
            if (isFound)
            {
                (hasRead, var members, var newIndex) = TsParser.TryReadMembers(tokens, indexOfLastMatchedToken);
                if (hasRead)
                {
                    var lines = new List<string>();
                    
                    lines.Add("namespace ReactWithDotNet.Libraries.mui.material;");
                    lines.Add(string.Empty);
                    
                    lines.Add("public sealed class PaperClasses");
                    lines.Add("{");

                    var isFirstMember = true;
                    
                    foreach (var tsMemberInfo in members)
                    {
                        if (!isFirstMember)
                        {
                            lines.Add(string.Empty);
                        }

                        isFirstMember = false;
                        
                        lines.AddRange(AsCSharpMember(tsMemberInfo));
                    }

                    lines.Add("}");

                    return lines;
                }

            }
        }

        return null;
    }

    static IReadOnlyList<string> AsCSharpMember( TsMemberInfo memberInfo)
    {
        var lines = new List<string>();
        
        if (memberInfo.Comment is not null)
        {
            lines.AddRange(AsCSharpComment(memberInfo.Comment));
        }

        lines.Add("[React]");

        lines.Add("public "+ AsCSharpType(memberInfo.PropertyType) +" " + memberInfo.Name + " {get; set; }");
        

        return lines;
    }

    static string AsCSharpType(TsTypeReference tsTypeReference)
    {
        if (tsTypeReference.Name.Equals("string", StringComparison.OrdinalIgnoreCase))
        {
            return "string";
        }
        if (tsTypeReference.Name.Equals("OverridableStringUnion", StringComparison.OrdinalIgnoreCase))
        {
            return "string";
        }

        if (tsTypeReference.Name.Equals("Partial", StringComparison.OrdinalIgnoreCase))
        {
            return "string";
        }

        if (tsTypeReference.Name.Equals("number", StringComparison.OrdinalIgnoreCase))
        {
            return "double";
        }

        if (tsTypeReference.Name.Equals("boolean", StringComparison.OrdinalIgnoreCase))
        {
            return "bool";
        }

        if (tsTypeReference.Name.Equals("SxProps", StringComparison.OrdinalIgnoreCase))
        {
            return "dynamic";
        }


        if (tsTypeReference.Name.Equals("React.ReactNode", StringComparison.OrdinalIgnoreCase))
        {
            return "Element";
        }


        return tsTypeReference.Name;
    }

    static IEnumerable<string> AsCSharpComment(string tsComment)
    {
        if (tsComment is null)
        {
            return Enumerable.Empty<string>();
        }

        var lines = new List<string>();

        var commentLines = tsComment.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
        lines.Add("/// <summary>");

        var isFirst = true;
        
        foreach (var commentLine in commentLines)
        {
            var line = commentLine.Trim()
                                  .Trim(Environment.NewLine.ToCharArray())
                                  .RemoveFromStart("/**")
                                  .RemoveFromStart("/*")
                                  .RemoveFromEnd("*/")
                                  .Trim()
                                  .Trim(Environment.NewLine.ToCharArray());
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            if (isFirst)
            {
                isFirst = false;
            }
            else
            {
                lines.Add("/// <br/>");
            }
            lines.Add("///    "+ line);
        }
        lines.Add("/// </summary>");


        return lines;

    }
}