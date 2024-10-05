using static ReactWithDotNet.WebSite.HeaderComponents.MobileMenu;

namespace ReactWithDotNet.WebSite.HeaderComponents;

sealed class MobileMenu : Component<MobileMenuState>
{
    protected override Element render()
    {
        return new div
        {
            new HamburgerButton { IsOpen = state.IsOpen, Click = ToggleIsOpen } + MD(DisplayNone),

            new FlexColumn(PositionFixed, Top(60), LeftRight(0), DisplayNoneWhenNotMobile, BoxShadow(rgba(170, 180, 190, 0.3), 0, 4, 20))
            {
                AnimateHeightAndOpacity(state.IsOpen),

                When(state.IsOpen, MinHeight(20)),
                BackgroundForPaper,
                Padding(20),
                Zindex(5),

                MenuAccess.MenuList.Select(m => new MenuView
                {
                    Model = m,
                    children =
                    {
                        m.Children.Select(x => new a
                        {
                            Href(x.PageLink),
                            TextDecorationNone,
                            Color(Theme.text_secondary),
                            Padding(10),

                            new div { x.Title, FontWeight700, FontSize14 },
                            new div { x.Description, FontWeight400, FontSize14 }
                        })
                    }
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

    Task ToggleIsOpen(MouseEvent _)
    {
        state = state with { IsOpen = !state.IsOpen };

        return Task.CompletedTask;
    }

    sealed class MenuView : Component<MenuView.State>
    {
        public Menu Model { get; init; }

        protected override Element render()
        {
            return new div(Padding(10))
            {
                new FlexRow(AlignItemsCenter, PaddingBottom(10))
                {
                    new span { Model.Title, FontSize14, FontWeight600, Color(Theme.text_secondary) },
                    new ArrowUpDownIcon { IsArrowUp = state.IsOpen } + OnClick(ToggleIsOpen)
                },
                new FlexColumn(JustifyContentSpaceEvenly, BorderLeft(Solid(1, Theme.grey_100)), PaddingLeft(10))
                {
                    AnimateHeightAndOpacity(state.IsOpen),
                    children
                }
            };
        }

        Task ToggleIsOpen(MouseEvent _)
        {
            state = state with { IsOpen = !state.IsOpen };

            return Task.CompletedTask;
        }

        internal sealed record State
        {
            public bool IsOpen { get; init; }
        }
    }

    internal record MobileMenuState
    {
        public bool IsOpen { get; init; }
    }
}