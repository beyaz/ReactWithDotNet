using Newtonsoft.Json.Linq;
using ReactWithDotNet.TypeScriptCodeAnalyzer;
using System.Globalization;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.Mixin;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.TokenMatch;

namespace ReactWithDotNet.Exporting;

static class Exporter
{
    public static void ExportToCSharpFile(ExportInput input)
    {
        var code = CalculateCSharpFileContentLines(input).GetValue().ToCSharpCode();

        const string projectFolder = @"C:\github\ReactWithDotNet\ReactWithDotNet\ThirdPartyLibraries";

        WriteAllText($@"{projectFolder}{input.OutputFileLocation}{input.ClassName}.cs", code);
    }

    static (bool hasMatch, string dotNetType) TryMatchDotNetOneParameterAction(TsMemberInfo memberInfo)
    {
        var tokens = memberInfo.RemainingPart?.Where(IsNotSpace).ToList() ?? new List<Token>();

        string parameterType = null;
        
        if (tokens.FullMatch("(", "event", ":", OnTokenMatched(t=> parameterType = t.value), ")", ":", "void"))
        {
            return (hasMatch: true, dotNetType: $"Action<{parameterType}>");
        }

        return default;
    }

    static (bool success, string dotNetType) ResolveDotNetTypeName(IReadOnlyList<Token> tokens)
    {
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

        if (tokens.FullMatch("string | undefined"))
        {
            return (true, "string");
        }
        
        if (tokens.FullMatch("number | undefined"))
        {
            return (true, "double?");
        }

        if (tokens.FullMatch("boolean | undefined"))
        {
            return (true, "bool?");
        }

        if (tokens.FullMatch("React.CSSProperties | undefined"))
        {
            return (true, "Style");
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

    static (bool hasMatch, string dotNetType) ResolveDotNetTypeName(TsMemberInfo memberInfo)
    {
        var tokens = memberInfo.RemainingPart?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count <= 0)
        {
            return default;
        }

        var (success, dotNetTypeName) = ResolveDotNetTypeName(tokens);
        if (success)
        {
            return (true, dotNetTypeName);
        }

        var (hasMatch, dotNetType) = TryMatchDotNetOneParameterAction(memberInfo);
        if (hasMatch)
        {
            return (true, dotNetType);
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

        if (isVoidFunction())
        {
            var (hasRead, parameters, newIndex) = TypeScriptCodeAnalyzer.TsParser.TryReadFunctionParameters(memberInfo.RemainingPart, 1);
            if (hasRead)
            {
                lines.Add($"public Func<Task,{string.Join(", ",parameters.Select(p=>$"{p.tsTypeReference} {p.parameterName}"))}> {memberInfo.Name} {{get;set;}}");
                return lines;
            }
        }
        
        if (!input.PropToDotNetTypeMap.TryGetValue($"{input.NamespaceName} > {input.ClassName} > {memberInfo.Name}", out var dotNetType))
        {
            if (!input.PropToDotNetTypeMap.TryGetValue($"{input.NamespaceName} > * > {memberInfo.Name}", out dotNetType))
            {
                var matchResponse = ResolveDotNetTypeName(memberInfo);
                if (matchResponse.hasMatch)
                {
                    dotNetType = matchResponse.dotNetType;
                }
            }
        }

        if (dotNetType is null)
        {
            return lines;
        }
        
        
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

        if (dotNetType == "Style")
        {
            lines.Add("[ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenStyleEmpty))]");
            lines.Add($"public Style {memberInfo.Name} {{ get; }} = new ();");
            return lines;
        }

        lines.Add($"public {dotNetType} {memberName} {{ get; set; }}");
        
        lines.Add(string.Empty);
        
        if (memberInfo.Comment is not null)
        {
            lines.AddRange(AsCSharpComment(memberInfo.Comment));
        }
        lines.Add($"public static IModifier {UpperCaseFirstChar(memberName)}({dotNetType} value) => CreateThirdPartyReactComponentModifier<{input.ClassName}>(x => x.{memberName} = value);");

        return lines;

        bool isVoidFunction()
        {
            if (memberInfo.RemainingPart.StartsWith(":("))
            {
                if (memberInfo.RemainingPart.EndsWith("=> void"))
                {
                    
                    return true;
                }
            }
            
            return false;
        }

        static string UpperCaseFirstChar(string str)
        {
            return char.ToUpper(str[0], new CultureInfo("en-US")) + str.Substring(1);
        }
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
            
            lines.Add($"public {input.ClassName}(){{ }}");
            lines.Add(string.Empty);
            lines.Add($"public {input.ClassName}(params Action<{input.ClassName}>[] modifiers) => modifiers.ApplyAll(Add);");
            lines.Add(string.Empty);
            lines.Add($"public {input.ClassName}(StyleModifier styleModifier, params Action<{input.ClassName}>[] modifiers)");
            lines.Add("{");
            lines.Add("Add(styleModifier);");
            lines.Add("modifiers.ApplyAll(Add);");
            lines.Add("}");
            lines.Add(string.Empty);
            lines.Add($"public void Add(Action<{input.ClassName}> modify) => modify?.Invoke(this);");
        }

        lines.Add("}");

        return (null, lines);

    }
}