using ReactWithDotNet.WebSite.Content;

namespace ReactWithDotNet.WebSite.HeaderComponents;

class MobileMenu : ReactComponent
{
    public bool IsOpen { get; set; }

    protected override Element render()
    {
        return new div
        {
            new HamburgerButton { IsOpen = IsOpen, Click = _ => IsOpen = !IsOpen } + MediaQueryOnTabletOrDesktop(DisplayNone),

            new FlexColumn(PositionFixed, Top(70), LeftRight(0), DisplayNoneWhenNotMobile,
                           BoxShadow("rgb(170 180 190 / 30%) 0px 4px 20px"))
            {
                AnimateHeightAndOpacity(IsOpen),

                When(IsOpen, MinHeight(20)),
                BackgroundForPaper,
                Padding(20),
                Zindex(5),

                RawData.MenuList.Select(m => new MenuView
                {
                    Modify<MenuView>(ui => ui.Model = m),

                    m.Children.Select(x => new a
                    {
                        GetPageLink(x.PageName),
                        TextDecorationNone,
                        Color(Theme.text_secondary),
                        Padding(10),

                        new div { x.Title, FontWeight700, FontSize14 },
                        new div { x.Description, FontWeight400, FontSize14 }
                    })
                })
            }
        };
    }

    static StyleModifier AnimateHeightAndOpacity(bool isVisible)
    {
        return Transition(nameof(Style.all), 300) +
               Transition(nameof(Style.opacity), 300) +
               (isVisible
                   ? VisibilityVisible + Opacity1 + HeightAuto
                   : VisibilityCollapse + Opacity0 + Height0);
    }

    [ReactHigherOrderComponent]
    class MenuView : ReactComponent
    {
        public Menu Model { get; set; }

        protected override Element render()
        {
            return new div(Padding(10))
            {
                new CollapseContainer
                {
                    ContentOnOpened = new Fragment(Key("content"))
                    {
                        new FlexRow(AlignItemsCenter, PaddingBottom(10))
                        {
                            new span { Model.Title, FontSize14, FontWeight600, Color(Theme.text_secondary) },
                            new ArrowUpDownIcon { IsArrowUp = true }
                        },
                        new FlexColumn(JustifyContentSpaceEvenly, BorderLeft(Solid(1, Theme.grey_100)), PaddingLeft(10))
                        {
                            AnimateHeightAndOpacity(true),
                            children
                        }
                    },
                    ContentOnClosed = new Fragment(Key("content"))
                    {
                        new FlexRow(AlignItemsCenter, PaddingBottom(10))
                        {
                            new span { Model.Title, FontSize14, FontWeight600, Color(Theme.text_secondary) },
                            new ArrowUpDownIcon { IsArrowUp = false }
                        },
                        new FlexColumn(JustifyContentSpaceEvenly, BorderLeft(Solid(1, Theme.grey_100)), PaddingLeft(10))
                        {
                            AnimateHeightAndOpacity(false),
                            children
                        }
                    }
                }
            };
        }
    }
}

class CollapseContainer : ThirdPartyReactComponent
{
    [ReactProp]
    public Element ContentOnOpened { get; set; }

    [ReactProp]
    public Element ContentOnClosed{ get; set; }
}