using FluentAssertions;
using static ReactWithDotNet.WebSite.HelperApps.HtmlToReactWithDotNetCsharpCodeConverter;

namespace ReactWithDotNet.WebSite.Test;

[TestClass]
public class HtmlToCSharpTests
{
    [TestMethod]
    public void _0()
    {
        Assert("""
               <span class='a-b c' style = "color:rgb(28, 32, 37);border-radius:12px;">xYz1</span>
               """,
               """
               new span("a-b c", Color(rgb(28, 32, 37)), BorderRadius(12))
               {
                   "xYz1"
               }
               """);


        Assert("""
               <span class='a-b c'>xYz1</span>
               """,
               """
               new span("a-b c")
               {
                   "xYz1"
               }
               """);

        
        Assert("""
               <span>xYz1</span>
               """,
               """
               new span
               {
                   "xYz1"
               }
               """);

        


        Assert("""
               <a target='_blank' />
               """,
               """
               new a(TargetBlank)
               """);

        
        Assert("""
               <a target='_blank' style = "color:rgb(28, 32, 37);border-radius:12px;"/>
               """,
               """
               new a(TargetBlank, Color(rgb(28, 32, 37)), BorderRadius(12))
               """);

        
        Assert("""
               <a target='_blank'  aria-hidden="true"   data-testid="AcUnitIcon"  style = "color:rgb(28, 32, 37);border-radius:12px;"/>
               """,
               """
               new a(TargetBlank, Aria("hidden", "true"), Data("testid", "AcUnitIcon"), Color(rgb(28, 32, 37)), BorderRadius(12))
               """);

        
        Assert("""
               <a target='_blank'  aria-hidden="true"   data-testid="AcUnitIcon"  style = "color:rgb(28, 32, 37);border-radius:12px;">
               xyz
               </a>
               """,
               """
               new a(TargetBlank, Aria("hidden", "true"), Data("testid", "AcUnitIcon"), Color(rgb(28, 32, 37)), BorderRadius(12))
               {
                   "xyz"
               }
               """);

        
        Assert("""
               <div aria-hidden="true"   data-testid="AcUnitIcon"  style = "display: flex; flexDirection: row; color: blue;">

               </div>
               """,
               """
               new FlexRow(Aria("hidden", "true"), Data("testid", "AcUnitIcon"), Color("blue"))
               """);
        return;
        
        static void Assert(string html, string expected, bool smartMode=true)
        {
            var actual = HtmlToCSharp(html, smartMode, 3);
            
            actual.Should().Be(expected);
        }
    }

    [TestMethod]
    public void _1()
    {
        Assert("""
               <path style="fill:#010002;" d="abc"/>
               """,
               """
               new path(Fill("#010002"))
               {
                   d = "abc"
               }
               """
               );


        Assert("""
               <div aria-hidden="true"   data-testid="AcUnitIcon"  style = "display: flex; flexDirection: row; color: blue;">

               </div>
               """,
               """
               new FlexRow(Aria("hidden", "true"), Data("testid", "AcUnitIcon"), Color("blue"))
               """);



        Assert("""
               <div style="width: 96.7px; padding-top: 1px; padding-bottom: 3px; padding-left: 4px; padding-right: 2px;">

               </div>
               """,
               """
               new div(Width(96.7), Padding(1, 2, 3, 4))
               """);

        Assert("""
               <div style="width: 246.7px; flex-direction: column; justify-content: flex-start; align-items: flex-start; display: inline-flex">
               aB c
               </div>
               """,
               """
               new InlineFlexColumn(Width(246.7), JustifyContentFlexStart, AlignItemsFlexStart)
               {
                   "aB c"
               }
               """);
        
        
        Assert("""
               <div style="width: 246.7px; flex-direction: column; justify-content: flex-start; align-items: flex-start; display: inline-flex">
                   aB c
                   <div style="width: 96.7px; padding-top: 1px; padding-bottom: 3px; padding-left: 4px; padding-right: 2px;">
                       xY z
                   </div>
               </div>
               """,
               """
               new InlineFlexColumn(Width(246.7), JustifyContentFlexStart, AlignItemsFlexStart)
               {
                   @"
                   aB c
                   ",
                   new div(Width(96.7), Padding(1, 2, 3, 4))
                   {
                       "xY z"
                   }
               }
               """);
        
        
        return;

        static void Assert(string html, string expected)
        {
            var current = HtmlToCSharp(html, true, 3);

            current.Should().Be(expected);
        }
    }
}