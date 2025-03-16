namespace ReactWithDotNet;

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer1Analyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "NextJsTsxGenerator";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, "NextJs Tsx Generator", "NextJs Tsx Generator", "NextJs", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "NextJs Tsx Generator");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration); // MethodDeclaration'ları analiz et
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            var methodName = methodDeclaration.Identifier.Text;

            // NextJs.yaml dosyasını oku
            var yamlPath = Path.Combine(context.GetFileFromProject("NextJs.yaml"));
            if (!File.Exists(yamlPath))
            {
                return;
            }

            var yamlContent = File.ReadAllText(yamlPath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yamlData = deserializer.Deserialize<NextJsConfig>(yamlContent);

            // YAML dosyasında metot adını kontrol et
            if (yamlData.Components.ContainsKey(methodName))
            {
                // C# kodunu al
                var methodCode = methodDeclaration.ToString();

                // C# kodunu TSX'e çevir
                var tsxCode = CSharpToTsxConverter.Convert(methodCode);

                // TSX dosyasını oluştur
                GenerateTsxFile(methodName, tsxCode, context.Compilation.Options.SourceReferenceResolver.NormalizePath(context.Compilation.Options.SourceReferenceResolver.ResolveReference(context.Compilation.Options.SourceReferenceResolver.NormalizePath(".", null) ?? string.Empty, null) ?? string.Empty,null));
            }
        }

        private void GenerateTsxFile(string methodName, string tsxContent, string projectRoot)
        {
            var outputPath = Path.Combine(projectRoot, "NextJsComponents", $"{methodName}.tsx");
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            File.WriteAllText(outputPath, tsxContent);
        }
    }

    public class NextJsConfig
    {
        public Dictionary<string, string> Components { get; set; } // Sadece metot adlarını tut
    }

    public static class CSharpToTsxConverter
    {
        public static string Convert(string csharpCode)
        {
            // Önceki CSharpToTsxConverter sınıfının kodunu buraya ekleyin
            var tree = CSharpSyntaxTree.ParseText(csharpCode);
            var root = tree.GetRoot();

            var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault();
            if (methodDeclaration == null)
            {
                return "Method declaration not found.";
            }

            var returnStatement = methodDeclaration.DescendantNodes().OfType<ReturnStatementSyntax>().FirstOrDefault();
            if (returnStatement == null)
            {
                return "Return statement not found.";
            }

            var objectCreation = returnStatement.DescendantNodes().OfType<ObjectCreationExpressionSyntax>().FirstOrDefault();
            if (objectCreation == null)
            {
                return "Object creation expression not found.";
            }

            var tsxCode = ConvertElement(objectCreation);

            return $"export function {methodDeclaration.Identifier.Text}()\n{{\n\treturn (\n\t\t{tsxCode}\n\t);\t\n}}";
        }

        private static string ConvertElement(ObjectCreationExpressionSyntax objectCreation)
        {
            var elementType = objectCreation.Type.ToString();
            var attributes = objectCreation.ArgumentList?.Arguments.Select(arg => arg.ToString()).ToList() ?? new System.Collections.Generic.List<string>();
            var initializers = objectCreation.Initializer?.Expressions.OfType<ObjectCreationExpressionSyntax>().ToList() ?? new System.Collections.Generic.List<ObjectCreationExpressionSyntax>();

            var attributeString = attributes.Any() ? " " + string.Join(" ", attributes) : "";
            var childrenString = string.Join("\n\t\t\t", initializers.Select(ConvertElement));

            var textContent = objectCreation.Initializer?.Expressions.OfType<InitializerExpressionSyntax>().FirstOrDefault()?.Expressions.OfType<LiteralExpressionSyntax>().FirstOrDefault()?.Token.ValueText;
            var textContentString = textContent != null ? textContent : "";

            if (childrenString != "" || textContentString != "")
            {
                return $"<{elementType}{attributeString}>{childrenString}{textContentString}</{elementType}>";
            }
            else
            {
                return $"<{elementType}{attributeString} />";
            }
        }
    }
