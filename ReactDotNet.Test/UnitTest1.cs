using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactDotNet
{
    [TestClass]
    public class UnitTest1
    {

        class View1 : ReactComponent<SampleModelA>
        {
            public string Prop1 { get; set; } = "PropValue1";
            public string Prop2 { get; set; } = "PropValue2";

            public ReactElement RootElement => render();

            public override ReactElement render()
            {
                return new div("A")
                {
                    text = "b"
                };
            }
        }

        class View2:ReactComponent<SampleModelA>
        {
            public string Prop1 { get; set; } = "PropValue1";
            public string Prop2 { get; set; } = "PropValue2";

            public ReactElement RootElement => render();

            public override ReactElement render()
            {
                return new div("A")
                {
                   new View1{ Prop1 = "A"}
                };
            }
        }

        [TestMethod]
        public void SerializeComponentWithProperties()
        {
            var view = new View2();

            var result = JsonSerializer.Serialize(view, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

            result.Should().Be(@"""innerA.innerB.text""".Trim());
        }


        [TestMethod]
        public void BindingPath()
        {
            var state = new SampleModelAContainer();

            Expression<Func<string>> valueBind = () => state.InnerA.InnerB.Text;

            var result = JsonSerializer.Serialize(valueBind, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

            result.Should().Be(@"""innerA.innerB.text""".Trim());
        }


        [Serializable]
        public class SampleModelAContainer
        {
            public string Text { get; set; }
            public SampleModelA InnerA { get; set; }
        }

        [Serializable]
        public class SampleModelA
        {
            public string Text { get; set; }
            public SampleModelB InnerB { get; set; }
        }

        [Serializable]
        public class SampleModelB
        {
            public string Text { get; set; }
        }

        [Serializable]
        public class SampleModel
        {
            public int ClickCount { get; set; }
            public int Id { get; set; }
            public string Text { get; set; }
            public int DropdownSelectedValue { get; set; }


        }


        [TestMethod]
        public void Deserialize()
        {
            var json  = "{\r\n\"clickCount\": 2}";
            var state = (SampleModel)JsonSerializer.Deserialize(json, typeof(SampleModel), JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

            state.ClickCount.Should().Be(2);
        }

        void onClicked()
        {

        }

        [TestMethod]
        public void ActionSerialization()
        {
            ReactElement model = new button
            {
                text = "Aloha",
                onClick = onClicked
            };

            var result = JsonSerializer.Serialize(model, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

            result.Should().Be(@"
{
  ""tag"": ""button"",
  ""text"": ""Aloha"",
  ""props"": {
    ""onClick"": ""$remote$onClicked"",
    ""style"": {}
  },
  ""children"": []
}
".Trim());
        }

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
