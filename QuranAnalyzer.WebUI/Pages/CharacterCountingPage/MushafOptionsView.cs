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
        return new FlexColumn(JustifyContentCenter, Gap(20))
        {
            new SwitchWithLabel
            {
                label = "Elif sayımları için Tanzil.net'i referans al",
                LabelMaxWidth = 250,
                value = Model.UseElifReferencesFromTanzil,
                valueChange = x =>
                {
                    Model.UseElifReferencesFromTanzil = x;
                    FireMushafOptionChanged();
                }
            },

            new SwitchWithLabel
            {
                label         = "7:69 ve 2:245 daki bestaten ve yebsutu kelimelerindeki sad-sin yazım farklılığında Sad harfini tercih et",
                LabelMaxWidth = 250,
                value         = Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten,
                valueChange = x =>
                {
                    Model.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = x;
                    FireMushafOptionChanged();
                }
            },


            new SwitchWithLabel
            {
                label         = "68:1 tek nun olarak say-(henüz çalışmıyor ayarlayacağım)",
                LabelMaxWidth = 250,
                value         = Model.Chapter_68_Should_Single_Nun,
                valueChange = x =>
                {
                    Model.Chapter_68_Should_Single_Nun = x;
                    FireMushafOptionChanged();
                }
            },

            new SwitchWithLabel
            {
                label         = "11:70 ve 30:21 surelerdeki Lam harf farklılığında Tanzil.neti tercih et (henüz çalışmıyor ayarlayacağım)",
                LabelMaxWidth = 250,
                value         = Model.Use_Laam_SpecifiedByTanzil,
                valueChange = x =>
                {
                    Model.Use_Laam_SpecifiedByTanzil = x;
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