using ReactWithDotNet.TypeScriptCodeAnalyzer;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.Mixin;

namespace ReactWithDotNet.Exporting;

static class Exporter
{
    public static void ExportToCSharpFile(ExportInput input)
    {
        input.DefinitionTsCode = FixSourceCode(input.DefinitionTsCode);
        
        var code = CalculateCSharpFileContentLines(input).GetValue().ToCSharpCode();

        const string projectFolder = @"C:\github\ReactWithDotNet\ReactWithDotNet\ThirdPartyLibraries";

        WriteAllText($@"{projectFolder}{input.OutputFileLocation}{input.ClassName}.cs", code);
    }

    static string FixSourceCode(string tsCode)
    {
        return tsCode.Replace(" | undefined", string.Empty)
            .Replace("Partial<PopperProps>;","dynamic;");
    }

    static bool IsMuiPartialType(TsMemberInfo memberInfo)
    {
        var tokens = memberInfo.RemainingPart?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count > 1)
        {
            if (tokens[0].value == "Partial")
            {
                return true;
            }
        }

        return false;
    }

    static bool StartsWith(this IReadOnlyList<Token> tokens, string value)
    {
        tokens = tokens?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count > 1)
        {
            var reactNode = TsLexer.ParseTokens(value, 0);
            if (reactNode.hasRead)
            {
                if (TsParser.FindMatch(tokens, 0, reactNode.tokens).isFound)
                {
                    return true;
                }
            }
        }

        return false;
    }

    static bool IsReactNode(TsMemberInfo memberInfo)
        => memberInfo.RemainingPart.StartsWith("React.ReactNode");
    

    static (bool hasMatch, string dotNetType) TryMatchDotNetType(TsMemberInfo memberInfo)
    {
        var tokens = memberInfo.RemainingPart?.Where(IsNotSpace).Where(IsNotColon).ToList() ?? new List<Token>();
        if (tokens.Count > 0)
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

            if (tokens[0].tokenType == TokenType.LeftBrace && tokens[^1].tokenType == TokenType.RightBrace)
            {
                return (true, "dynamic");
            }

            if (IsReactNode(memberInfo))
            {
                return (true, "Element");
            }

            var (hasRead, tsTypeReference, _) = TsParser.TryReadUnionTypeReference(tokens, 0);
            if (hasRead)
            {
                if (tsTypeReference.UnionTypes?.All(t=>t.IsStringValue)==true)
                {
                    return (true, "string");
                }
            }
        }

        return default;
    }

    static bool IsNotSpace(Token t) => t.tokenType != TokenType.Space;
    static bool IsNotColon(Token t) => t.tokenType != TokenType.Colon;
    
    static IReadOnlyList<string> AsCSharpMember(TsMemberInfo memberInfo)
    {
        var lines = new List<string>();

        if (memberInfo.Comment is not null)
        {
            lines.AddRange(AsCSharpComment(memberInfo.Comment));
        }

        if (memberInfo.Name == "sx")
        {
            lines.Add("[ReactProp]");
            lines.Add("[ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]");
            lines.Add("public dynamic sx { get; } = new ExpandoObject();");

            return lines;
        }

        if (memberInfo.Name == "classes")
        {
            if ((memberInfo.PropertyType?.Name == "Partial" || IsMuiPartialType(memberInfo)))
            {
                lines.Add("[ReactProp]");
                lines.Add("[ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]");
                lines.Add($"public Dictionary<string, Style> {memberInfo.Name} {{ get; }} = new ();");
                return lines;
            }
            
        }

        
        
        var (hasMatch, dotNetType) = TryMatchDotNetType(memberInfo);
        if (hasMatch)
        {
            lines.Add("[ReactProp]");

            if (dotNetType == "dynamic")
            {
                lines.Add("[ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]");
                lines.Add($"public dynamic {memberInfo.Name} {{ get; }} = new ExpandoObject();");
                return lines;
            }
            lines.Add($"public {dotNetType} {memberInfo.Name} {{ get; set; }}");

            return lines;
        }


        // export as property
        if (memberInfo.PropertyType is not null)
        {
            if (memberInfo.PropertyType.Name == "React.Ref")
            {
                return lines;
            }

            var exportAsDynamicObjectMap = asCSharpType(memberInfo.PropertyType) == "dynamic";

            lines.Add("[ReactProp]");

            if (exportAsDynamicObjectMap)
            {
                lines.Add("[ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]");
                lines.Add($"public dynamic {memberInfo.Name} {{ get; }} = new ExpandoObject();");
                return lines;
            }

            var memberName = memberInfo.Name;
            if (memberName == "checked")
            {
                memberName = "@" + memberName;
            }

            lines.Add("public " + asCSharpType(memberInfo.PropertyType) + " " + memberName + " { get; set; }");

            static string asCSharpType(TsTypeReference tsTypeReference)
            {
                if (tsTypeReference.TokenListAsUnionValues?.Count > 0)
                {
                    return "string";
                }

                if (tsTypeReference.TokenListAsObjectMap?.Count > 0)
                {
                    return "dynamic";
                }

                if (tsTypeReference.IsUnionType && tsTypeReference.UnionTypes.All(x=>x.IsStringValue))
                {
                    return "string";
                }
                
                if (tsTypeReference.Name.Equals("string", StringComparison.OrdinalIgnoreCase))
                {
                    return "string";
                }

                if (tsTypeReference.Name.Equals("React.InputHTMLAttributes", StringComparison.OrdinalIgnoreCase))
                {
                    return "string";
                }

                if (tsTypeReference.Name.Equals("unknown", StringComparison.OrdinalIgnoreCase))
                {
                    return "string";
                }

                if (tsTypeReference.Name.Equals("OverridableStringUnion", StringComparison.OrdinalIgnoreCase))
                {
                    return "string";
                }

                if (tsTypeReference.Name.Equals("Partial", StringComparison.OrdinalIgnoreCase))
                {
                    return "dynamic";
                }

                if (tsTypeReference.Name.Equals("number", StringComparison.OrdinalIgnoreCase))
                {
                    return "double?";
                }

                if (tsTypeReference.Name.Equals("boolean", StringComparison.OrdinalIgnoreCase))
                {
                    return "bool?";
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
        if (hasRead)
        {
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

                lines.AddRange(AsCSharpMember(tsMemberInfo));
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

        return (null, new List<string>());
    }
}