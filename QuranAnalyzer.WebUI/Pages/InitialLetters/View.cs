using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet.Html5;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

public class View : PageBase
{
    public override string id { get; set; } = PageId;

    public static string PageId => nameof(InitialLetters);

    public override Element render()
    {
        var facts = ResourceAccess.Facts;

        return new div(facts.Select(ToElement))
               + background("white")
               + Display.flex
               + FlexWrap.wrap
               + JustifyContent.center;

        Element ToElement(FactModel factModel)
        {
            return new FactMiniView
            {
                state = new FactMiniViewModel { Fact = factModel }
            };
        }
    }

   
}