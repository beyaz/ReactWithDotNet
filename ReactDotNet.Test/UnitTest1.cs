using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ReactDotNet.Mixin;

namespace ReactDotNet;

[TestClass]
public class UnitTest1
{

    [TestMethod]
    public void ToJsonBasicHtmlElements()
    {
        void onClick(string id){}
        
        var map = new a { className = "X", href = "abc", style = { marginRight = "5px", paddingTop = "6px"}, onClick = onClick}.ToMap();
        var json = JsonSerializer.Serialize(map,new JsonSerializerOptions{ IgnoreNullValues = true});

        json.ShouldBeSameAs(@"
        {
          ""$type"": ""a"",
          ""href"": ""abc"",
          ""style"":{""marginRight"": ""5px"", ""paddingTop"": ""6px""},
          ""onClick"": {""$isRemoteMethod"": true, ""remoteMethodName"": ""\u003CToJsonBasicHtmlElements\u003Eg__onClick|0_0""},
          ""className"": ""X"",
          ""reactAttributes"": [""href"",""style"", ""onClick"", ""className""]
        }

        ");


    }

    class View1 : ReactComponent<SampleModelA>
    {
        public string Prop1 { get; set; } = "PropValue1";
        public string Prop2 { get; set; } = "PropValue2";

       

        public override Element render()
        {
            return new div
            {
                className = "A",
                innerText = "b",
                
            };
        }
    }

    class View2 : ReactComponent<SampleModelA>
    {
        public string Prop1 { get; set; } = "PropValue1";
        public string Prop2 { get; set; } = "PropValue2";

        public View2()
        {
            
        }

        public override Element render()
        {
            return new div
            {
                className = "A",
                children  = { new View1 { Prop1 = "A" } }
            };
        }
    }

    static string ReadTestFile(string name)
    {
        return File.ReadAllText(name);
    }

    [TestMethod]
    public void SerializeBasicDiv()
    {
        var div = new div{ className = "B" };

        var actual = ToJson(div);

        var expected = @"
{
  ""$type"": ""div"",
  ""className"": ""B"",
  ""reactAttributes"": [
    ""className""
  ]
}
";
        actual.Clear().Should().Be(expected.Clear());
    }

    [TestMethod]
    public void SerializeClientTask()
    {
        var a = new SerializeClientTaskModel
        {
            ClientTask = new ClientTaskCallJsFunction
            {
                JsFunctionArguments = new [] {(object)2},
                JsFunctionPath      = "A.B"
            }
        };

        var json = ToJson(a);
    }

    class SerializeClientTaskModel
    {
        public ClientTask ClientTask { get; set; }
    }


   



    [TestMethod]
    public void SerializeComponentWithProperties()
    {
            

        var state = new SampleModelAContainer();

        var xxx = ToJson(new div{ new PrimeReact.InputText { valueBind = () => state.InnerA.InnerB.Text } });

        var xxx2 = ToJson(new div { new PrimeReact.InputTextarea { valueBind = () => state.InnerA.InnerB.Text } });

        var xxx3 = ToJson(new div { new PrimeReact.InputTextarea { value = null } });


        var div = new div
        {
            className = "abc",
           children =
           {
               new div{className = "B"},
               new div
               {
                   className = "C",
                   style     = { paddingLeft = "5px" }
               },
               new img { src                    = "a.png", width = 3, onClick = onClicked },
               new PrimeReact.InputText { value = "abc" },
               new PrimeReact.InputText { valueBind = ()=> state.InnerA.InnerB.Text },
               new View2 { Prop1                = "x", Prop2 = "y" }
           }
        };

        var actual = ToJson(div);

        var expected = ReadTestFile("SerializeComponentWithProperties.txt");
        actual.Clear().Should().Be(expected.Clear());
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
        var json  = "{\r\n\"ClickCount\": 2}";
        var state = (SampleModel)JsonSerializer.Deserialize(json, typeof(SampleModel), new JsonSerializerOptions().ModifyForReactDotNet());

        state.ClickCount.Should().Be(2);
    }

    void onClicked(string id)
    {

    }



}