namespace ReactWithDotNet.WebSite.HeaderComponents;

public class Logo : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexColumn(Width(140), AlignItemsCenter)
        {
            new FlexRow(Gap(25))
            {
                new img { Src(Asset("react.svg")), WidthHeight(30) },
                new img { Src(Asset("net_core_logo.svg")), WidthHeight(30) }
            },

            new small{"React with DotNet"}
        };
    }
}