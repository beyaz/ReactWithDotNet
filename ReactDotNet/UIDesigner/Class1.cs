using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace ReactDotNet.UIDesigner
{
    [Serializable]
    public class UIDesignerModel
    {
        public IReadOnlyList<ReactComponentInfo> Suggestions { get; set; } = new List<ReactComponentInfo>
        {
            new (){Name ="abc1", Value = "ytr"},
            new() {Name ="abc2", Value = "ytr2"}
        };
        public string SelectedComponentName { get; set; }
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
            state = new UIDesignerModel();
        }
        
        public override Element render()
        {
            var componentSelector = new ListBox
            {
                options     = state.Suggestions,
                optionLabel = nameof(ReactComponentInfo.Name),
                optionValue = nameof(ReactComponentInfo.Value),
                value       = state.SelectedComponentName,
                onChange    = OnChange,
                style =
                {
                    height = px(500),
                    width = px(300)
                },
                filter = true
            };
            return new HPanel(border("1px dashed #e0e0e0"), padding(5), margin(7))
            {
                componentSelector,
                new div("data"),
                new div("output" + state.SelectedComponentName)
            };
        }

        void OnChange(ListBoxChangeParams e)
        {
            state.SelectedComponentName = e.value;
        }
    }
}
