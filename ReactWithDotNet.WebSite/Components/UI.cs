namespace ReactWithDotNet.WebSite.Components;

static class UI
{
    public static Element ProgressBar(int total, int current)
    {
        return new div(WidthFull,Height(14), BackgroundColor("#dddddd"), Border(Solid(3, "#eee")), BorderRadius(15))
        {
            new div
            {
                HeightFull,
                Width(current, total),
                BorderRadius(15),
                BackgroundImage(linear_gradientTo("right", "#8490ff", "#a3eeff"))
            }
        };
    }
}