using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed class MainWindow : PureComponent
{
    protected override Element render()
    {
        return new PageLayout
        {

            createContent

        };

        Element createContent()
        {
            var pageName = Context.HttpContext.Request.Query[QueryKey.Page];

            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{pageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }
    }
}