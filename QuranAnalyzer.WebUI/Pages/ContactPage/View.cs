using System;
using ReactDotNet;

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
            new div {text = model.Header},
            new div {text = model.Note}
        };
    }
}

