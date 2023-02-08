namespace QuranAnalyzer.WebUI.Components;

public class Tooltip2 : ReactComponent
{
    public string Text { get; set; }

    protected override Element render()
    {
        var firstChild = children.FirstOrDefault() ?? new div{WidthHeight(50),Background("red")};

        return new div(PositionRelative)
        {
            Children(firstChild, new span(FontWeightBold,FontSize11)
            {
                text = Text,
                style =
                {
                    letterSpacing = "1px",
                    display = "block",
                    backgroundColor = "#211f1fd4",
                    color           = "white",
                    textAlign       = "center",
                    borderRadius    = "6px",
                    padding         = "4px 8px",
                    position        = "absolute",
                    zIndex          = "1",
                    top             = "110%",
                    left            = "10%",
                    marginLeft      = "-60px",
                    after =
                    {
                        content     = "",
                        position    = "absolute",
                        bottom      = "100%",
                        left        = "40%",
                        marginLeft  = "-5px",
                        borderWidth = "5px",
                        borderStyle = "solid",
                        borderColor = "transparent transparent #211f1fd4 transparent"
                    },
                    hover =
                    {
                        display = "block"
                    }
                }
            }),
            
            
        };
    }
}