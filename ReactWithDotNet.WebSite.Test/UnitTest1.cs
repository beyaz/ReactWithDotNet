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
               new a { target = "_blank", style = { borderRadius = "12px", color = "rgb(28, 32, 37)" } }
               """);

        Assert("""
               <a target='_blank'  aria-hidden="true"   data-testid="AcUnitIcon"  style = "color:rgb(28, 32, 37);border-radius:12px;"/>
               """,

               """
               new a(Aria("hidden", "true"), Data("testid", "AcUnitIcon")) { target = "_blank", style = { borderRadius = "12px", color = "rgb(28, 32, 37)" } }
               """);


        Assert("""
               <a target='_blank'  aria-hidden="true"   data-testid="AcUnitIcon"  style = "color:rgb(28, 32, 37);border-radius:12px;">
               xyz
               </a>
               """,

               """
               new a(Aria("hidden", "true"), Data("testid", "AcUnitIcon"))
               {
                   text = "xyz",
                   target = "_blank",
                   style = { borderRadius = "12px", color = "rgb(28, 32, 37)" }
               }
               """);
        
        
        Assert("""
               <div aria-hidden="true"   data-testid="AcUnitIcon"  style = "display: flex; flexDirection: row; color: blue;">
               
               </div>
               """,

               """
               new FlexRow(Aria("hidden", "true"), Data("testid", "AcUnitIcon")) { style = { color = "blue" } }
               """);
    }


    static void Assert(string html, string expected)
    {
        
        HtmlToCSharp(html).Should().Be(expected);
    }
}