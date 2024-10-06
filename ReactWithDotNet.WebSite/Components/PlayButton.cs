using ReactWithDotNet.ThirdPartyLibraries.React_Player;

namespace ReactWithDotNet.WebSite.Components;

sealed class PlayButton : Component<PlayButton.State>
{
    public string Label { get; init; } = "Play tutorial (2 min)";

    public (double W, double H) VideoSize { get; init; } = (640, 360);

    public string VideoUrl { get; init; }

    protected override Element render()
    {
        if (state.IsPlayerOpen)
        {
            return new VideoPlayer
            {
                Size     = VideoSize,
                VideoUrl = VideoUrl,
                Closed = () =>
                {
                    state = state with { IsPlayerOpen = false };

                    return Task.CompletedTask;
                }
            };
        }

        Style style =
        [
            Color(Gray500),
            Gap(4),
            UserSelect(none),
            Hover(Color(Gray700))
        ];

        return new FlexRowCentered(Padding(3, 10), SizeFitContent, BorderRadius(3), style)
        {
            new IconPlay { Size = 25 },

            Label,

            OnClick(_ =>
            {
                state = state with { IsPlayerOpen = true };

                return Task.CompletedTask;
            })
        };
    }

    class Backdrop : PureComponent
    {
        protected override Element render()
        {
            return new FlexRowCentered(PositionFixed, Inset0, Background("#0003"), Zindex5, Transition(Opacity, 0.3, "ease"))
            {
                children
            };
        }
    }

    class VideoPlayer : Component<VideoPlayer.VideoPlayerState>
    {
        [CustomEvent]
        public Func<Task> Closed { get; init; }

        public (double W, double H) Size { get; init; } = (640, 360);

        public required string VideoUrl { get; init; }

        protected override Element render()
        {
            if (state.IsClosed)
            {
                return null;
            }

            var w = Size.W;
            var h = Size.H;

            List<StyleModifier> style = [];

            for (var i = 300; i < 700; i += 50)
            {
                style.Add(WhenMediaSizeGreaterThan(i, Width(i - 50)));
                style.Add(WhenMediaSizeGreaterThan(i, Height(h * i / w)));
            }

            return new Backdrop
            {
                OnClick(_ =>
                {
                    state.IsClosed = true;

                    DispatchEvent(Closed, []);

                    return Task.CompletedTask;
                }),

                new FlexRowCentered(WidthFitContent, HeightAuto, Padding(16), Background(White), BorderRadius(8))
                {
                    new div(style)
                    {
                        new ReactPlayer
                        {
                            url   = VideoUrl,
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

        internal class VideoPlayerState
        {
            public bool IsClosed { get; set; }
        }
    }

    internal record State
    {
        public bool IsPlayerOpen { get; init; }
    }
}