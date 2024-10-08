﻿namespace ReactWithDotNet.WebSite.Components;

sealed class IconPlay : PureComponent
{
    public int Size { get; init; }

    protected override Element render()
    {
        return new FlexRowCentered(Size(Size))
        {
            new svg(ViewBox(0, 0, 100, 100), Fill("#FFFFFF"), Size(Size))
            {
                new path(Fill("currentColor"))
                {
                    d = "M50,3.8C24.5,3.8,3.8,24.5,3.8,50S24.5,96.2,50,96.2S96.2,75.5,96.2,50S75.5,3.8,50,3.8z M71.2,53.3l-30.8,18  c-0.6,0.4-1.3,0.5-1.9,0.5c-0.6,0-1.3-0.1-1.9-0.5c-1.2-0.6-1.9-1.9-1.9-3.3V32c0-1.4,0.8-2.7,1.9-3.3c1.2-0.6,2.7-0.6,3.8,0  l30.8,18c1.2,0.6,1.9,1.9,1.9,3.3S72.3,52.7,71.2,53.3z"
                }
            }
        };
    }
}