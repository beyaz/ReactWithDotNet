using System.IO;
using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class MainWindow : PureComponent
{
    protected override Element render()
    {
        return new div(WidthMaximized, HeightMaximized)
        {
            new MainPageHeader(),

            new main
            {
                // createContent
                new Playground
                {
                    Files                         = Directory.GetFiles(nameof(_1_HelloWorld)).Select(fi=>(Path.GetFileName(fi), File.ReadAllText(fi))).ToList(),
                    FullTypeNameOfTargetComponent = typeof(ReactWithDotNet.WebSite._1_HelloWorld.HomePageDemoComponent).FullName,
                    TypeOfTargetComponent         = typeof(ReactWithDotNet.WebSite._1_HelloWorld.HomePageDemoComponent)
                }
            },


            //new footer(BorderTop(Solid(1, Theme.grey_100)), Height(50), DisplayFlexRowCentered)
            //{
            //    new HighlightedText { Text = RawData.FooterText }
            //}
        };

        Element createContent()
        {
            var pageName = KeyForHttpContext[Context].Request.Query[QueryKey.Page];
            
            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{pageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }
    }
}