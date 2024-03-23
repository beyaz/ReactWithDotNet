using System.Reflection;

namespace ReactWithDotNet;

static partial class ElementSerializer
{
    static Exception HandlerMethodShouldBelongToReactComponent(string fullNameOfProperty, object handlerTarget)
    {
        var errorMessage = new List<string>
        {
            "Delegate method should belong to ReactComponent. ",
            "Please give named method to " + fullNameOfProperty
        };

        if (handlerTarget is null)
        {
            throw DeveloperException(string.Join(Environment.NewLine, errorMessage));
        }

        errorMessage.Add("How to fix: Inherit handler class from ReactComponent");

        var handlerDetail = handlerTarget.GetType().FullName;

        var target = handlerTarget.GetType();

        // more detail for compiler generated types because there is no meaningfull class name
        if (target.IsCompilerGenerated())
        {
            handlerDetail += string.Join(Environment.NewLine, target.GetFields().Select(x => $"{x.FieldType} {x.Name}"));
        }

        errorMessage.Add(handlerDetail);

        throw DeveloperException(string.Join(Environment.NewLine, errorMessage));
    }

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo, string bindingPath)
    {
        throw new InvalidOperationException("Delegate method should belong to ReactComponent. Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name + $" Given bindingPath:{bindingPath} is invalid.");
    }
}