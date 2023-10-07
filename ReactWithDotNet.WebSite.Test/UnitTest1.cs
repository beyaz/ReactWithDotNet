using FluentAssertions;
using static ReactWithDotNet.WebSite.HelperApps.HtmlToReactWithDotNetCsharpCodeConverter;

namespace ReactWithDotNet.WebSite.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var html = """
                       <a target='_blank' />
                       """;

            var expected = """
                           new a { target = "_blank" }
                           """;
            
            HtmlToCSharp(html).Should().Be(expected);
        }
    }
}