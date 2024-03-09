using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class DelegatePropertyTest
{
    class MyComponent : Component
    {

        public Func<string,Task> SampleDelegateProperty1 { get; set; }
        
        public Func<string,string , Task> SampleDelegateProperty2 { get; set; }
        
    }
    
    [TestMethod]
    public void CreationTest()
    {

        

        {
            MyComponent myComponent = new MyComponent
            {
                ComponentUniqueIdentifier = 5
            };
            
            var propertyValue = DelegatePropertyHelper.ReCalculatePropertyValue(myComponent, typeof(MyComponent).GetProperty(nameof(myComponent.SampleDelegateProperty1)));

            var delegateFunc = (Func<string, Task>)propertyValue;

            delegateFunc("abC");

            myComponent.Client.TaskList.Count.Should().Be(1);
        }

        
        {
            MyComponent myComponent = new MyComponent
            {
                ComponentUniqueIdentifier = 5
            };
            
            var propertyValue = DelegatePropertyHelper.ReCalculatePropertyValue(myComponent, typeof(MyComponent).GetProperty(nameof(myComponent.SampleDelegateProperty2)));

            var delegateFunc = (Func<string, string, Task>)propertyValue;

            delegateFunc("abC", "ab");

            myComponent.Client.TaskList.Count.Should().Be(1);
        }
    }
}