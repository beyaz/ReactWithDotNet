using ReactWithDotNet.WebSite.Content;

namespace ReactWithDotNet.WebSite.Components;




class RawCardViewer : ReactPureComponent
{
    public RawCard Model { get; set; }

    Element CreateTitleIcon()
    {
        if (Model.IconFile != null)
        {
            return GetSvgByClassName(Model.IconFile);
        }

        return new svg(wh(30))
        {
            fill    = "none",
            viewBox = "0 0 24 24",
            xmlns   = "http://www.w3.org/2000/svg",
            children =
            {
                new path { d = "M21 7v10c0 3-1.5 5-5 5H8c-3.5 0-5-2-5-5V7c0-3 1.5-5 5-5h8c3.5 0 5 2 5 5Z", stroke = "#FF4ECD" }, new path { d = "M14.5 4.5v2c0 1.1.9 2 2 2h2M10 13l-2 2 2 2M14 13l2 2-2 2", stroke = "#FF4ECD" }
            }
        };
    }
    
    protected override Element render()
    {
        return new div(FlexGrow(1))
        {
            MediaQueryOnDesktop(Width(33.333 | percent)),
            MediaQueryOnTablet(Width(50 | percent)),
            MediaQueryOnMobile(Width(100 | percent)),

            new div
            {
                BackdropFilterBlur(10),
                Padding(20),
                Margin(15),
                BorderRadius(15),
                Border(Solid(1,Theme.grey_200)),
                
                Hover(BorderColor(Theme.grey_300), BoxShadow(0,2,8,2,Theme.grey_100)),

                MediaQueryOnDesktop(Height(150)),
                MediaQueryOnTablet(Height(160)),
                MediaQueryOnMobile(Height(100)),

                new FlexRow(MarginBottom(12), AlignItemsCenter, Gap(5))
                {
                    new FlexRowCentered(Padding(8),Background(Theme.pink100), BorderRadius("50%"))
                    {
                        CreateTitleIcon
                    },

                    new div(FontWeightBold, FontSize(1.1 | rem))
                    {
                        Model.Title
                    }
                },
                new div(Color(Theme.grey_700), FontSize(0.875|rem))
                {
                    Model.Description
                }
            }
        };
    }
}