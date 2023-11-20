using System.Text.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class SerializationTests
{
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

    [TestMethod]
    public void NestedSerialize2()
    {
        var a = new A
        {
            Prop0 = "p0",
            Children = new List<A>
            {
                new()
                {
                    Prop0 = "p1"
                },
                new()
                {
                    Prop0 = "p2",
                    Children = new List<A>
                    {
                        new()
                        {
                            Prop0 = "p3"
                        },
                        new()
                        {
                            Prop0 = "p4",
                            Children = new List<A>
                            {
                                new()
                                {
                                    Prop0 = "p5"
                                },
                                new()
                                {
                                    Prop0 = "p6"
                                }
                            }
                        }
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        var a2 = DeserializeJsonBySystemTextJson<A>(json);

        a2.Children[1].Children[1].Children[1].Prop0.Should().Be("p6");
    }

    class A
    {
        public A Child { get; set; }

        public List<A> Children { get; set; }
        public string Prop0 { get; set; }
    }
    
    
    [TestMethod]
    public void TupleSerialize()
    {
        UnionStringOrInt32 a = "xyZ";

        var json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("\"xyZ\"");
        
        var a2 = DeserializeJsonBySystemTextJson<UnionStringOrInt32>(json);
        
        (a2 == "xyZ").Should().BeTrue();
        
        
        a = 65;

        json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("65");
        
        a2 = DeserializeJsonBySystemTextJson<UnionStringOrInt32>(json);
        
        (a2 == 65).Should().BeTrue();
        
        
        a = null;

        json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("null");
        
        a2 = DeserializeJsonBySystemTextJson<UnionStringOrInt32>(json);
        
        (a2 == null).Should().BeTrue();
    }
}