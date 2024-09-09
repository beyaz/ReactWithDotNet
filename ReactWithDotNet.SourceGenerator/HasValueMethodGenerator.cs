using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ReactWithDotNet;

[Generator]
public class HasValueMethodGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
        {
            return;
        }

        // Loop through collected classes
        foreach (var classDeclaration in receiver.CandidateClasses)
        {
            // Get the class name
            var className = classDeclaration.Identifier.Text;

            var lines = new List<string>
            {
                $"namespace {GetNamespace(classDeclaration)};",

                $"static class {className}HasValueChecker",
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

            context.AddSource($"{className}HasValueChecker.generated", SourceText.From(string.Join(Environment.NewLine, lines), Encoding.UTF8));
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // AttachToDebugger();

        // Register a syntax receiver to collect classes with the custom attribute
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    // Helper to extract namespace
    static string GetNamespace(ClassDeclarationSyntax classDeclaration)
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
                    .Any(ad => ad.Name.ToString() == "GenerateHasValue"))
            {
                CandidateClasses.Add(classDeclaration);
            }
        }
    }
}