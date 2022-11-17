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
            createSwitchWithLabel("Elif sayımları için Tanzil.net'i referans al",Model.UseElifReferencesFromTanzil,x =>
            {
                Model.UseElifReferencesFromTanzil = x;
                FireMushafOptionChanged();
            }),

            createSwitchWithLabel("7:69 daki bestaten'i Sad olarak say",Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten,x =>
            {
                Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = x;
                FireMushafOptionChanged();
            }),

            new a { Text("Mushaf ayarları hakkında detaylı bilgi"), Href(GetPageLink(PageIdOfMushafOptionsDetail)), MarginTop(10) }
        };

        Element createSwitchWithLabel(string label, bool value, Action<bool> valueChange)
        {
            return new FlexRow(AlignItemsCenter, Gap(5))
            {
                new Switch
                {
                    IsChecked = value, ValueChange = valueChange
                },
               label
            };
        }
    }

    void FireMushafOptionChanged()
    {
        DispatchEvent(()=>MushafOptionChanged, Model);
    }
}