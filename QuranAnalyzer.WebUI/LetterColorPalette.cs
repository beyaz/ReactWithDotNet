namespace QuranAnalyzer.WebUI;

static class LetterColorPalette
{
    static readonly string[] Colors =
    {
        "blue", "red", "#9900CC", "#00FF00", "#33CC00"
    };

    public static string GetColor(int index)
    {
        if (index >= 0 && index < Colors.Length)
        {
            return Colors[index];
        }

        return "red";
    }
}