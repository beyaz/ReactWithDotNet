using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ReactWithDotNet.WebSite;

static class DynamicCode
{
    public static (bool isCompiledSuccessfully, byte[] bytesOfAssembly, string compileError) Compile(string assemblyName, IReadOnlyList<string> sourceCodes)
    {
        using (var peStream = new MemoryStream())
        {
            var result = GenerateCode(assemblyName, sourceCodes).Emit(peStream);

            if (!result.Success)
            {
                var compileErrors = result.Diagnostics
                    .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage());

                return (default, default, string.Join(Environment.NewLine, compileErrors));
            }

            peStream.Seek(0, SeekOrigin.Begin);

            return (isCompiledSuccessfully: true, peStream.ToArray(), default);
        }
    }

    public static (bool isTypeFound, Type type, AssemblyLoadContext assemblyLoadContext, bool sourceCodeHasError, string sourceCodeError)
        LoadAndFindType(string assemblyName, IReadOnlyList<string> sourceCodes, string fullTypeName)
    {
        var (isCompiledSuccessfully, bytesOfAssembly, compileError) = Compile(assemblyName, sourceCodes);
        if (!isCompiledSuccessfully)
        {
            return (isTypeFound: default, type: default, assemblyLoadContext: default, sourceCodeHasError: true, sourceCodeError: compileError);
        }

        using (var asm = new MemoryStream(bytesOfAssembly))
        {
            var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

            var assembly = assemblyLoadContext.LoadFromStream(asm);

            var type = assembly.GetType(fullTypeName);
            if (type == null)
            {
                assemblyLoadContext.Unload();

                return default;
            }

            return (isTypeFound: true, type, assemblyLoadContext, default, default);
        }
    }

    static CSharpCompilation GenerateCode(string assemblyName, IReadOnlyList<string> sourceCodes)
    {
        var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview);

        var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Component).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(div).Assembly.Location)
        };

        Assembly.GetEntryAssembly()?.GetReferencedAssemblies().ToList()
            .ForEach(a => references.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));

        const string globalUsings = """
                                    global using System;
                                    global using System.Linq.Expressions;
                                    global using ReactWithDotNet;
                                    global using System.Collections.Generic;
                                    global using System.Linq;
                                    global using static ReactWithDotNet.Mixin;
                                    global using System.Threading.Tasks;
                                    """;

        return CSharpCompilation.Create(assemblyName,
                                        sourceCodes.Select(sourceCode => SyntaxFactory.ParseSyntaxTree(SourceText.From(globalUsings + sourceCode), options)).ToArray(),
                                        references,
                                        new(OutputKind.DynamicallyLinkedLibrary,
                                            optimizationLevel: OptimizationLevel.Release,
                                            assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
    }

    class SimpleUnloadableAssemblyLoadContext : AssemblyLoadContext
    {
        public SimpleUnloadableAssemblyLoadContext()
            : base(true)
        {
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}