using System.Text.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class SerializationTests
{
    class A
    {
        public string Prop0 { get; set; }
        public A Child { get; set; }
    }
    
    [TestMethod]
    public void NestedSerialize()
    {

        var a = new A
        {
            Prop0 = "p0",
            Child = new A
            {
                Prop0 = "p1",
                Child = new A
                {
                    Prop0 = "p2",
                    Child = new A
                    {
                        Prop0 = "p3",
                        Child = new A
                        {
                            Prop0 = "p4",
                            Child = new A
                            {
                                Prop0 = "p5",
                                Child = new A
                                {
                                    Prop0 = "p6",
                                    Child = new A
                                    {
                                        Prop0 = "p7"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        var a2 = DeserializeJsonBySystemTextJson<A>(json);

        a2.Child.Child.Child.Child.Child.Child.Child.Prop0.Should().Be("p7");
    }
}