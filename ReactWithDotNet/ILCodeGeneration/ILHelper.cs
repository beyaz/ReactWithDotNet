using System.Text.Json;
using Mono.Cecil;

namespace ReactWithDotNet.ILCodeGeneration;

class Deneme17
{
    public readonly string abc = "abc";
}

public static class ILHelper
{
    public static string Deneme2<A,B,C>(DenemeClass h, string p0, int[] arr = null, int[,,] arr2 = null)
    {
        return string.Empty;
    }

    public class DenemeClass
    {
        public string hhhhh { get; set; }
    }
    public static string Deneme(string p0)
    {
        

        if (p0 == "a")
        {
            try
            {
                return "a";
            }
            catch (Exception)
            {
                return "t";
            }
        }


        object x = p0;
        if (x is string)
        {
            Deneme2<string, int, int>(default,"g");
        }

        return "b";
    }

    internal static string denemeeee()
    {
        var assemblyDefinition = AssemblyDefinition.ReadAssembly(typeof(ILHelper).Assembly.Location);

        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType("ReactWithDotNet.ILCodeGeneration", "Deneme17");

            var metadataTable = new MetadataTable();
            
            var typeModel = typeDefinition.AsModel(metadataTable);

            return JsonSerializer.Serialize(new {metadataTable, value=typeModel }, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            });
            
          
        }

        return null;
    }

   

   
    
   
    
    
   

}