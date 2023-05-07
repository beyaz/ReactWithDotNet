namespace ReactWithDotNet.WebSite.Components;

class Article : ReactPureComponent
{
    public string FilePathInContentFolder { get; set; }
    
    protected override Element render()
    {
        var htmlContent = GetArticleHtmlContent(FilePathInContentFolder);

        return new article
        {
            dangerouslySetInnerHTML = htmlContent
        };
    }
}