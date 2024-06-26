﻿using ReactWithDotNet.WebSite.Content;

namespace ReactWithDotNet.WebSite.Components;

class YoutubeCard : PureComponent
{
    public YoutubeLink Model { get; set; }

    protected override Element render()
    {
        return new a(DisplayFlexRow, Background("white"), BorderRadius(5))
        {
            Href($"https://www.youtube.com/watch?v={Model.YoutubeVideoId}"),
            TextDecorationNone,
            Color("black"),

            BoxShadow(-3, -3, 5,0, rgba(0, 0, 0, 0.12)),
            Hover(BoxShadow(0, 0, 20,0, rgba(0, 0, 0, 0.12))),
            


            new img
            {
                BorderTopLeftRadius(5),
                BorderBottomLeftRadius(5),
                Src($"https://i.ytimg.com/vi_webp/{Model.YoutubeVideoId}/maxresdefault.webp"), 
                Alt(Model.Title),
                Width(200),
                Height(160),
                ObjectFitFill
            },
            new FlexColumn(PaddingLeftRight(30))
            {
                
                new h3{ Model.Title },
                new span{ Model.Description, MaxWidth(250) }
            },

            // Play icon
            PositionRelative,
            new img { Src(Asset("play.svg")), Size(30), Right(10), Top(10), PositionAbsolute },
        };
    }
}