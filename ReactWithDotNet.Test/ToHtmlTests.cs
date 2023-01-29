using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactWithDotNet.Libraries.PrimeReact;
using ReactWithDotNet.Libraries.react_free_scrollbar;

namespace ReactWithDotNet.Test;

[TestClass]
public class ToHtmlTests
{


    
    [TestMethod]
    public void _0_()
    {
        var el = new div
        {
            new InputText()
        };

        var expectedHtml = @"
<div>
  <div style=""min-height:30px;min-width:150px;"" aria-component=""ReactWithDotNet.Libraries.PrimeReact.InputText""></div>
</div>
";

        el.ToString().Should().BeEquivalentTo(expectedHtml.Trim());

    }
    
    
    [TestMethod]
    public void _1_()
    {
        var el = new div
        {
            new div
            {
                "Abc0"
            },
            new div
            {
                "Abc1",
                new a{ href = "w"},
                "Abc2",
                (span)"uuO",
                new div
                {
                    Aria("A","B"), Aria("c","D"),
                    new InputText()
                },
                new div{dangerouslySetInnerHTML = "De1"}
            }
        };

        var expectedHtml = @"
<div>
  <div>Abc0</div>
  <div>Abc1
    <a href=""w""></a>Abc2
    <span>uuO</span>
    <div aria-A=""B"" aria-c=""D"">
      <div style=""min-height:30px;min-width:150px;"" aria-component=""ReactWithDotNet.Libraries.PrimeReact.InputText""></div>
    </div>
    <div>De1</div>
  </div>
</div>

";
        
        el.ToString().Should().BeEquivalentTo(expectedHtml.Trim());

    }
}