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
            new div
            {
                text = model.Header
            } + marginTop(20)
              + Display.flex
              + JustifyContent.center
              + AlignItems.center
              + fontSize(19)
           ,

            new div {text = model.Note} + fontSize(17) + marginLeft(10) + marginRight(10),

            new VPanel
            {
                new InputText(),
                new InputTextarea{ rows = 6},
                new Button{ label = "Gönder"}
            } + marginTop(22)
        };
    }
}