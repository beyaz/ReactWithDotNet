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
        UnionProp<string,int,bool> a = "xyZ";

        var json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("\"xyZ\"");
        
        a = 65;

        json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("65");
        
        a = null;

        // ReSharper disable once ExpressionIsAlwaysNull
        json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("null");
        
        a = false;

        json = JsonSerializer.Serialize(a, JsonSerializerOptionsInstance);

        json.Should().Be("false");
    }


    enum MyEnum
    {
        A=5,B=6, C4TYU3 =7
    }

    class MyClass
    {
        public MyEnum JJJ { get; set; }
        public string Prop1{ get; set; }
    }
    
    [TestMethod]
    public void EnumSerialize()
    {
        var instance = new MyClass
        {
            JJJ   = MyEnum.C4TYU3,
            Prop1 = "a"
        };

        
        var json = JsonSerializer.Serialize(instance, JsonSerializerOptionsInstance);

        json.Should().Be(
                         """
                         {
                           "JJJ": "C4TYU3",
                           "Prop1": "a"
                         }
                         """);

        json = """
               {
                 "JJJ": "C4TYU3",
                 "Prop1": "a"
               }
               """;


        instance = JsonSerializer.Deserialize<MyClass>(json,JsonSerializerOptionsInstance);
        
        instance.Prop1.Should().Be("a");
        instance.JJJ.Should().Be(MyEnum.C4TYU3);


    }
}