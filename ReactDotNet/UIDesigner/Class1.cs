using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace ReactDotNet.UIDesigner
{
    [Serializable]
    public class UIDesignerModel
    {
        public string SelectedComponentName { get; set; }

        public ClientTask ClientTask { get; set; }
    }

    public class ReactComponentInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class UIDesignerView:ReactComponent<UIDesignerModel>
    {
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
                    yield return new ReactComponentInfo {Name = type.FullName, Value = type.FullName};
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
                value       = state.SelectedComponentName,
                onChange    = OnChange,
                filter = true
            };

            var dataPanel = new InputTextarea
            {
                value = "abc-data" + state.SelectedComponentName
            };

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
                            new div("output" + state.SelectedComponentName)
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
                                    size     = 3,
                                    children = {componentSelector | height("100%")}
                                },

                                new SplitterPanel
                                {
                                    size     = 5,
                                    children = {dataPanel | width("100%")}
                                }
                            }
                        }
                    }
                }
            };

            return new div { mainPanel } | width("100%")| height("100%")| padding(10);
        }

        void OnChange(ListBoxChangeParams e)
        {
            state.SelectedComponentName = e.value;
        }
    }
}
