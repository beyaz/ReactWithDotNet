using ReactWithDotNet.TypeScriptCodeAnalyzer;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.Mixin;

namespace ReactWithDotNet.Exporting;

static class Exporter
{
    public static void ExportToCSharpFile(ExportInput input)
    {
        var code = CalculateCSharpFileContentLines(input).GetValue().ToCSharpCode();

        const string projectFolder = @"C:\github\ReactWithDotNet\ReactWithDotNet\ThirdPartyLibraries";

        WriteAllText($@"{projectFolder}{input.OutputFileLocation}{input.ClassName}.cs", code);
    }

    static (bool hasMatch, string dotNetType) TryMatchDotNetType(TsMemberInfo memberInfo)
    {
        var tokens = memberInfo.RemainingPart?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count <= 0)
        {
            return default;
        }

        if (tokens.Count == 1)
        {
            var name = tokens[0].value;

            if (name.Equals("string", StringComparison.OrdinalIgnoreCase))
            {
                return (true, "string");
            }

            if (name.Equals("number", StringComparison.OrdinalIgnoreCase))
            {
                return (true, "double?");
            }

            if (name.Equals("boolean", StringComparison.OrdinalIgnoreCase))
            {
                return (true, "bool?");
            }

            if (name.Equals("dynamic", StringComparison.OrdinalIgnoreCase))
            {
                return (true, "dynamic");
            }
        }

        // is object
        if (tokens[0].tokenType == TokenType.LeftBrace && tokens[^1].tokenType == TokenType.RightBrace)
        {
            return (true, "dynamic");
        }

        if (tokens.StartsWith("Partial<"))
        {
            return (true, "dynamic");
        }

        if (tokens.StartsWith("React.ReactNode"))
        {
            return (true, "Element");
        }

        if (tokens.StartsWith("OverridableStringUnion"))
        {
            return (true, "string");
        }

        if (tokens.FullMatch("string | number"))
        {
            return (true, "int?");
        }

        var (hasRead, tsTypeReference, _) = TsParser.TryReadUnionTypeReference(tokens, 0);
        if (hasRead)
        {
            if (tsTypeReference.UnionTypes?.All(t => t.IsStringValue || t.Name == "undefined") == true)
            {
                return (true, "string");
            }
            
            return (true, "object");
        }

        return default;
    }

    static IReadOnlyList<string> AsCSharpMember(ExportInput input, TsMemberInfo memberInfo)
    {
        var lines = new List<string>();

        if (memberInfo.Comment is not null)
        {
            lines.AddRange(AsCSharpComment(memberInfo.Comment));
        }

        if (!input.PropToDotNetTypeMap.TryGetValue($"{input.NamespaceName} > {input.ClassName} > {memberInfo.Name}", out var dotNetType))
        {
            if (!input.PropToDotNetTypeMap.TryGetValue($"{input.NamespaceName} > * > {memberInfo.Name}", out dotNetType))
            {
                var matchResponse = TryMatchDotNetType(memberInfo);
                if (matchResponse.hasMatch)
                {
                    dotNetType = matchResponse.dotNetType;
                }
            }
        }

        if (dotNetType is not null)
        {
            var memberName = memberInfo.Name;
            if (memberName == "checked")
            {
                memberName = "@" + memberName;
            }

            lines.Add("[ReactProp]");

            if (dotNetType == "dynamic")
            {
                lines.Add("[ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]");
                lines.Add($"public dynamic {memberInfo.Name} {{ get; }} = new ExpandoObject();");
                return lines;
            }

            if (dotNetType == "init_only_style_map")
            {
                lines.Add("[ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]");
                lines.Add($"public Dictionary<string, Style> {memberInfo.Name} {{ get; }} = new ();");
                return lines;
            }

            lines.Add($"public {dotNetType} {memberName} {{ get; set; }}");
        }

        return lines;
    }

    public static (bool success, (string comment, string name, IReadOnlyList<Token> remainingPart))
        ParseMemberTokens(IReadOnlyList<Token> tokens)
    {
        tokens = tokens.Where(t => t.tokenType != TokenType.Space).ToList();

        var i = 0;

        string comment = null;

        if (tokens[i].tokenType == TokenType.Comment)
        {
            comment = tokens[i].value;

            i++;
        }

        if (tokens[i].tokenType == TokenType.AlfaNumeric)
        {
            var name = tokens[i].value;

            i++;

            if (tokens[i].tokenType == TokenType.QuestionMark)
            {
                i++;
            }

            return (true, (comment, name, tokens.ToList().GetRange(i, tokens.Count - i)));
        }

        return (false, default);
    }

    static (Exception exception, IReadOnlyList<string> lines) CalculateCSharpFileContentLines(ExportInput input)
    {
        var (exception, hasRead, _, tokens) = TsLexer.ParseTokens(input.DefinitionTsCode, 0);
        if (exception is not null)
        {
            return (exception, null);
        }

        if (!hasRead)
        {
            return (null, new List<string>());
        }

        var (isFound, indexOfLastMatchedToken) = TsParser.FindMatch(tokens, 0, TsLexer.ParseTokens(input.StartFrom, 0).tokens);
        if (!isFound)
        {
            return (null, new List<string>());
        }

        (hasRead, var members, _) = TsParser.TryReadMembers(tokens, indexOfLastMatchedToken);
        if (!hasRead)
        {
            return (null, new List<string>());
        }
        
        var lines = new List<string>
        {
            "// auto generated code (do not edit manually)",
            string.Empty,
            $"namespace ReactWithDotNet.ThirdPartyLibraries.{input.NamespaceName};",
            string.Empty
        };

        var inheritPart = " : " + input.BaseClassName;

        var classModifier = input.ClassModifier;

        if (classModifier == "partial")
        {
            inheritPart = string.Empty;
        }

        if (!string.IsNullOrWhiteSpace(classModifier))
        {
            classModifier += " ";
        }

        lines.Add($"public {classModifier}class {input.ClassName}{inheritPart}");

        lines.Add("{");

        var isFirstMember = true;

        foreach (var tsMemberInfo in members)
        {
            if (input.SkipMembers?.Contains(tsMemberInfo.Name) == true)
            {
                continue;
            }

            if (!isFirstMember)
            {
                lines.Add(string.Empty);
            }

            isFirstMember = false;

            lines.AddRange(AsCSharpMember(input, tsMemberInfo));
        }

        if (input.ExtraProps is not null)
        {
            foreach (var extraProp in input.ExtraProps)
            {
                lines.Add(string.Empty);
                lines.Add("[ReactProp]");
                lines.Add($"public {extraProp} {{ get; set; }}");
            }
        }

        if (input.IsContainer)
        {
            lines.Add(string.Empty);
            lines.Add("protected override Element GetSuspenseFallbackElement()");
            lines.Add("{");
            lines.Add("return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();");
            lines.Add("}");

            lines.Add(string.Empty);
            lines.Add($"public static IModifier Modify(Action<{input.ClassName}> modifyAction) => CreateThirdPartyReactComponentModifier(modifyAction);");
        }

        lines.Add("}");

        return (null, lines);

    }
}