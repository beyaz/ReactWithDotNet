global using static ReactWithDotNet.Extensions;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Diagnostics;

namespace ReactWithDotNet;

static class Extensions
{
    
    public static string GetFileFromProject(this SyntaxNodeAnalysisContext context, string fileName)
    {
        var sourceReferenceResolver = context.Compilation.Options.SourceReferenceResolver;
        var normalizedPath = sourceReferenceResolver.NormalizePath(fileName, null);
        var resolvedPath = sourceReferenceResolver.ResolveReference(normalizedPath, null);

        if (resolvedPath != null && File.Exists(resolvedPath))
        {
            return File.ReadAllText(resolvedPath);
        }

        return null;
    }
    
    public static void AttachToDebugger()
    {
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
    }

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