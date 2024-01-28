namespace ReactWithDotNet.WebSite.Components;

class ArrowUpDownIcon : PureComponent
{
    public bool IsArrowUp { get; set; }

    protected override Element render()
    {
        var arrowDown = new svg(svg.ViewBox(0, 0, 24, 24), svg.Width(24),svg.Height(24), Transition("all", 400))
        {
            new path { d = "M8.12 9.29 12 13.17l3.88-3.88c.39-.39 1.02-.39 1.41 0 .39.39.39 1.02 0 1.41l-4.59 4.59c-.39.39-1.02.39-1.41 0L6.7 10.7a.9959.9959 0 0 1 0-1.41c.39-.38 1.03-.39 1.42 0z" }
        };

        if (IsArrowUp)
        {
            return arrowDown+WithStyle([
                Transform("rotate(-180deg)")
            ]);
        }
        
        return arrowDown+WithStyle([
            Transform("rotate(0deg)")
        ]);
    }
}