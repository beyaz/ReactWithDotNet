﻿using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ReactWithDotNet.RoslynHelper;
using System.Collections.Immutable;

namespace ReactWithDotNet;

[Generator(LanguageNames.CSharp)]
public class FastSerializeJsonConverterGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null);

        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax classDeclaration &&
               classDeclaration.AttributeLists
                   .SelectMany(al => al.Attributes)
                   .Any(ad => ad.Name.ToString() == "FastSerialize");
    }

    static ClassDeclarationSyntax GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;
        return classDeclaration;
    }

    static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            return;
        }

        Dictionary<string, IReadOnlyList<string>> cache = new();

        foreach (var classDeclaration in classes)
        {
            var className = classDeclaration.Identifier.Text;

            var lines = new List<string>
            {
                "using System.Text.Json.Serialization;",
                "using System.Text.Json;",
                $"namespace {GetNamespace(classDeclaration)};",

                $"sealed class {className}JsonConverter : JsonConverter<{className}>",
                "{",
                $"    public override {className} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)",
                "    {",
                "        throw new NotImplementedException();",
                "    }",
                $"    public override void Write(Utf8JsonWriter writer, {className} value, JsonSerializerOptions options)",
                "    {",
                "        writer.WriteStartObject();"
            };

            foreach (var member in classDeclaration.Members)
            {
                if (member is PropertyDeclarationSyntax propertyDeclarationSyntax)
                {
                    var propertyTypeName = propertyDeclarationSyntax.Type.ToFullString().Trim();
                    var propertyName = propertyDeclarationSyntax.Identifier.ValueText;

                    if (propertyTypeName == "double?")
                    {
                        lines.Add($"if (value.{propertyName}.HasValue)");
                        lines.Add("{");
                        lines.Add($"  writer.WritePropertyName(\"{propertyName}\");");
                        lines.Add($"  writer.WriteNumberValue(value.{propertyName}.Value);");
                        lines.Add("}");
                        continue;
                    }

                    if (propertyTypeName == "bool?")
                    {
                        lines.Add($"if (value.{propertyName}.HasValue)");
                        lines.Add("{");
                        lines.Add($"  writer.WritePropertyName(\"{propertyName}\");");
                        lines.Add($"  writer.WriteBooleanValue(value.{propertyName}.Value);");
                        lines.Add("}");
                        continue;
                    }

                    var found = FindClassByNameAndNamespace(compilation, propertyTypeName, GetNamespace(classDeclaration));
                    if (found is not null && cache.ContainsKey(propertyTypeName) is false)
                    {
                        cache[propertyTypeName] = GetSourceTextOfHasValueChecker(found).value;
                    }

                    var serializeAsNullWhenEmpty = propertyDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes).Any(ad => ad.Name.ToString() == "SerializeAsNullWhenEmpty");
                    if (serializeAsNullWhenEmpty)
                    {
                        lines.Add($"if (HasValueChecker.HasValue(value.{propertyName}))");
                        lines.Add("{");
                        lines.Add($"  writer.WritePropertyName(\"{propertyName}\");");
                        lines.Add($"  JsonSerializer.Serialize(writer, value.{propertyName}, options);");
                        lines.Add("}");
                    }
                    else
                    {
                        lines.Add($"if (value.{propertyName} != null)");
                        lines.Add("{");
                        lines.Add($"  writer.WritePropertyName(\"{propertyName}\");");
                        lines.Add($"  JsonSerializer.Serialize(writer, value.{propertyName}, options);");
                        lines.Add("}");
                    }
                }
            }

            lines.Add("    writer.WriteEndObject();");
            lines.Add("  }"); // close method
            lines.Add("}"); // close class

            foreach (var keyValuePair in cache)
            {
                lines.AddRange(keyValuePair.Value);
            }

            context.AddSource($"{className}.generated", SourceText.From(string.Join(Environment.NewLine, lines), Encoding.UTF8));
        }
    }

    static (IReadOnlyList<string> value, Exception exception) GetSourceTextOfHasValueChecker(ClassDeclarationSyntax classDeclaration)
    {
        var className = classDeclaration.Identifier.Text;

        var lines = new List<string>
        {
            "static partial class HasValueChecker",
            "{",
            $"    public static bool HasValue({className} value)",
            "    {",
            "        if(value == null)",
            "        {",
            "           return false;",
            "        }"
        };

        foreach (var member in classDeclaration.Members)
        {
            if (member is PropertyDeclarationSyntax propertyDeclarationSyntax)
            {
                var propertyTypeName = propertyDeclarationSyntax.Type.ToFullString().Trim();
                var propertyName = propertyDeclarationSyntax.Identifier.ValueText;

                if (propertyTypeName == "double?" || propertyTypeName == "bool?")
                {
                    lines.Add($"if (value.{propertyName}.HasValue)");
                    lines.Add("  return true;");
                }
            }
        }

        lines.Add("    return false;");
        lines.Add("  }"); // close method
        lines.Add("}"); // close class

        return (lines, null);
    }
}
