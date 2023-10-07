namespace ReactWithDotNet.WebSite.Components;

class Article : PureComponent
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