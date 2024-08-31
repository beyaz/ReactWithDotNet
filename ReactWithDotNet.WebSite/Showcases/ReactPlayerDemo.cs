using ReactWithDotNet.ThirdPartyLibraries.React_Player;

namespace ReactWithDotNet.WebSite.Showcases;

sealed class ReactPlayerDemo : PureComponent
{
    protected override Element render()
    {
        return new div(Size(640, 360))
        {
            new ReactPlayer
            {
                url = "https://uploads.codesandbox.io/uploads/user/fb7bd72f-ef17-4810-9e14-ca854fb0f56e/9GBo-mountain-video.mp4",

                width       = "100%",
                height      = "100%",
                volume      = 0,
                controls    = true,
                playsinline = true
            }
        };
    }
}