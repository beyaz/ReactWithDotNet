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
        public IReadOnlyList<AAA> Suggestions { get; set; } = new List<AAA>
        {
            new() {Name = "Abx", Value = "xyz"}, 
            new() {Name = "Abx", Value = "xyzyy"},
        };
        public string SelectedComponentName { get; set; }

    }

    public class AAA
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
                optionLabel = "Name",
                optionValue = "Value",
                value       = state.SelectedComponentName,
                onChange    = OnChange,
                style =
                {
                    height = px(500),
                    width = px(300)
                },
                filter = true
            };
            return new div(width(px(500)) , height(px(500)), border("1px dashed #e0e0e0"), padding(5))
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
