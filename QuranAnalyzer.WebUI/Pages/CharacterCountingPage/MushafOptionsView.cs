using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.WebUI.PageId;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class MushafOptionsView : ReactComponent<MushafOption>
{
    protected override void constructor()
    {
        state = Context.TryGetValue(ContextKey.MushafOptionKey) ?? new MushafOption();
    }

    protected override Element render()
    {
        state = Context.TryGetValue(ContextKey.MushafOptionKey) ?? state;

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
                            @checked = state.UseElifReferencesFromTanzil,
                            onChange = x =>
                            {
                                state.UseElifReferencesFromTanzil = x.value;
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
                        new h5 { text = "7:69 daki bestaten'i Sad olarak say" }
                    }
                },
                new div
                {
                    style = { display = "flex", flexDirection = "row", alignItems = "center" },
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
                        new h5 { text = "Hemzeleri Elif(ﺍ) olarak say" }
                    }
                },
                new a { text = "Mushaf ayarları hakkında detaylı bilgi", href = GetPageLink(PageIdOfMushafOptionsDetail), }
            }
        };
    }

    void FireMushafOptionChanged()
    {
        Client.MushafOptionChanged(state);
    }
}