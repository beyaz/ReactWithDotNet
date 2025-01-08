using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class ILCodeGenerationTest
{
    [TestMethod]
    public void M1()
    {
        var methodDefinition = GetTypeDefinition<MyClass1>().Methods.First(x => x.Name == nameof(MyClass1.M1));

        var response = MetadataHelper2.GetMetadata(methodDefinition);

        const string expectedJson =
            """
            ["M1"]
            """;

        response.AsJsonShouldBe(expectedJson);
    }
}

class MyClass1
{
    public static string M1()
    {
        return "A";
    }
}