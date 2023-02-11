namespace QuranAnalyzer.WebUI.Components;

class SvgHamburgerIcon : ReactPureComponent
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

class MenuCloseIcon : ReactPureComponent
{
    protected override Element render()
    {
        return new svg
        {
            viewBox = "0 0 24 24",
            children =
            {
                new path { d = "M 4.7070312 3.2929688 L 3.2929688 4.7070312 L 10.585938 12 L 3.2929688 19.292969 L 4.7070312 20.707031 L 12 13.414062 L 19.292969 20.707031 L 20.707031 19.292969 L 13.414062 12 L 20.707031 4.7070312 L 19.292969 3.2929688 L 12 10.585938 L 4.7070312 3.2929688 z", fill = Theme.TextColor }
            },
            style =
            {
                Height(24),
                Margin(12)
            }
        };
    }
}