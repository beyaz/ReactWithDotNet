
namespace ReactWithDotNet.WebSite.Pages;

class PageMain: PureComponent
{
    protected override Element render()
    {
        return new FlexRowCentered(WidthFull)
        {
            new FlexColumn(Gap(20), ContainerStyle, FlexDirectionColumn)
            {
                new MainPageContentDescription(),
                
                SpaceY(15),
                
                new MainPageContentSample{ Height(300)},
                
                SpaceY(30)
            }
        };
    }
}