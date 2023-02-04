using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

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


    [TestMethod]
    public void _2_()
    {

        var element = new div
        {
         key = "A",
         children =
         {
             new div
             {
                 key = "1"
             },
             new div
             {
                 key = "0"
             },
             new div
             {

             },

             new div
             {

             },
             null,
             new div
             {

             },
             new div
             {
                 key = "2"
             }
         }
        };

        ElementSerializer.InitializeKeyIfNotExists(element);

        element.key.Should().Be("A");

        element._children[0].key.Should().Be("1");
        element._children[1].key.Should().Be("0");
        element._children[2].key.Should().Be("3");
        element._children[3].key.Should().Be("4");
        element._children[4].Should().BeNull();
        element._children[5].key.Should().Be("5");
        element._children[6].key.Should().Be("2");

    }
}