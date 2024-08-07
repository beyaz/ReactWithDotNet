﻿using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed class MainWindow : PureComponent
{
    protected override Element render()
    {
        return new div(SizeFull)
        {
            new MainPageHeader(),
            
            new main(PaddingY(48))
            {
                new CommonPageLayout
                {
                    createContent
                }
                
            },
            
            new MainPageFooter()
        };

        Element createContent()
        {
            var pageName = KeyForHttpContext[Context].Request.Query[QueryKey.Page];

            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{pageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }
    }
    
    sealed class CommonPageLayout: PureComponent
    {
        protected override Element render()
        {
            return new FlexRowCentered(WidthFull)
            {
                new FlexColumn(ContainerStyle, PaddingY(48))
                {
                    children
                }
            };
        }
    }
}