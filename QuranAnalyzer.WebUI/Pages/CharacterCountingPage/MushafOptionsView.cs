using System;
using System.Linq.Expressions;
using QuranAnalyzer.WebUI.Pages.MainPage;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class MushafOptionsView:ReactComponent
{
    public bool MushafOptionsPanelIsVisible { get; set; }

    public MushafOptions MushafOption { get; set; }

    public Expression<Func<bool>> Bestaten_7_69 { get; set; }

    public Expression<Func<bool>> UseElifReferencesFromTanzil { get; set; }
    public Expression<Func<bool>> CountHamzaAsAlif { get; set; }

    public override Element render()
    {
        return new Panel
        {
            toggleable = true,
            collapsed  = true,
            header     = "Mushaf Ayarları",
            children =
            {
                new div
                {
                    style = {  display = "flex", flexDirection = "row", alignItems = "center"},
                    children =
                    {
                        new InputSwitch
                        {
                            checkedBind = UseElifReferencesFromTanzil
                        },
                        new HSpace(15),
                        new h5{text = "Elif sayımları için Tanzil.net'i referans al"}
                    }
                },
                new div
                {
                    style = {  display = "flex", flexDirection = "row", alignItems = "center"},
                    children =
                    {
                        new InputSwitch
                        {
                            checkedBind = Bestaten_7_69
                        },
                        new HSpace(15),
                        new h5{text = "7:69 daki bestaten'i Sad olarak say"}
                    }
                },
                new div
                {
                    style = {  display = "flex", flexDirection = "row", alignItems = "center"},
                    children =
                    {
                        new InputSwitch
                        {
                            checkedBind = CountHamzaAsAlif
                        },
                        new HSpace(15),
                        new h5{text = "Hemzeleri Elif(ﺍ) olarak say"}
                    }
                },
                new a{text = "Mushaf ayarları hakkında detaylı bilgi", href =  Extensions.GetPageLink(PageId.PageIdOfMushafOptionsDetail), }
            }
        };
    }
}