namespace QuranAnalyzer.WebUI.Components;

class SvgHamburgerIcon : ReactComponent
{
    protected override Element render()
    {
        return new svg
        {
            viewBox = "0 0 24 24",
            children =
            {
                new path { d = "M3 18h18v-2H3v2zm0-5h18v-2H3v2zm0-7v2h18V6H3z", fill = Theme.TextColor }
            },
            style =
            {
                Height(24),
                Margin(12)
            }
        };
    }
}