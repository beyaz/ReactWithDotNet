namespace QuranAnalyzer;

public static class ArabicCharacterIndex
{
    #region Public Properties
    public static int Elif => DataAccess.harfler.GetIndex(ArabicCharacter.Alif).Value;
    #endregion
}