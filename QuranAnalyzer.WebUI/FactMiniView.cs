using ReactDotNet;

namespace QuranAnalyzer.WebUI;
class FactMiniViewModel
{
    public ClientTask ClientTask { get; set; }
    public FactModel Fact { get; set; }
}
class FactMiniView:ReactComponent<FactMiniViewModel>
{
    public FactMiniView()
    {
        state = new FactMiniViewModel();
    }


        

    public string title { get; set; }

    void OnClicked(string name)
    {
        state.ClientTask = new ClientTask
        {
            DispatchEvent           = true,
            DispatchEventName       = "OnFactClicked",
            DispatchEventParameters = new object[]{ name }
        };
    }

    public override Element render()
    {
        return new div
        {
            style =
            {
                borderRadius = "5px",
                width        = ReactDotNet.Mixin.px(150),
                height       = ReactDotNet.Mixin.px(200) ,
                //boxShadow  ="0 4px 8px 0 rgba(0,0,0,0.2)",
                margin         = ReactDotNet.Mixin.px(20),
                display        = Display.flex,
                justifyContent = JustifyContent.center,
                alignItems     = AlignItems.center,
                flexDirection  = FlexDirection.column,
                textAlign      = TextAlign.center,
                fontFamily     = "Verdana,sans-serif"
            },
            onClick = ()=>OnClicked(state.Fact.Name),
            children =
            {
                new div
                {

                    style = { padding = "1px",  },
                    children =
                    {
                        new div { style =   {color ="#08090a", fontSize = "17px", fontWeight = "600" }, text = title },
                        new div
                        {
                            style =
                            {
                                wordBreak = WordBreak.break_all,
                                margin    = "15px",
                                fontSize  = ReactDotNet.Mixin.px(13),
                                color     ="#546285"
                            },
                            text = "Kısa açıklamaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa1gtgtgrtrtğ", Margin ={ Top =5} }
                    }
                }

            }

        }.HasBorder();
    }
}