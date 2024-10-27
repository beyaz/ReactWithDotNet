using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Mono.Cecil;
using AssemblyDefinition = Mono.Cecil.AssemblyDefinition;

namespace ReactWithDotNet.ILCodeGeneration;

class Deneme17
{
    public readonly string abc = "abc";
}

sealed record MetadataRequest
{
    public string AssemblyName { get; init; }
    public string MethodName { get; init; }
    public string NamesapceName { get; init; }
    public string TypeName { get; init; }
}

public static class ILHelper
{
    static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
        IncludeFields = true
    };

    public static Task GetMetadata(HttpContext httpContext)
    {
        // Sample: "(ReactWithDotNet.dll-ReactWithDotNet.ILCodeGeneration-Deneme17-*)"

        var requests = ParseQuery(httpContext.Request.Query["query"].FirstOrDefault());

        return httpContext.Response.WriteAsJsonAsync(GetMetadata(requests), JsonSerializerOptions);

        static IEnumerable<MetadataRequest> ParseQuery(string query)
        {
            return query.Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(parseOne);

            static MetadataRequest parseOne(string text)
            {
                var arr = text.Split("-");

                return new()
                {
                    AssemblyName  = arr[0],
                    NamesapceName = arr[1],
                    TypeName      = arr[2],
                    MethodName    = arr[3]
                };
            }
        }
    }

    static TypeDefinition FindType(this AssemblyDefinition assemblyDefinition, MetadataRequest request)
    {
        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType(request.NamesapceName, request.TypeName);
            if (typeDefinition != null)
            {
                return typeDefinition;
            }
        }

        return null;
    }

    static (MetadataTable value, bool success, string errorMessage) GetMetadata(IEnumerable<MetadataRequest> requests)
    {
        var metadataTable = new MetadataTable();

        foreach (var request in requests)
        {
            var assemblyFilePath = Path.Combine(Path.GetDirectoryName(typeof(ILHelper).Assembly.Location) ?? string.Empty, request.AssemblyName);
            if (!File.Exists(assemblyFilePath))
            {
                return (default, default, $"FileNotFound. @file: {assemblyFilePath}");
            }

            var typeDefinition = AssemblyDefinition.ReadAssembly(assemblyFilePath).FindType(request);
            if (typeDefinition is null)
            {
                return (default, default, $"TypeNotFound. @file: {assemblyFilePath}");
            }

            if (request.MethodName is null || request.MethodName == "*")
            {
                metadataTable.Import(typeDefinition);
                continue;
            }

            foreach (var methodDefinition in typeDefinition.Methods.Where(m => m.Name.Contains(request.MethodName, StringComparison.OrdinalIgnoreCase)))
            {
                metadataTable.Import(methodDefinition);
            }
        }

        return (metadataTable, true, default);
    }
}