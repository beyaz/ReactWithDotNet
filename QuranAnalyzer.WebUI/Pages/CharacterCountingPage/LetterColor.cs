namespace QuranAnalyzer;

static class LetterColor
{
    static readonly string[] Colors = { "blue", "red", "#E0B4E8", "#D4D925", "#159E09" };

    public static string GetColor(int index)
    {
        if (index >= 0 && index < Colors.Length)
        {
            return Colors[index];
        }

        return "red";
    }
}