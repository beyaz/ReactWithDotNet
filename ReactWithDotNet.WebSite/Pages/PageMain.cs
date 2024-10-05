using System.IO;

namespace ReactWithDotNet.WebSite.Pages;

class PageMain : PureComponent
{
    protected override Element render()
    {
        return new CommonPageLayout
        {
            new FlexColumn(Gap(20))
            {
                new MainPageContentDescription(),

                SpaceY(15),

                new MainPageContentSample()
            }
        };
    }

    class MainPageContentDescription : PureComponent
    {
        protected override Element render()
        {
            return new FlexColumn(AlignItemsCenter)
            {
                SpaceY(16),
                new div(FontFamily_PlusJakartaSans_ExtraBold, FontSize40, LG(FontSize50), FontWeight800, WhenMediaSizeLessThan(MD, TextAlignCenter))
                {
                    LineHeight(50), LG(LineHeight(60)),

                    // new HighlightedText { Text = "Write [react.js]  application in [c#]  language" }

                    "Write ", new GradientText { "react.js" }, "  application in ", new GradientText { "c#" }, "  language"
                },

                SpaceY(20),
                new div
                {
                    LineHeight30,
                    Color(Gray700),
                    FontWeight400,
                    """
                    ReactWithDotNet is a new way to build web applications. 
                    Build component tree in server side on .Net Core (c#) then sends component tree to react client side. 
                    When any component has any action then React client communicates serverside only required parts of application. 
                    Does not hold any state at .Net Core server side. 
                    Thousend of users can be handle at same time. 
                    In summary, combines power of c#, .net and react.js
                    """
                },
                SpaceY(40),

                new FlexRow(JustifyContentFlexStart, WidthFull, FlexWrap, Gap(32))
                {
                    new PrimaryLinkButton { Text = "Documentation", Href      = Page.Doc.Url } + WidthFull + SM(Width(auto)),
                    new PrimaryLinkButton { Text = "Showcase", Href           = Page.PageShowcase.Url } + WidthFull + SM(Width(auto)),
                    new PrimaryLinkButton { Text = "Project Milestones", Href = Page.PageMilestones.Url } + WidthFull + SM(Width(auto))
                }
            };
        }

        sealed class PrimaryLinkButton : PureComponent
        {
            public string Href { get; init; }
            public string Text { get; init; }

            protected override Element render()
            {
                return new a(Href(Href), DisplayFlexRowCentered, SizeFitContent)
                {
                    Text ?? "Button",
                    
                    new []
                    {
                        Padding(15,45),
                        BackgroundImage("linear-gradient(to right, #DA22FF 0%, #9733EE  51%, #DA22FF  100%)"),
                        BoxShadow(0,0,20,"#eee"),
                        BorderRadius(10),
                        Color(White),
                        TextDecorationNone,
                        Transition("all 0.5s"),
                        BackgroundSize("200% auto"),
                        Hover([
                            BackgroundPosition("right center")
                        ])
                    }
                };
            }
        }
    }

    class MainPageContentSample : Component
    {
        protected override Element render()
        {
            string[] files = [Path.Combine(nameof(Components), nameof(HomePageDemoComponent) + ".cs")];

            return new Playground
            {
                TypeOfTargetComponent = typeof(HomePageDemoComponent),

                Files = files.Select(fi => (Path.GetFileName(fi), File.ReadAllText(fi))).ToList()
            } + Height(300);
        }
    }
}