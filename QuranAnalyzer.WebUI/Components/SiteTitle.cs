//namespace QuranAnalyzer.WebUI.Components;

//class SiteTitle : ReactComponent
//{
//    protected override Element render()
//    {
//        return new div(Text("19 Sistemi Nedir"), FontSize20);
//    }
//}


using ReactWithDotNet.Libraries.mui.material;

namespace QuranAnalyzer.WebUI.Components;

class SiteTitle : ReactComponent
{
    protected override Element render()
    {
        return new div
        {
            new Tooltip
            {
                arrow   = true,
                title   = new div("aloha"),
                classes =
                {
                    { "popper", new Style{ BackgroundColor("red"), Color("blue")}},
                    { "arrow", new Style{ BackgroundColor("yellow")}},
                    { "tooltip", new Style{ BackgroundColor("green"), Color("blue")}}
                },
                children =
                {
                    new div(Text("19 Sistemi Nedir"), FontSize20)
                }
            }
        };
    }
}