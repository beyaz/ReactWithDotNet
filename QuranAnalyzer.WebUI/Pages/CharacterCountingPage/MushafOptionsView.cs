using ReactWithDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class MushafOptionsView: ReactComponent<MushafOption>
{
    public MushafOption MushafOption { get; set; }

    protected override void constructor()
    {
        state = MushafOption ?? new MushafOption();
    }

    public void FireMushafOptionChanged()
    {
        ClientTask.DispatchEvent(ApplicationEventName.MushafOptionChanged, state);
    }

    public override Element render()
    {
        return new Panel
        {
            toggleable     = true,
            //collapsed      = true,
            header         = "Mushaf Ayarları",
            headerTemplate = "QuranAnalyzer_WebUI_PanelHeaderTemplate",
            children =
            {
                new div
                {
                    style = {  display = "flex", flexDirection = "row", alignItems = "center"},
                    children =
                    {
                        new InputSwitch
                        {
                            @checked = state.UseElifReferencesFromTanzil,
                            onChange = x =>
                            {
                                state.UseElifReferencesFromTanzil = x.value;
                                FireMushafOptionChanged();
                            }
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
                            


                            @checked = state.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten,
                            onChange = x =>
                            {
                                state.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = x.value;
                                FireMushafOptionChanged();
                            }
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
                            @checked = state.CountHamzaAsAlif,
                            onChange = x =>
                            {
                                state.CountHamzaAsAlif = x.value;
                                FireMushafOptionChanged();
                            }
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