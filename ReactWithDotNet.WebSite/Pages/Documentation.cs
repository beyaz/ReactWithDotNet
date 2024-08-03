
using Microsoft.AspNetCore.Http.Extensions;
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
                UI.LeftMenu(KeyForHttpContext[Context].Request.GetDisplayUrl()),
                UI.SampleDocumentContent()
            },
            
            new MainPageFooter()
            
        };
    }
}


