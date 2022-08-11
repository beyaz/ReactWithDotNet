
namespace QuranAnalyzer;

[Serializable]
public sealed class MushafOption
{
    #region Public Properties
    public bool UseElifCountsSpecifiedByRK  =>!UseElifReferencesFromTanzil;
    public bool Use_Sad_in_Surah_7_Verse_69_in_word_bestaten { get; set; }
    public bool Use_Lam_SpecifiedByRK { get; set; }

    public bool UseElifReferencesFromTanzil { get; set; }

    public bool CountHamzaAsAlif { get; set; } = true;

    #endregion
}