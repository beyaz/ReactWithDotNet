using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class ToHtmlTests
{
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
                (span)"uuO"
            }
        };

        var expectedHtml = @"
<div>
  <div>Abc0</div>
  <div>Abc1
    <a href=""w""></a>Abc2
    <span>uuO</span>
  </div>
</div>

";
        
        el.ToString().Should().BeEquivalentTo(expectedHtml.Trim());

    }
}