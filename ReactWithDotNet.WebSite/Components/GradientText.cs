namespace ReactWithDotNet.WebSite.Components;

sealed class GradientText : PureComponent
{
    protected override Element render()
    {
        return new span
        {
            children,
            
            PaddingLeftRight(3),
            WebkitTextFillColor(transparent),
            Background(linear_gradientTo("right", Blue400, Blue600)),
            BackgroundClipText,
            WebkitBackgroundClipText
        };
    }
}