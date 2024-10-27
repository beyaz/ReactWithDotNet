using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactWithDotNet.ILCodeGeneration;

namespace ReactWithDotNet.Test;

[TestClass]
public class ILHelperTests
{
    [TestMethod]
    public void GenerateIL()
    {
        var response = ILHelper.GetMetadata("ReactWithDotNet.dll","ReactWithDotNet.ILCodeGeneration", "Deneme17","*");
        
        
    }
}