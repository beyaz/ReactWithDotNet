using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Mono.Cecil;
using AssemblyDefinition = Mono.Cecil.AssemblyDefinition;

namespace ReactWithDotNet;

sealed record MetadataRequest
{
    public string AssemblyName { get; init; }
    public string MethodName { get; init; }
    public string NamespaceName { get; init; }
    public string TypeName { get; init; }
}

sealed record MetadataResponse
{
    public string ErrorMessage { get; init; }
    public MetadataTable Metadata { get; init; }
    public bool Success { get; init; }
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
    class PolymorphicJsonConverter<TBase> : JsonConverter<TBase>
    {
        public override TBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException(); 
        }

        public override void Write(Utf8JsonWriter writer, TBase value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            JsonSerializer.Serialize(writer, value, type, options);
        }
    }
    
    static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
        IncludeFields = true,
        PropertyNamingPolicy =null,
        Converters =
        {
            new PolymorphicJsonConverter<MemberReferenceModel>()
        }
    };

    static void Add(this List<MetadataRequest> requests, Type type)
    {
        requests.Add(new MetadataRequest
        {
            AssemblyName  = Path.GetFileName(type.Assembly.Location),
            NamespaceName = type.Namespace,
            TypeName      = type.Name
        });
    }
    public static async Task GetMetadata(HttpContext httpContext, Func<string, bool> isTypeForbiddenToSendClient = null)
    {
        var requests = await JsonSerializer.DeserializeAsync<List<MetadataRequest>>(httpContext.Request.Body);

        requests.Add(typeof(InterpreterBridge));
        requests.Add(typeof(_System_.Exception));
        requests.Add(typeof(_System_.String));
        requests.Add(typeof(_System_.Object));
        

        requests.Reverse();
        

        await httpContext.Response.WriteAsJsonAsync(GetMetadata(requests, isTypeForbiddenToSendClient), JsonSerializerOptions);
    }

    static TypeDefinition FindType(this AssemblyDefinition assemblyDefinition, MetadataRequest request)
    {
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
    
    
    
    static MetadataResponse GetMetadata(IEnumerable<MetadataRequest> requests, Func<string, bool> isTypeForbiddenToSendClient = null)
    {
        var metadataTable = new MetadataTable();

        foreach (var request in requests)
        {
            var assemblyFilePath = Path.Combine(Path.GetDirectoryName(typeof(Mixin).Assembly.Location) ?? string.Empty, request.AssemblyName);
            if (!File.Exists(assemblyFilePath))
            {
                return new()
                {
                    ErrorMessage = $"FileNotFound. @file: {assemblyFilePath}"
                };
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
            Success  = true,
            Metadata = metadataTable
        };
    }
}