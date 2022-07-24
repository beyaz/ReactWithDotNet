namespace QuranAnalyzer;

public static class ArabicCharacters
{
    #region Static Fields
    public const string Kaf = "ق";
    public const string Sad = "ص";
    public const string Sin = "س";
    public const string Ya = "ي";
    public const string Elif = "ا";
    public const string Mim = "م";
    public const string Lam = "ل";
    public const string Ra = "ر";

    public const string Kef = "ك";
    public const string Ha = "ه";
    public const string Ayn = "ع";

    public const string HH = "ح";

    public const string T = "ط";

    public const string Nun = "ن";
    #endregion
}


public static class ArabicCharacterIndex
{
    public static int Elif => DataAccess.harfler.GetIndex(ArabicCharacters.Elif).Value;
}