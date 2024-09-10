using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ReactWithDotNet.RoslynHelper;

namespace ReactWithDotNet;

[Generator]
public class FastSerializeJsonConverterGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
        {
            return;
        }

        Dictionary<string, IReadOnlyList<string>> cache = new();
        
        
        // Loop through collected classes
        foreach (var classDeclaration in receiver.CandidateClasses)
        {
            // Get the class name
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

                    var found = FindClassByNameAndNamespace(context.Compilation, propertyTypeName, GetNamespace(classDeclaration));
                    if (found is not null && cache.ContainsKey(propertyTypeName) is false)
                    {
                        cache[propertyTypeName] = GetSourceTextOfHasValueChecker(found).value;
                    }
                    
                    lines.Add($"if (HasValueChecker.HasValue(value.{propertyName}))");
                    lines.Add("{");
                    lines.Add($"  writer.WritePropertyName(\"{propertyName}\");");
                    lines.Add($"  JsonSerializer.Serialize(writer, value.{propertyName}, options);");
                    lines.Add("}");
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

    public void Initialize(GeneratorInitializationContext context)
    {
         AttachToDebugger();

        // Register a syntax receiver to collect classes with the custom attribute
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    
    static (IReadOnlyList<string> value, Exception exception) GetSourceTextOfHasValueChecker( ClassDeclarationSyntax classDeclaration)
    {
        // Get the class name
        var className = classDeclaration.Identifier.Text;

        var lines = new List<string>
        {
            // $"namespace {GetNamespace(classDeclaration)};",

            $"static partial class HasValueChecker",
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

                if (propertyTypeName == "double?"|| propertyTypeName == "bool?")
                {
                    lines.Add($"if (value.{propertyName}.HasValue)");
                    lines.Add("  return true;");
                    continue;
                }
                    
                    
            }
        }

        lines.Add("    return false;");
        lines.Add("  }"); // close method
        lines.Add("}"); // close class

        return (lines, default);
    }
   

    class SyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClasses { get; } = new();

        // Called for every syntax node in the compilation, we gather candidates here
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // Check if the node is a class declaration with the [AddExtraProperties] attribute
            if (syntaxNode is ClassDeclarationSyntax classDeclaration &&
                classDeclaration.AttributeLists
                    .SelectMany(al => al.Attributes)
                    .Any(ad => ad.Name.ToString() == "FastSerialize"))
            {
                CandidateClasses.Add(classDeclaration);
            }
        }
    }
}