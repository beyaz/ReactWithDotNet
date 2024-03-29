﻿using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ReactWithDotNet.WebSite.HelperApps;

static class DynamicCode
{
    public static (bool isCompiledSuccessfully, byte[] bytesOfAssembly, string compileError) Compile(IReadOnlyList<string> sourceCodes)
    {
        using (var peStream = new MemoryStream())
        {
            var result = GenerateCode(sourceCodes).Emit(peStream);

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

    public static (bool isTypeFound, Type type, AssemblyLoadContext assemblyLoadContext, bool sourceCodeHasError, string sourceCodeError) LoadAndFindType(IReadOnlyList<string> sourceCodes, string fullTypeName)
    {
        var (isCompiledSuccessfully, bytesOfAssembly, compileError) = Compile(sourceCodes);
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
                TryClear(assemblyLoadContext);

                return default;
            }

            return (isTypeFound: true, type, assemblyLoadContext, default, default);
        }
    }

    public static bool TryClear(AssemblyLoadContext assemblyLoadContext)
    {
        return TryClear(new WeakReference(assemblyLoadContext));
    }

    static CSharpCompilation GenerateCode(IReadOnlyList<string> sourceCodes)
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
        
        return CSharpCompilation.Create("Hello.dll",
                                        sourceCodes.Select(sourceCode => SyntaxFactory.ParseSyntaxTree(SourceText.From(globalUsings+sourceCode), options)).ToArray(),
                                        references,
                                        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                                                                     optimizationLevel: OptimizationLevel.Release,
                                                                     assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
    }

    static bool TryClear(WeakReference weakReference)
    {
        for (var i = 0; i < 8 && weakReference.IsAlive; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        return weakReference.IsAlive == false;
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