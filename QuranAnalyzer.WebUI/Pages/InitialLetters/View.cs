using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

public class View : PageBase
{
    public override string id { get; set; } = nameof(InitialLetters);

    public override Element render()
    {
        var facts = ResourceAccess.Facts;

        return new div(facts.Select(x => new FactMiniView { state = new FactMiniViewModel { Fact = x } }))
               + background("white")
               + Display.flex
               + FlexWrap.wrap
               + JustifyContent.center;
    }
}