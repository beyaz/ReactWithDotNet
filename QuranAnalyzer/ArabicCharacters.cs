namespace QuranAnalyzer;

public static class ArabicCharacters
{
    #region Static Fields
    public const string Alif = "ا";
    public const string Baa = "ب";
    public const string Taa = "ت";
    public const string Thaa = "ث";
    public const string Jiim = "ج‎";
    public const string Haa = "ح";
    public const string Khaa = "خ";
    public const string Daal = "د";




    public const string Kaf = "ق";
    public const string Sad = "ص";
    public const string Sin = "س";
    public const string Ya = "ي";
    public const string Mim = "م";
    public const string Lam = "ل";
    public const string Ra = "ر";

    public const string Kef = "ك";
    public const string Ha = "ه";
    public const string Ayn = "ع";

    

    public const string T = "ط";

    public const string Nun = "ن";
    #endregion
}


public static class ArabicCharacterIndex
{
    public static int Elif => DataAccess.harfler.GetIndex(ArabicCharacters.Alif).Value;
}