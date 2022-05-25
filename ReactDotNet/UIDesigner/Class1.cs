using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace ReactDotNet.UIDesigner
{
    [Serializable]
    public class UIDesignerModel
    {
        public string SelectedComponentTypeReference { get; set; }

        public ClientTask ClientTask { get; set; }

        public IReadOnlyList<DotNetObjectPropertyValue> Properties { get; set; }
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
                if (type.IsSubclassOf(typeof(ReactComponent)))
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
                Timeout    = 200000
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
                onChange    = OnChange,
                filter = true
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

                    return null;
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

        void OnDataChange(string dataAsJson)
        {
            // state.ComponentData = dataAsJson;
        }

       

        void OnChange(ListBoxChangeParams e)
        {
            // state.ComponentData = GetDefaultDataAsJson(state.SelectedComponentName);


            state.SelectedComponentTypeReference = e.value;

            
        }

        IEnumerable<Element> GetPropertyEditors()
        {
            if (string.IsNullOrWhiteSpace(state.SelectedComponentTypeReference) == false)
            {
                var type = Type.GetType(state.SelectedComponentTypeReference, false);
                if (type != null)
                {
                    state.Properties = GetProperties(type).ToList();

                    for (int i = 0; i < state.Properties.Count; i++)
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
    
}
