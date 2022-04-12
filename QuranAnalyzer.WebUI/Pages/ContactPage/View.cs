using System;
using ReactDotNet;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.ContactPage;

[Serializable]
public sealed class Model
{
    public string Header { get; set; }
    public string Note { get; set; }
}

public class View : PageBase
{
    public override string id { get; set; } = nameof(ContactPage);

    public override Element render()
    {
        var model = ResourceHelper.Read<Model>("Pages.ContactPage.Data.yaml");

        return new div
        {
            new div(Display.flex,AlignItems.center,JustifyContent.center,fontSize(19),marginTop(20))
            {
                text = model.Header
            },

            new div(fontSize(17) , marginLeft(10) , marginRight(10)) {text = model.Note},

            new VPanel(marginTop(22))
            {
                new InputText(),
                new InputTextarea{ rows = 6},
                new Button{ label = "Gönder"}
            } 
        };
    }
}