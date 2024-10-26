using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class ILHelperTests
{
    [TestMethod]
    public void GenerateIL()
    {
        var response = ILHelper.GetMethodBody();
        
        
    }
}