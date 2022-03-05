using ReactDotNet;

namespace QuranAnalyzer.WebUI;

class Theme
{
    public readonly string MainPaperBackgroundColor = "white";
}

class divWithBorder:div
{
    public override string tagName => nameof(div);
    public divWithBorder()
    {
        style.border = "1px solid rgb(218, 220, 224)";
    }
}