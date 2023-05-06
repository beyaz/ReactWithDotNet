namespace ReactWithDotNet.WebSite.Content;

public class Menu
{
    public string Title { get; set; }
    public IReadOnlyList<MenuItem> Children { get; set; }
}
public class MenuItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string PageName { get; set; }
    public string SvgFileName { get; set; } = "doc.svg";
}

public class SocialMediaLink
{
    public string Text { get; set; }
    public string Link { get; set; }
    public string Svg { get; set; }
}

public class YoutubeLink
{
    public string YoutubeVideoId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}


public class RawCard
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string IconFile { get; set; }
}


public class SiteRawData
{
    public IReadOnlyList<Menu> MenuList { get; set; }
    public IReadOnlyList<SocialMediaLink> SocialMediaLinks { get; set; }
    public IReadOnlyList<RawCard> Cards { get; set; }
    public IReadOnlyList<YoutubeLink> YoutubeLinks { get; set; }
    

}