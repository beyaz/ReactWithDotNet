using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ReactWithDotNet.WebSite.HelperApps;

static class DynamicCode
{
    public static (byte[] bytesOfAssembly, IReadOnlyList<string> compileErrors) Compile(string sourceCode)
    {
        using (var peStream = new MemoryStream())
        {
            var result = GenerateCode(sourceCode).Emit(peStream);

            if (!result.Success)
            {
                var compileErrors = result.Diagnostics
                    .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage());

                return (default, compileErrors.ToArray());
            }

            peStream.Seek(0, SeekOrigin.Begin);

            return (peStream.ToArray(), default);
        }
    }

    public static (Exception error, object invocationOutput, AssemblyLoadContext assemblyLoadContext) Execute(string sourceCode, string fullTypeName, string staticMethodName, object[] methodParameters)
    {
        var (bytesOfAssembly, compileErrors) = Compile(sourceCode);
        if (compileErrors?.Count > 0)
        {
            return (new Exception(string.Join(Environment.NewLine, compileErrors)), default,default);
        }

        return LoadAndExecute(bytesOfAssembly, fullTypeName, staticMethodName, methodParameters);
    }
    
    public static object Execute2(string sourceCode, string fullTypeName, string staticMethodName, object[] methodParameters)
    {
        var (bytesOfAssembly, compileErrors) = Compile(sourceCode);
        if (compileErrors?.Count > 0)
        {
            //return (new Exception(string.Join(Environment.NewLine, compileErrors)), default,default);
        }

        return LoadAndExecute(bytesOfAssembly, fullTypeName, staticMethodName, methodParameters).invocationOutput;
    }
    
    public static (Exception error, object invocationOutput, AssemblyLoadContext assemblyLoadContext) LoadAndExecute(byte[] compiledAssembly, string fullTypeName, string staticMethodName, object[] methodParameters)
    {
        using (var asm = new MemoryStream(compiledAssembly))
        {
            var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

            var assembly = assemblyLoadContext.LoadFromStream(asm);

            var type = assembly.GetType(fullTypeName);
            if (type == null)
            {
                return (new MissingMemberException(fullTypeName), default, default);
            }

            var methodInfo = type.GetMethod(staticMethodName);
            if (methodInfo == null)
            {
                return (new MissingMemberException(staticMethodName), default, default);
            }

            try
            {
                var output = methodInfo.Invoke(null, methodParameters);

                TryClear(assemblyLoadContext);
                
                return (default, output, assemblyLoadContext);
                
            }
            catch (Exception exception)
            {
                return (exception, default, assemblyLoadContext);
            }
        }
    }

    public static bool TryClear(AssemblyLoadContext assemblyLoadContext)
    {
        return TryClear(new WeakReference(assemblyLoadContext));
    }

    static CSharpCompilation GenerateCode(string sourceCode)
    {
        var codeString = SourceText.From(sourceCode);
        var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp11);

        var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

        var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ReactComponent).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(div).Assembly.Location)
        };

        Assembly.GetEntryAssembly()?.GetReferencedAssemblies().ToList()
            .ForEach(a => references.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));

        return CSharpCompilation.Create("Hello.dll",
                                        new[] { parsedSyntaxTree },
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