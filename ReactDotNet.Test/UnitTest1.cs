using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactDotNet
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ReactElement model = new div("C4")
            {
                text = "Aloha",
                style =
                {
                    AlignContent = AlignContent.FlexStart,
                    Width        = "5px",
                    Display      = Display.Flex,
                    
                }
            };

            var result = JsonSerializer.Serialize(model,JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

            result.Should().Be(@"
{
  ""tag"": ""div"",
  ""text"": ""Aloha"",
  ""props"": {
    ""className"": ""C4"",
    ""style"": {
      ""display"": ""flex"",
      ""width"": ""5px"",
      ""alignContent"": ""flex-start""
    }
  },
  ""children"": []
}
".Trim());
        }
    }
}
