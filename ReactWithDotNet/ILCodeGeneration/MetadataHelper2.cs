using Mono.Cecil;
using ReactWithDotNet.Tokenizing;

namespace ReactWithDotNet;

class MyClass1_2
{
    public static string M1(string p0, int p1,string p3)
    {
        return "A";
    }
}
class MyClass1_1
{
    public static string M1(string p0, int p1,string p3)
    {
        return "A";
    }
}
class MyClass1
{
    public static string M1(string p0, int p1,string p3, MyClass1_1 p4, MyClass1_2 p5)
    {
        return "A";
    }
}

public static class MetadataHelper2
{
    static uint[] GetToken(this TypeReference typeReference)
    {
        if (typeReference == typeReference.Module.TypeSystem.String)
        {
            return [1,(uint)typeReference.MetadataType];
        }

        return [(uint)typeReference.MetadataToken.TokenType,typeReference.MetadataToken.RID];
    }
    internal static (uint Token, object[] Value) GetMetadata(MethodDefinition methodDefinition)
    {
        return (methodDefinition.MetadataToken.ToUInt32(), 
        [
            methodDefinition.Name,
            methodDefinition.ReturnType.GetToken(),
            methodDefinition.Parameters.Select(p=>new object[]
            {   
                p.Name,
                p.ParameterType.GetToken()
            })
        ]);
    }
    
    public static object Inspect()
    {
        var methodDefinition = GetTypeDefinition<MyClass1>().Methods.First(x => x.Name == nameof(MyClass1.M1));

        return GetMetadata(methodDefinition);
    }
    
    public  static TypeDefinition GetTypeDefinition<T>()
    {
        var assemblyDefinition = AssemblyDefinition.ReadAssembly(typeof(MetadataHelper2).Assembly.Location);
        
        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            foreach (var typeDefinition in moduleDefinition.Types)
            {
                if (typeDefinition.FullName == typeof(T).FullName)
                {
                    return typeDefinition;
                }
            }
        }
        
        throw new Exception("Type not found");
    }
}