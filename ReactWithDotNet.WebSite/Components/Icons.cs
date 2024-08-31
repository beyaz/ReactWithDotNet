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

    public int? Size { get; init; }
    
    protected override Element render()
    {
        var size = Size ?? 24;
        
        return new div(Size(size))
        {
            new svg(ViewBox(0, 0, 24, 24), svg.FocusableFalse, svg.Size(size))
            {
                new path { d = "M20.23 17.885L22 16.115L12 6.11501L2 16.115L3.77 17.885L12 9.65501L20.23 17.885Z", fill = Color}
            }
        };
    }
}


class IconFilter: PureComponent
{
    public string Color { get; init; } = "#1C2B3D";

    public int? Size { get; init; }
    
    protected override Element render()
    {
        var size = Size ?? 24;
        
        return new div(Size(size))
        {
            new svg(ViewBox(0, 0, 24, 24), svg.FocusableFalse, svg.Size(size))
            {
                new path { d = "M3 7C3 6.44772 3.44772 6 4 6H20C20.5523 6 21 6.44772 21 7C21 7.55228 20.5523 8 20 8H4C3.44772 8 3 7.55228 3 7ZM6 12C6 11.4477 6.44772 11 7 11H17C17.5523 11 18 11.4477 18 12C18 12.5523 17.5523 13 17 13H7C6.44772 13 6 12.5523 6 12ZM9 17C9 16.4477 9.44772 16 10 16H14C14.5523 16 15 16.4477 15 17C15 17.5523 14.5523 18 14 18H10C9.44772 18 9 17.5523 9 17Z", fill = Color}
            }
        };
    }
}