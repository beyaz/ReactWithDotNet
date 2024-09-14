namespace ReactWithDotNet.WebSite.HeaderComponents;

sealed class Logo : PureComponent
{
    protected override Element render()
    {
        return new a(DisplayFlexColumn, TextDecorationNone, Color(inherit), Width(140), Href(Page.Home.Url), AlignItemsStretch, Padding(8))
        {
            new FlexRow
            {
                SpaceX(12),
                new img { Src(Asset("react.svg")), Size(24) },
                SpaceX(60),
                new img { Src(Asset("net_core_logo.svg")), Size(24) }
            },

            new FlexRow
            {
                new GradientText{"React",FontWeight500},
                new GradientText{" with ",FontWeight400 ,FontSize(15)},
                new GradientText{"DotNet",FontWeight500}
            }
        };
    }
}

