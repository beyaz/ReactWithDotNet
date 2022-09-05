using System;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI.Pages.ContactPage;


public class View : ReactComponent
{
    protected override Element render()
    {
        return new div
        {
            new div
            {
                innerText = "İletişim",
                style     =
                {
                    display = "flex", alignItems = "center", justifyContent = "center",
                    fontSize = "19px",
                    marginTop = "20px"
                    
                }
            },

            new div
            {
                
                innerText = "Yazılım, Felsefe, Kuran bu üç konuda istediğiniz kadar fikir alışverişine açığım.",
                style = { fontSize = "17px", marginLeftRight = "10px"}
            },

            new VStack
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