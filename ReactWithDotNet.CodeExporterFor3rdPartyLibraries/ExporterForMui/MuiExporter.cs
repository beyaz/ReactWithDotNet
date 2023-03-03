using System.Net.Http;
using System.Text;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.Mixin;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

public class MuiExportInput
{
    public string ClassName { get; set; }
    public string DefinitionTsCode { get; set; }
    public IReadOnlyList<string> SkipMembers { get; set; }
    public string StartFrom { get; set; }
}

public class MuiExporter
{
    public static void ExportToCSharpFile(MuiExportInput input)
    {
        var lines = CalculateCSharpFileContentLines(input);

        var sb = new StringBuilder();

        lines.WriteLines(x => sb.AppendLine(x));

        WriteAllText($@"D:\work\git\ReactDotNet\ReactWithDotNet.Libraries\mui\material\{input.ClassName}.cs", sb.ToString());
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
                                  .RemoveFromStart("* ")
                                  .Trim();
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

            lines.Add("///    " + line);
        }

        lines.Add("/// </summary>");

        return lines;
    }

    static IReadOnlyList<string> AsCSharpMember(TsMemberInfo memberInfo)
    {
        var lines = new List<string>();

        if (memberInfo.Comment is not null)
        {
            lines.AddRange(AsCSharpComment(memberInfo.Comment));
        }

        if (memberInfo.Name =="sx")
        {
            lines.Add("[React]");
            lines.Add("[ReactTransformValueInClient(\"ReactWithDotNet::Core::ReplaceNullWhenEmpty\")]");
            lines.Add("public dynamic sx { get; } = new ExpandoObject();");

            return lines;
        }

        lines.Add("[React]");

        lines.Add("public " + AsCSharpType(memberInfo.PropertyType) + " " + memberInfo.Name + " {get; set; }");

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

    static IReadOnlyList<string> CalculateCSharpFileContentLines(MuiExportInput input)
    {
        

        var (exception, hasRead, endIndex, tokens) = TsLexer.ParseTokens(input.DefinitionTsCode, 0);
        if (hasRead)
        {
            var (isFound, indexOfLastMatchedToken) = TsParser.FindMatch(tokens, 0, TsLexer.ParseTokens(input.StartFrom, 0).tokens);
            if (isFound)
            {
                (hasRead, var members, var newIndex) = TsParser.TryReadMembers(tokens, indexOfLastMatchedToken);
                if (hasRead)
                {
                    var lines = new List<string>();

                    lines.Add("namespace ReactWithDotNet.Libraries.mui.material;");
                    lines.Add(string.Empty);

                    lines.Add($"partial class {input.ClassName}");
                    lines.Add("{");

                    var isFirstMember = true;

                    foreach (var tsMemberInfo in members)
                    {
                        if (input.SkipMembers.Contains(tsMemberInfo.Name))
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
}