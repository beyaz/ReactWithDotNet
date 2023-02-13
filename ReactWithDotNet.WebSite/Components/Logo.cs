namespace ReactWithDotNet.WebSite.Components;

public class Logo : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow
        {
            new img { Src(Asset("react.svg")) }
        };
    }
}