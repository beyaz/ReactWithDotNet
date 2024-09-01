using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite.Content;




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

}