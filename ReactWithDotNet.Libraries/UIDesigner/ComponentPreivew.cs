using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ReactWithDotNet.UIDesigner;

class ComponentPreivew: ReactComponent<UIDesignerModel>
{
    public ComponentPreivew()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();
    }

    public void Refresh()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();
        Context.ClientTask.GotoMethod(1*1000, nameof(Refresh));
    }

    public void ComponentDidMount()
    {
        Context.ClientTask.GotoMethod(1 * 1000, nameof(Refresh));
    }
    
    public override Element render()
    {
        return createElement();
    }

    Element createElement()
    {
        try
        {

            // try invoke as static function
            {
                var fullAssemblyPath = Path.Combine(state.SelectedFolder ?? string.Empty, state.SelectedAssembly ?? string.Empty);
                if (File.Exists(fullAssemblyPath))
                {
                    var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey);
                    if (node is not null)
                    {
                        if (node.IsMethod)
                        {
                            var methodInfo = MetadataHelper.FindMethodInfo(MetadataHelper.LoadAssembly(fullAssemblyPath), node);
                            if (methodInfo != null)
                            {
                                var invocationParameters = new List<object>();

                                var methodParameters = methodInfo.GetParameters();

                                var jsObject = (JObject)Json.DeserializeJsonByNewtonsoft(state.JsonText.HasValue() ? state.JsonText : "{}", typeof(JObject));
                                foreach (var parameterInfo in methodParameters)
                                {
                                    var parameterName = parameterInfo.Name;
                                    var parameterType = parameterInfo.ParameterType;

                                    if (parameterName is not null)
                                    {
                                        var parameterValueAsJsonObject = jsObject[parameterName];
                                        if (parameterValueAsJsonObject is not null)
                                        {
                                            invocationParameters.Add(parameterValueAsJsonObject.ToObject(parameterType));
                                            continue;
                                        }

                                        return new div { text = $"Missing parameter {parameterName}" };
                                    }

                                    return new div { text = "parameterName not be evaluated" };
                                }

                                return (Element)methodInfo.Invoke(null, invocationParameters.ToArray());
                            }
                        }
                    }
                }


            }


            var type = FindType(state.SelectedComponentTypeReference);
            if (type == null)
            {
                return new div("type not found.@" + state.SelectedComponentTypeReference);
            }

            var instance = (Element)Json.DeserializeJsonByNewtonsoft(state.JsonText.HasValue() ? state.JsonText : "{}", type);

            if (instance is ReactComponent reactComponent)
            {
                reactComponent.Context = new ReactContext();
                return reactComponent.render();
            }

            if (instance is ReactStatefulComponent reactStatefulComponent)
            {
                reactStatefulComponent.Context = new ReactContext();

                return reactStatefulComponent.render();
            }

            return new div(instance.ToString());
        }
        catch (Exception exception)
        {
            return new div(exception.ToString());
        }
    }

    static Type FindType(string typeReference)
    {
        if (!string.IsNullOrWhiteSpace(typeReference))
        {
            return Type.GetType(typeReference, false);
        }

        return null;
    }

    
}