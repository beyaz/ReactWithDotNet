namespace ReactWithDotNet.WebSite.Components;

static class UI
{
    public static Element ProgressBar(int total, int current)
    {
        return new div(WidthFull, Height(14), BorderRadius(15), Border(Solid(3, "#eee")), BackgroundColor("#dddddd"))
        {
            new div
            {
                Width(current, total),
                HeightFull,
                BorderRadius(15),
                BackgroundImage(linear_gradientTo("right", "#8490ff", "#a3eeff"))
            }
        };
    }
}