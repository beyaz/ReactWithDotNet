using System.IO;
using Newtonsoft.Json.Linq;

namespace ReactWithDotNet.UIDesigner;

class ComponentPreivew: ReactComponent<UIDesignerModel>
{
   

    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();
    }

    public void Refresh()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();
        
        ClientTask.GotoMethod(700, Refresh);
    }

    protected override void componentDidMount()
    {
        ClientTask.GotoMethod(700, Refresh);
    }

    protected override Element render()
    {
        return createElement();
    }

    Element createElement()
    {
        try
        {

            // try invoke as static function
            {
                var fullAssemblyPath = state.SelectedAssemblyFilePath;
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

                                var jsObject = (JObject)Json.DeserializeJsonByNewtonsoft(state.SelectedDotNetMemberSpecification. JsonTextForDotNetMethodParameters.HasValue() ? state.SelectedDotNetMemberSpecification.JsonTextForDotNetMethodParameters : "{}", typeof(JObject));
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

                                if (methodInfo.IsStatic)
                                {
                                    return (Element)methodInfo.Invoke(null, invocationParameters.ToArray());
                                }

                                // invoke as instance
                                {
                                    var declaringType = methodInfo.DeclaringType;
                                    if (declaringType is null)
                                    {
                                        return new div { text = "Method declaring type is null." };
                                    }

                                    var instance = (Element)Json.DeserializeJsonByNewtonsoft(state.SelectedDotNetMemberSpecification.JsonTextForDotNetInstanceProperties.HasValue() ? state.SelectedDotNetMemberSpecification?.JsonTextForDotNetInstanceProperties : "{}", declaringType);

                                    if (instance is ReactStatefulComponent component)
                                    {
                                        component.key     = "0";
                                        component.Context = Context;
                                        component.InvokeConstructor();
                                    }

                                    return (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());
                                }

                            }
                        }
                    }
                }


            }

            {
                var type = FindType(state.SelectedComponentTypeReference);
                if (type == null)
                {
                    return new div("type not found.@" + state.SelectedComponentTypeReference);
                }

                var instance = (Element)Json.DeserializeJsonByNewtonsoft(state.SelectedDotNetMemberSpecification.JsonTextForDotNetInstanceProperties.HasValue() ? state.SelectedDotNetMemberSpecification.JsonTextForDotNetInstanceProperties : "{}", type);



                if (instance is ReactStatefulComponent component)
                {
                    component.key     = "0";
                    component.Context = Context;
                    component.InvokeConstructor();

                    return component;
                }

                return new div(instance.ToString());
            }

            
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