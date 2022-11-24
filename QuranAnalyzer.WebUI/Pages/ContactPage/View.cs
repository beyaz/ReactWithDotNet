using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.ContactPage;


public class View : ReactComponent
{
    public bool IsChecked1 { get; set; }
    public bool IsChecked2 { get; set; }
    
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
                   new Switch
                   {
                       IsChecked = IsChecked1, ValueChange = ValueChange1
                   },
                   new Switch
                   {
                       IsChecked = IsChecked2, ValueChange = ValueChange2
                   },
               }
            } 
        };
    }

    void ValueChange1(bool obj)
    {
        IsChecked1 = obj;
    }

    void ValueChange2(bool obj)
    {
        IsChecked2 = obj;
    }
}