using QuranAnalyzer.WebUI.Components;
using static QuranAnalyzer.WebUI.PageId;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class MushafOptionsView : ReactComponent
{
    public MushafOption Model { get; set; } = new();
    
    [ReactCustomEvent]
    public Action<MushafOption> MushafOptionChanged { get; set; }
    
    protected override Element render()
    {
        return new FlexColumn(JustifyContentCenter, Gap(10))
        {
            new SwitchWithLabel
            {
                label = "Elif sayımları için Tanzil.net'i referans al",
                value = Model.UseElifReferencesFromTanzil,
                valueChange = x =>
                {
                    Model.UseElifReferencesFromTanzil = x;
                    FireMushafOptionChanged();
                }
            },

            new SwitchWithLabel
            {
                label = "7:69 daki bestaten'i Sad olarak say",
                value = Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten,
                valueChange = x =>
                {
                    Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = x;
                    FireMushafOptionChanged();
                }
            },

            new a { Text("Mushaf ayarları hakkında detaylı bilgi"), Href(GetPageLink(PageIdOfMushafOptionsDetail)), MarginTop(10) }
        };
    }

    void FireMushafOptionChanged()
    {
        DispatchEvent(()=>MushafOptionChanged, Model);
    }
}