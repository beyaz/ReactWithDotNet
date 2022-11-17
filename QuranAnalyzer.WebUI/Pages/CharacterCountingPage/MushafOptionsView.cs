using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.WebUI.PageId;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class MushafOptionsView : ReactComponent
{
    public MushafOption Model { get; set; } = new();
    
    [ReactCustomEvent]
    public Action<MushafOption> MushafOptionChanged { get; set; }
    
    protected override Element render()
    {
        return new div
        {
            children =
            {
                new div
                {
                    style = { display = "flex", flexDirection = "row", alignItems = "center" },
                    children =
                    {
                        new InputSwitch
                        {
                            @checked = Model.UseElifReferencesFromTanzil,
                            onChange = x =>
                            {
                                Model.UseElifReferencesFromTanzil = x.value;
                                FireMushafOptionChanged();
                            }
                        },
                        new HSpace(15),
                        new h5 { text = "Elif sayımları için Tanzil.net'i referans al" }
                    }
                },
                new div
                {
                    style = { display = "flex", flexDirection = "row", alignItems = "center" },
                    children =
                    {

                        new FlexRow(AlignItemsCenter, Gap(5))
                        {
                            new Switch { IsChecked = Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten, ValueChange = x =>
                                {
                                    Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = x;
                                    FireMushafOptionChanged();
                                }
                            },
                            "7:69 daki bestaten'i Sad olarak say"
                        },


                        
                    }
                },
                new div
                {
                    style = { display = "flex", flexDirection = "row", alignItems = "center" },
                    children =
                    {
                        new InputSwitch
                        {
                            @checked = Model.CountHamzaAsAlif,
                            onChange = x =>
                            {
                                Model.CountHamzaAsAlif = x.value;
                                FireMushafOptionChanged();
                            }
                        },
                        new HSpace(15),
                        new h5 { text = "Hemzeleri Elif(ﺍ) olarak say" }
                    }
                },
                new a { text = "Mushaf ayarları hakkında detaylı bilgi", href = GetPageLink(PageIdOfMushafOptionsDetail), }
            }
        };
    }

    void FireMushafOptionChanged()
    {
        DispatchEvent(()=>MushafOptionChanged, Model);
    }
}