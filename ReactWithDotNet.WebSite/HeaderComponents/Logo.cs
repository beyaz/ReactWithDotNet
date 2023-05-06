namespace ReactWithDotNet.WebSite.HeaderComponents;

public class Logo : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexColumn(Width(140), AlignItemsCenter, Padding(5))
        {
            new FlexRow(Gap(25))
            {
                new img { Src(Asset("react.svg")), WidthHeight(30) },
                new img { Src(Asset("net_core_logo.svg")), WidthHeight(30) }
            },

            new FlexRow(Gap(3)){ (span)"React"+FontWeight500, " with " , (span)"DotNet" + FontWeight500 }
        };
    }
}