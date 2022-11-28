using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test
{
    [TestClass]
    public class StyleTests
    {
        [TestMethod]
        public void _1_()
        {

            var style = new Style
            {
                minWidth = "4.56px",
                height   = "5.32rem"
            };

            style.ToCss().Should().BeEquivalentTo("height: 5.32rem;min-width: 4.56px;");

        }
    }
}