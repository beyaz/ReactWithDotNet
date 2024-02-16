namespace ReactWithDotNet.WebSite.Components;

static class UI
{
    public static Element ProgressBar(int total, int current)
    {
        return new div(WidthFull,BackgroundColor("#dddddd"), Border(Solid(3,"#eee")), BorderRadius(15), Height(14))
        {
            new div
            {
                HeightFull,
                Width(total,current),
                BorderRadius(16),
                
                BackgroundImage(linear_gradientTo("right","#8490ff","#a3eeff"))
            }
        };
    }
}