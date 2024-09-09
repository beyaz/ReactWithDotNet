


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ReactWithDotNet;




[Generator]
public class FastSerializeJsonConverterGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            //System.Diagnostics.Debugger.Launch();  // This will prompt to attach a debugger
        }

        // Register a syntax receiver to collect classes with the custom attribute
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        
        
        
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
            return;

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
                    }

                    
                }
            }
            
            
            lines.Add("    writer.WriteEndObject();");
            lines.Add("  }"); // close method
            lines.Add("}"); // close class

            
            context.AddSource($"{className}.generated", SourceText.From(string.Join(Environment.NewLine,lines), Encoding.UTF8));
        }
    }

    // Helper to extract namespace
    private string GetNamespace(ClassDeclarationSyntax classDeclaration)
    {
        var fileScopedNamespaceName = classDeclaration.FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>()?.Name;
        if (fileScopedNamespaceName != null)
        {
            return fileScopedNamespaceName.ToString();
        }

        var namespaceDeclaration = classDeclaration.Ancestors()
            .OfType<NamespaceDeclarationSyntax>()
            .FirstOrDefault();
        
        
        
        
        return namespaceDeclaration?.Name.ToString() ?? "global";
    }
    
}

class SyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> CandidateClasses { get; } = new List<ClassDeclarationSyntax>();

    // Called for every syntax node in the compilation, we gather candidates here
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        
        
        // Check if the node is a class declaration with the [AddExtraProperties] attribute
        if (syntaxNode is ClassDeclarationSyntax classDeclaration &&
            classDeclaration.AttributeLists
                .SelectMany(al => al.Attributes)
                .Any(ad => ad.Name.ToString() == "FastSerialize" ))
        {
            CandidateClasses.Add(classDeclaration);
        }
    }
}

static class Extensions
{
    /// <summary>
    ///     Removes value from end of str
    /// </summary>
    public static string RemoveFromEnd(this string data, string value)
    {
        return RemoveFromEnd(data, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes from end.
    /// </summary>
    public static string RemoveFromEnd(this string data, string value, StringComparison comparison)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }
}
