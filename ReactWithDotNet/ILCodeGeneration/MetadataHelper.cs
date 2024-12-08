using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Mono.Cecil;
using AssemblyDefinition = Mono.Cecil.AssemblyDefinition;

namespace ReactWithDotNet;

[AttributeUsage(AttributeTargets.All)]
public sealed class ExternalAttribute : Attribute
{
}

[External]
public static class console
{
    public static extern void log(object data);
}

[External]
public static class Math
{
    public static extern double max(double a, double b);
    public static extern int max(int a, int b);
}

sealed class MetadataRequest
{
    public bool IsInitialRequest { get; init; }
    
    public List<Item> RequestedTypes { get; init; }
    
    public sealed class Item
    {
        public string AssemblyName { get; init; }
        public string MethodName { get; init; }
        public string NamespaceName { get; init; }
        public string TypeName { get; init; }
        public Item DeclaringType { get; init; }
    }
}

sealed class MetadataResponse
{
    public string ErrorMessage { get; init; }
    public MetadataTable Metadata { get; init; }
    public int Success { get; init; }
}

static partial class Mixin
{
    public static string MetadataRequestHandlerPath;

    public static Task GetMetadata(HttpContext httpContext)
    {
        return MetadataHelper.GetMetadata(httpContext);
    }
}

static class MetadataHelper
{
    static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
        IncludeFields = true,
        PropertyNamingPolicy =null
    };

    static void Add(this List<MetadataRequest.Item> requests, Type type)
    {
        requests.Add(new()
        {
            AssemblyName  = Path.GetFileName(type.Assembly.Location),
            NamespaceName = type.Namespace,
            TypeName      = type.Name
        });
    }
    public static async Task GetMetadata(HttpContext httpContext, Func<string, bool> isTypeForbiddenToSendClient = null)
    {
        var request = await JsonSerializer.DeserializeAsync<MetadataRequest>(httpContext.Request.Body);

        if (request.IsInitialRequest)
        {
            request.RequestedTypes.Add(typeof(Nullable<>));
            request.RequestedTypes.Add(typeof(InterpreterBridge));
            request.RequestedTypes.Add(typeof(ExternalAttribute));
            request.RequestedTypes.Add(typeof(console));
            request.RequestedTypes.Add(typeof(Math));
            request.RequestedTypes.Add(typeof(_System_.Int64));
            request.RequestedTypes.Add(typeof(Exception));
            request.RequestedTypes.Add(typeof(SystemException));
            request.RequestedTypes.Add(typeof(NullReferenceException));
            request.RequestedTypes.Add(typeof(_System_.String));
            request.RequestedTypes.Add(typeof(_System_.Object));

            request.RequestedTypes.Reverse();
        }
        
        
        

        await httpContext.Response.WriteAsJsonAsync(GetMetadata(request.RequestedTypes, isTypeForbiddenToSendClient), JsonSerializerOptions);
    }

    static TypeDefinition FindType(this AssemblyDefinition assemblyDefinition, MetadataRequest.Item request)
    {
        if (request.DeclaringType is not null)
        {
            var declaringType = FindType(assemblyDefinition, request.DeclaringType);

            foreach (var nestedType in declaringType.NestedTypes)
            {
                if (nestedType.Name == request.TypeName)
                {
                    return nestedType;
                }
            }
        }
        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType(request.NamespaceName, request.TypeName);
            if (typeDefinition != null)
            {
                return typeDefinition;
            }
        }

        return null;
    }
    
    public static TypeDefinition FindType(this AssemblyDefinition assemblyDefinition, Type type)
    {
        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType(type.Namespace, type.Name);
            if (typeDefinition != null)
            {
                return typeDefinition;
            }
        }

        return null;
    }

    
    internal static  AssemblyDefinition AssemblyDefinitionOfCore => AssemblyDefinition.ReadAssembly(typeof(Mixin).Assembly.Location);
    
    
    
    static MetadataResponse GetMetadata(IEnumerable<MetadataRequest.Item> requests, Func<string, bool> isTypeForbiddenToSendClient = null)
    {
        var metadataTable = new MetadataTable();

        foreach (var request in requests)
        {
            var assemblyFilePath = Path.Combine(Path.GetDirectoryName(typeof(Mixin).Assembly.Location) ?? string.Empty, request.AssemblyName);
            if (!File.Exists(assemblyFilePath))
            {
                if ( request.AssemblyName == "System.Private.CoreLib.dll")
                {
                    assemblyFilePath = typeof(string).Assembly.Location;
                }
                else
                {
                    return new()
                    {
                        ErrorMessage = $"FileNotFound. @file: {assemblyFilePath}"
                    };
                }
                
            }

            var typeDefinition = AssemblyDefinition.ReadAssembly(assemblyFilePath).FindType(request);
            if (typeDefinition is null)
            {
                return new()
                {
                    ErrorMessage = $"TypeNotFound. @file: {assemblyFilePath}"
                };
            }

            if (isTypeForbiddenToSendClient?.Invoke(typeDefinition.FullName) is true)
            {
                return new()
                {
                    ErrorMessage = $"TypeIsForbiddenToSerializeClient. @type: {typeDefinition.FullName}"
                };
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

        return new()
        {
            Success  = 1,
            Metadata = metadataTable
        };
    }
}