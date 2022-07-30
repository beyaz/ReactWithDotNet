namespace QuranAnalyzer;

public static class ArabicCharacterIndex
{
    #region Public Properties
    public static int Elif => ArabicLetter.AllArabicLetters.GetIndex(ArabicLetter.Alif).Value;
    #endregion
}