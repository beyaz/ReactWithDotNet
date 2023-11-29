using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class ReflectionHelperTests
{

    class MyClass
    {
        public string Prop1 { get; set; }

        public object this[string key]
        {
            get
            {
                if (key == nameof(Prop1))
                {
                    return Prop1;
                }

                return null;
            }
        }

        
        
    }
    
    [TestMethod]
    public void __1__1()
    {
        var propertyInfo = typeof(MyClass).GetProperty("Prop1");

        var getFunc = ReflectionHelper.CreateGetFunction(propertyInfo);

        var instance = new MyClass
        {
            Prop1 = "abc"
        };

        getFunc(instance).Should().Be("abc");
    }
    
    
}