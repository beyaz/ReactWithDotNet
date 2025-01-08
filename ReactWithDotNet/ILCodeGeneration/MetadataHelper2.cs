using Mono.Cecil;

namespace ReactWithDotNet;

public class MetadataHelper2
{
    internal static object[] GetMetadata(MethodDefinition methodDefinition)
    {
        return
        [
            methodDefinition.Name,
        ];
    }
}