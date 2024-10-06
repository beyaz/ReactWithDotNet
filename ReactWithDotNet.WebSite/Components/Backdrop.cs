using ReactWithDotNet.ThirdPartyLibraries.React_Player;

namespace ReactWithDotNet.WebSite.Components;

sealed class Backdrop : PureComponent
{
    protected override Element render()
    {
        return new FlexRowCentered(PositionFixed, Inset0, Background("#0003"), Zindex5,Transition(Opacity,0.3,"ease"))
        {
            children
        };
    }
}

class VideoPlayer : Component<VideoPlayer.State>
{
    
    internal class State
    {
        public bool IsClosed { get; set; }
    }
    
    public required string Video { get; init; }

    public double W { get; init; } = 640;

    public double H { get; init; } = 360;
    
    protected override Element render()
    {
        var url = Video;
        
        if (DesignMode)
        {
            url = "https://uploads.codesandbox.io/uploads/user/fb7bd72f-ef17-4810-9e14-ca854fb0f56e/9GBo-mountain-video.mp4";
        }

        if (state.IsClosed)
        {
            return null;
        }

        var w = W;
        var h = H;

        List<StyleModifier> style = [];

        for (var i = 300; i < 700; i+=50)
        {
            style.Add(WhenMediaSizeGreaterThan(i, Width(i-50)));
            style.Add(WhenMediaSizeGreaterThan(i, Height((h*i/w))));
        }
        
        return new Backdrop
        {
            OnClick(_ =>
            {
                state.IsClosed = true;

                return Task.CompletedTask;
            }),

          
            new FlexRowCentered(WidthFitContent, HeightAuto, Padding(16), Background(White), BorderRadius(8))
            {
                new div(style)
                {
                    new ReactPlayer
                    {
                        url = url,
                        style = { BorderRadius(5) },

                        width       = "100%",
                       height      = "100%",
                        volume      = 0,
                        controls    = true,
                        playsinline = true
                    }
                }
            }
        };
    }
}