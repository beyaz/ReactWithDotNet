namespace QuranAnalyzer;

public static class ArabicCharacterIndex
{
    #region Public Properties
    public static int Elif => ArabicCharacter.AllArabicLetters.GetIndex(ArabicCharacter.Alif).Value;
    #endregion
}