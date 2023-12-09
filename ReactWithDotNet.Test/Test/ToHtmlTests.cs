using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

namespace ReactWithDotNet.Test;

[TestClass]
public class ToHtmlTests
{
    [TestMethod]
    public void __1__1()
    {
        const string expectedHtml = """

<div>ABC</div>

""";

        Assert(new div { "AbC" }, expectedHtml);
    }

    [TestMethod]
    public void __1__2()
    {
        var el = new div { "AbC", (span)"xY" };

        const string expectedHtml = """

<div>ABC<span>xY</span></div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __1__3()
    {
        Assert(new div { dangerouslySetInnerHTML = "jU" },
               """
<div>jU</div>

""");
    }

    [TestMethod]
    public void __1__4()
    {
        var el = new a
        {
            style = { width = "4px" }, href = "j"
        };

        const string expectedHtml = """

<a style="width:4px;" href="j"></a>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __10()
    {
        var el = new div
        {
            "Aa", (strong)"Bb", " -", (small)"Cc"
        };

        const string expectedHtml = """
<div>Aa<strong>Bb</strong> -<small>Cc</small></div>

""";
        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __2__()
    {
        var el = new div
        {
            style = { width = "4px" }
        };

        const string expectedHtml = """

<div style="width:4px;"></div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __3()
    {
        var el = new div
        {
            "Aa", nbsp(), nbsp, nbsp(2), "bC"
        };

        const string expectedHtml = """

<div>Aa&nbsp;&nbsp;&nbsp;&nbsp;bC</div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __4()
    {
        var el = new div
        {
            style = { width = "4px" },
            children =
            {
                new div
                {
                    style = { width = "5px" },
                    children =
                    {
                        new div
                        {
                            style = { width = "6px" },
                            children =
                            {
                                new div
                                {
                                    style = { width = "7px" }
                                }
                            }
                        }
                    }
                }
            }
        };

        const string expectedHtml = """

<div style="width:4px;">
  <div style="width:5px;">
    <div style="width:6px;">
      <div style="width:7px;"></div>
    </div>
  </div>
</div>

""";

        el.ToHtml().Trim().Should().Be(expectedHtml.Trim());
    }

    [TestMethod]
    public void __5()
    {
        Element cmp = new SamplePureComponent2();

        var el = new div(Width(21))
        {
            cmp
        };

        var expectedHtml = """

<div style="width:21px;">
  <div style="width:32px;height:43px;"></div>
  <div style="width:33px;height:44px;"></div>
  <div style="width:34px;height:45px;"></div>
</div>

""";

        el.ToHtml().Trim().Should().BeEquivalentTo(expectedHtml.Trim());
    }

    [TestMethod]
    public void __6()
    {
        var el = new div
        {
            new InputText()
        };

        var expectedHtml = """
<div>
  <div style="min-height:30px;min-width:150px;"></div>
</div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __7()
    {
        Element cmp = new SamplePureComponent();

        var el = new div(Width(21))
        {
            new div(Width(22))
            {
                cmp + Width(43)
            }
        };

        const string expectedHtml = """
<div style="width:21px;">
  <div style="width:22px;">
    <div style="width:43px;height:33px;"></div>
  </div>
</div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __7__1()
    {
        Element el = new SamplePureComponent();

        const string expectedHtml = """

<div style="width:30px;height:33px;"></div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __8()
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
                new a { href = "w" },
                "Abc2",
                (span)"uuO",
                new div
                {
                    Aria("A", "B"), Aria("c", "D"),
                    new InputText()
                },
                new div { dangerouslySetInnerHTML = "De1" }
            }
        };

        const string expectedHtml = """
<div>
  <div>Abc0</div>
  <div>Abc1<a href="w"></a>Abc2<span>uuO</span>
    <div aria-A="B" aria-c="D">
      <div style="min-height:30px;min-width:150px;"></div>
    </div>
    <div>De1</div>
  </div>
</div>

""";

        Assert(el, expectedHtml);
    }

    [TestMethod]
    public void __9()
    {
        var el = new FlexColumn(Width(140), AlignItemsCenter)
        {
            new FlexRow(Gap(25))
            {
                new img { Src("A.svg"), WidthHeight(30) },
                new img { Src("B.svg"), WidthHeight(30) }
            },

            new small { "React with DotNet" }
        };

        const string expectedHtml = """

<div style="display:flex;flex-direction:column;width:140px;align-items:center;">
  <div style="display:flex;flex-direction:row;gap:25px;">
    <img style="width:30px;height:30px;" src="A.svg">
    <img style="width:30px;height:30px;" src="B.svg">
  </div>
  <small>React with DotNet</small>
</div>

""";

        Assert(el, expectedHtml);
    }

    static void Assert(Element element, string expectedHtml)
    {
        var actualHtml = element.ToHtml();

        actualHtml.Should().BeEquivalentTo(expectedHtml.Trim());
    }

    class SamplePureComponent : Component
    {
        protected override Element render()
        {
            return new div { Width(30), Height(33) };
        }
    }

    class SamplePureComponent2 : Component
    {
        protected override Element render()
        {
            return new Fragment
            {
                new div { Width(32), Height(43) },
                new div { Width(33), Height(44) },
                new div { Width(34), Height(45) }
            };
        }
    }
}