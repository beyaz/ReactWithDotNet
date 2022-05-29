using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Components;

class FactMiniViewModel
{
    public ClientTask ClientTask { get; set; }
    public FactModel Fact { get; set; }
}

class FactMiniView : ReactComponent<FactMiniViewModel>
{
    public FactMiniView()
    {
        state = new FactMiniViewModel();
    }

    void OnClicked(string name)
    {
        state.ClientTask = new ClientTaskDispatchEvent
        {
            EventName       = "OnFactClicked",
            EventArguments = new object[] {name}
        };
    }

    public override void constructor()
    {
        state = new FactMiniViewModel();
    }

    public override Element render()
    {
        var model = state.Fact;
        
        var title = new div
        {
            text = model.Name,
            style =
            {
                color      = "#08090a",
                fontSize   = vw(3.1),
                fontWeight = "600"
            }
        };

        var description = new div
        {
            text = model.ShortDescription,
            style =
            {
                wordBreak = WordBreak.break_all,
                margin    = "4px",
                color     = "#546285"
            }
        };

        return new div
               {
                   onClick = (_) => OnClicked(state.Fact.Name),
                   children =
                   {
                       new div(padding(5))
                       {
                           title,
                           description
                       }
                   }
               }
               + new Style
               {
                   border         = "1px solid #ced4da",
                   borderRadius   = "5px",
                   //width          = vw(30),
                   //height         = vw(30),
                   boxShadow      = "0 4px 8px 0 rgba(0,0,0,0.2)",
                   margin         = vw(4),
                   display        = Display.flex,
                   justifyContent = JustifyContent.center,
                   alignItems     = AlignItems.center,
                   flexDirection  = FlexDirection.column,
                   textAlign      = TextAlign.center,
                   fontFamily     = "Verdana,sans-serif",
                   cursor         = Cursor.pointer
               };
    }
}