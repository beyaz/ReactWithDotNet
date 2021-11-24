using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactDotNet.CSharpExporter.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var input = "type BadgeSizeType = 'normal' | 'large' | 'xlarge';";

            var expected = @"
    [Enum(Emit.StringNameLowerCase)]
    public enum BadgeSizeType
    {
        normal, large, xlarge
    }

";

            var output = MyParser.GetCSharpDefinition(input);

            output.Trim().Should().Be(expected);
        }

    }
}
