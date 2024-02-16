using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ReactWithDotNet;

static partial class ElementSerializer
{
    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo, object handlerTarget)
    {
        throw DeveloperException(string.Join(Environment.NewLine,
                                             "Delegate method should belong to ReactComponent. ",
                                             "Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name,
                                             $"How to fix: inherit {handlerTarget?.GetType().FullName} class from ReactComponent."));
    }
    
    static Exception HandlerMethodShouldBelongToReactComponent(string fullNameOfProperty, object handlerTarget)
    {
        throw DeveloperException(string.Join(Environment.NewLine,
                                             "Delegate method should belong to ReactComponent. ",
                                             "Please give named method to " + fullNameOfProperty,
                                             $"How to fix: inherit {handlerTarget?.GetType().FullName} class from ReactComponent."));
    }

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo, string bindingPath)
    {
        throw new InvalidOperationException("Delegate method should belong to ReactComponent. Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name + $" Given bindingPath:{bindingPath} is invalid.");
    }
}