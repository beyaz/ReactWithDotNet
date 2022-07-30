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
    public const string Dhaal = "ذ";
    public const string Raa = "ر";
    public const string Zay = "ز‎";
    public const string Siin = "س";
    public const string Shiin = "ش";
    public const string Saad = "ص";
    public const string Daad = "ض";

    public const string Zaa = "ظ";
    public const string Ayn = "ع";
    public const string Ghayn = "غ";
    public const string Faa = "ف";
    public const string Qaaf = "ق";
    public const string Kaaf = "ك";
    public const string Laam = "ل";
    

    public const string Ya = "ي";
    public const string Mim = "م";
    
    

    
    public const string Ha = "ه";
    

    

    public const string T = "ط";

    public const string Nun = "ن";
    #endregion
}


public static class ArabicCharacterIndex
{
    public static int Elif => DataAccess.harfler.GetIndex(ArabicCharacters.Alif).Value;
}