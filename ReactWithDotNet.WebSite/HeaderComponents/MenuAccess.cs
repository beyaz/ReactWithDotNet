namespace ReactWithDotNet.WebSite.HeaderComponents;

public class Menu
{
    public string Title { get; set; }
    public IReadOnlyList<MenuItem> Children { get; set; }
}
public class MenuItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string PageLink { get; set; }
    public string SvgFileName { get; set; } = "doc.svg";
}

static class MenuAccess
{
    public static IReadOnlyList<Menu> MenuList { get; set; }

    static MenuAccess()
    {
        MenuList =
        [
            new()
            {
                Title = "Products",
                Children =
                [
                    new()
                    {
                        Title       = "PageTechnicalDetail",
                        PageLink    = Page.PageTechnicalDetail.Url,
                        Description = "Teknink details of ReactWithDotnet"
                    },
                    new()
                    {
                        Title       = "Modifiers",
                        PageLink    = Page.PageModifiers.Url,
                        Description = "What is modifier"
                    },
                    new()
                    {
                        Title       = "Import Html",
                        PageLink    = Page.LiveEditor.Url,
                        Description = "Import any html / Live Editor"
                    }
                ]
            }
        ];
    }
}