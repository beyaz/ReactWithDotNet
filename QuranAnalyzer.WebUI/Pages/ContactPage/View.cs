using System;
using ReactDotNet.Html5;
using ReactDotNet.PrimeReact;

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
                innerText = model.Header,
                style     =
                {
                    display = "flex", alignItems = "center", justifyContent = "center",
                    fontSize = "19px",
                    marginTop = "20px"
                    
                }
            },

            new div
            {
                
                innerText = model.Note,
                style = { fontSize = "17px", marginLeftRight = "10px"}
            },

            new VPanel
            {
                style={marginTop = "22px"},
               children=
               {
                   new InputText(),
                   new InputTextarea{ rows = 6},
                   new Button{ label       = "Gönder"}
               }
            } 
        };
    }
}