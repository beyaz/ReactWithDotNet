using System;
using System.Collections.Generic;
using System.Text;
using static ReactDotNet.Mixin;

namespace ReactDotNet.UIDesigner
{
    [Serializable]
    public class UIDesignerModel
    {
    }

    public class UIDesignerView:ReactComponent<UIDesignerModel>
    {
        public override void constructor()
        {
            state = new UIDesignerModel();
        }

        public override Element render()
        {
            
            return new div(width(px(500)) , height(px(500)), border("1px dashed #e0e0e0"), padding(5))
            {
                new div("select component"),
                new div("data"),
                new div("output")
            };
        }
    }
}
