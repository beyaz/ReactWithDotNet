namespace QuranAnalyzer;

public static class ArabicLetterNumericValue
{
    public static int GetNumericalValue(int arabicLetterIndex)
    {
        return NumericValueMap[arabicLetterIndex];
    }

    // @formatter:off
    public const int Alif = 1;
    public const int Baa = 2;
    public const int Taa = 400;
    public const int Thaa = 500;
    public const int Jiim = 3;
    public const int Haa = 8;
    public const int Khaa = 600;
    public const int Daal = 4;
    public const int Dhaal = 700;
    public const int Raa = 200;
    public const int Zay = 7;
    public const int Siin = 60;
    public const int Shiin = 300;
    public const int Saad = 90;
    public const int Daad = 800;
    public const int Taa_ = 9;
    public const int Zaa = 900;
    public const int Ayn = 70;
    public const int Ghayn = 1000;
    public const int Faa = 80;
    public const int Qaaf = 100;
    public const int Kaaf = 20;
    public const int Laam = 30;
    public const int Miim = 40;
    public const int Nun = 50;
    public const int Haa_ = 5;
    public const int Waaw = 6;
    public const int Yaa = 10;

    static readonly int[] NumericValueMap =
    {
        Alif,
        Baa,
        Taa,
        Thaa,
        Jiim,
        Haa,
        Khaa,
        Daal,
        Dhaal,
        Raa,
        Zay,
        Siin,
        Shiin,
        Saad,
        Daad,
        Taa_,
        Zaa,
        Ayn,
        Ghayn,
        Faa,
        Qaaf,
        Kaaf,
        Laam,
        Miim,
        Nun,
        Haa_,
        Waaw,
        Yaa
    };
    // @formatter:on
}