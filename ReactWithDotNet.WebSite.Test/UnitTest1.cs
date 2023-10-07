using FluentAssertions;
using static ReactWithDotNet.WebSite.HelperApps.HtmlToReactWithDotNetCsharpCodeConverter;

namespace ReactWithDotNet.WebSite.Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void _0()
    {
        Assert("""
               <a target='_blank' />
               """,

               """
               new a { target = "_blank" }
               """);


        Assert("""
               <a target='_blank' style = "color:rgb(28, 32, 37);border-radius:12px;"/>
               """,

               """
               new a { target = "_blank", style = { color= "rgb(28, 32, 37)", borderRadius = "12px;"} }
               """);
    }


    static void Assert(string html, string expected)
    {
        HtmlToCSharp(html).Should().Be(expected);
    }
}