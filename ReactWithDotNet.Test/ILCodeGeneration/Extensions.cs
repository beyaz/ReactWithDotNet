global using static ReactWithDotNet.Test.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using FluentAssertions;
using Mono.Cecil;


namespace ReactWithDotNet.Test;

static  class Extension
{
    public static void AsJsonShouldBe(this object value,  string expectedJson)
    {
        var actual = System.Text.Json.JsonSerializer.Serialize(value);


        actual = actual.Trim();
        
        expectedJson = expectedJson.Trim();
        
        actual.Should().Be(expectedJson);
        
            
    }
    
    public  static TypeDefinition GetTypeDefinition<T>()
    {
        var assemblyDefinition = AssemblyDefinition.ReadAssembly(typeof(Extension).Assembly.Location);
        
        
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