namespace ReactWithDotNet.WebSite.Components;



class IconSuccess : PureComponent
{
    public string Color { get; init; } = "#2BDE3F";

    protected override Element render()
    {
        return new div(Size(24))
        {
            new svg(ViewBox(64, 64, 896, 896), svg.Focusable("false"), Fill(Color))
            {
                new path { d = "M512 64C264.6 64 64 264.6 64 512s200.6 448 448 448 448-200.6 448-448S759.4 64 512 64zm193.5 301.7l-210.6 292a31.8 31.8 0 01-51.7 0L318.5 484.9c-3.8-5.3 0-12.7 6.5-12.7h46.9c10.2 0 19.9 4.9 25.9 13.3l71.2 98.8 157.2-218c6-8.3 15.6-13.3 25.9-13.3H699c6.5 0 10.3 7.4 6.5 12.7z", style = { scrollMarginTop = "80px" } }
            }
        };
    }
}


class IconArrowUp: PureComponent
{
    public string Color { get; init; } = "#1C2B3D";

    public int Size { get; init; } = 24;
    
    protected override Element render()
    {
        return new div(Size(Size))
        {
            new svg(ViewBox(0, 0, Size, Size), svg.FocusableFalse, svg.Size(Size))
            {
                new path { d = "M20.23 17.885L22 16.115L12 6.11501L2 16.115L3.77 17.885L12 9.65501L20.23 17.885Z", fill = Color}
            }
        };
    }
}