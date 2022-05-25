using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace ReactDotNet.UIDesigner;

[Serializable]
public class UIDesignerModel
{
    public string SelectedComponentTypeReference { get; set; }

    public ClientTask ClientTask { get; set; }

    public IReadOnlyList<DotNetObjectPropertyValue> Properties { get; set; }

    public string SaveDirectoryPath { get; set; } = @"d:\\temp\\";
}

public class ReactComponentInfo
{
    public string Name { get; set; }
    public string Value { get; set; }
}

[Serializable]
public sealed class DotNetObjectPropertyValue
{
    public string Path { get; set; }
    public string Value { get; set; }
}

    

public class UIDesignerView:ReactComponent<UIDesignerModel>
{
    void Update(object obj, string navigation, object newval)
    {
        var firstSlash = navigation.IndexOf("/");
        if (firstSlash < 0)
        {
            obj.GetType().GetProperty(navigation).SetValue(obj, newval);
        }
        else
        {
            var header = navigation.Substring(0, firstSlash);
            var tail   = navigation.Substring(firstSlash + 1);
            var subObj = obj.GetType().GetProperty(header).GetValue(obj);
            Update(subObj, tail, newval);
        }
    }


    void SaveProperties(string typeReference, IEnumerable<DotNetObjectPropertyValue> items)
    {
        var filePath = GetCacheFilePath(typeReference);

        File.WriteAllText(filePath, JsonSerializer.Serialize(items, new JsonSerializerOptions {WriteIndented = true}));
    }

    string GetCacheFilePath(string typeReference) => state.SaveDirectoryPath + typeReference + ".json";

    IEnumerable<DotNetObjectPropertyValue> ReadProperties(string typeReference)
    {
        var filePath = GetCacheFilePath(typeReference);

        if (!File.Exists(filePath))
        {
            return Enumerable.Empty<DotNetObjectPropertyValue>();
        }
        var json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<IEnumerable<DotNetObjectPropertyValue>>(json);
    }

    static IEnumerable<DotNetObjectPropertyValue> GetProperties(Type type)
    {
        foreach (var propertyInfo in type.GetProperties())
        {
            if (isSerializable(propertyInfo))
            {
                yield return new DotNetObjectPropertyValue {Path = propertyInfo.Name};
            }
        }

        static bool isSerializable(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                return true;
            }

            return false;
        }
    }

        

    public override void constructor()
    {
        state = new UIDesignerModel
        {
            ClientTask = new ClientTaskListenComponentEvent
            {
                EventName     = ReactComponentEvents.componentDidMount.ToString(),
                RouteToMethod = nameof(OnFirstLoaded)
            },
                
        };
    }

    static IEnumerable<ReactComponentInfo> GetComponents(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(ReactComponent)) && !type.IsAbstract)
            {
                yield return new ReactComponentInfo {Name = type.GetFullName(), Value = type.GetFullName() };
            }
        }
    }

    IReadOnlyList<ReactComponentInfo> Suggestions => GetComponents(Assembly.Load("QuranAnalyzer.WebUI")).ToList();

    public void OnFirstLoaded()
    {
        Refresh();
    }

    public void Refresh()
    {
        state.ClientTask = new ClientTaskGotoMethod
        {
            MethodName = nameof(Refresh),
            Timeout    = 2000
        };
    }

    public override Element render()
    {
        var componentSelector = new ListBox
        {
            options     = Suggestions,
            optionLabel = nameof(ReactComponentInfo.Name),
            optionValue = nameof(ReactComponentInfo.Value),
            value       = state.SelectedComponentTypeReference,
            onChange    = OnSelectedComponentChanged,
            filter      = true
        };

        var dataPanel = new div(GetPropertyEditors())
        {
                
        };


        Element createElement()
        {
            try
            {
                var type = Type.GetType(state.SelectedComponentTypeReference);
                if (type == null)
                {
                    return new div("type not found.");
                }

                var instance = Activator.CreateInstance(type);

                foreach (var dotNetObjectPropertyValue in state.Properties.Where(x=>!string.IsNullOrWhiteSpace(x.Value)))
                {
                    var path      = dotNetObjectPropertyValue.Path;
                    var jsonValue = dotNetObjectPropertyValue.Value;

                    var propertyInfo = type.GetProperty(path);

                    object propertyValue = null;
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyValue = jsonValue;
                    }
                    else
                    {
                        propertyValue = JsonSerializer.Deserialize(jsonValue, propertyInfo.PropertyType);
                    }
                        

                    propertyInfo.SetValue(instance,propertyValue);
                }


                return instance as Element;
            }
            catch (Exception exception)
            {

                return new div(exception.ToString());
            }
        }

        var mainPanel = new Splitter
        {
            layout = SplitterLayoutType.vertical,
            style =
            {
                width  = "100%",
                height = "100%"
            },

            children =
            {
                new SplitterPanel
                {
                    size = 70,
                    children =
                    {
                        new div{ createElement() }
                        | border("1px dashed #e0e0e0")
                        | width("100%")
                        | height("100%")
                    }
                },

                new SplitterPanel
                {
                    size = 30,
                    children =
                    {
                        new Splitter
                        {
                            new SplitterPanel
                            {
                                size     = 2,
                                children = {componentSelector | height("100%")}
                            },

                            new SplitterPanel
                            {
                                size     = 6,
                                children = {dataPanel | width("100%")}
                            }
                        }
                    }
                }
            }
        };

        return new div { mainPanel } | width("100%")| height("100%")| padding(10);
    }
        
    void OnSelectedComponentChanged(ListBoxChangeParams e)
    {
        if (!string.IsNullOrWhiteSpace(state.SelectedComponentTypeReference) && state.Properties?.Count > 0)
        {
            SaveProperties(state.SelectedComponentTypeReference, state.Properties);
        }

        state.SelectedComponentTypeReference = e.value;

            

        state.Properties = null;

        var type = FindType(state.SelectedComponentTypeReference);
        if (type != null)
        {
            state.Properties = GetProperties(type).ToList();

            foreach (var item in ReadProperties(state.SelectedComponentTypeReference))
            {
                var entry = state.Properties.FirstOrDefault(x=>x.Path == item.Path);
                if (entry != null)
                {
                    entry.Value = item.Value;
                }
                    
            }
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




    IEnumerable<Element> GetPropertyEditors()
    {
        if (string.IsNullOrWhiteSpace(state.SelectedComponentTypeReference) == false)
        {
            var type = Type.GetType(state.SelectedComponentTypeReference, false);
            if (type != null)
            {
                for (var i = 0; i < state.Properties.Count; i++)
                {
                    yield return new div
                    {
                        new div(state.Properties[i].Path),
                        new InputText {value = new BindibleProperty<string>{ PathInState = $"Properties[{i}].Value"}},
                    };
                }
            }
        }

        yield return null;
    }
}