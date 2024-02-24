
using ReactWithDotNet.WebSite.HeaderComponents;

namespace ReactWithDotNet.WebSite.Pages;

class PageDocumentation : PureComponent
{
    protected override Element render()
    {
        return new div(WidthFull,HeightFull)
        {
            new MainPageHeader(),

            new main(DisplayFlexRow)
            {
                UI.LeftMenu(),
                UI.SampleDocumentContent()
            },
            
            //new Footer()
            
        };
    }
}


