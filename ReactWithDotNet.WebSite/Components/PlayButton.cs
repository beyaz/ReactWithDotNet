namespace ReactWithDotNet.WebSite.Components;

sealed class PlayButton : Component
{
    protected override Element render()
    {
        var elementCollection = children;

        if (DesignMode && elementCollection.Count == 0)
        {
            elementCollection.Add("Play tutorial (2 min)");
        }

        var style = new[]
        {
            Background(Green500),
            FontSize17,
            FontWeight500,
            Color(White),
            Gap(4),
            UserSelect(none),
            Hover(Background(Green600), Color(WhiteSmoke))
        };

        return new FlexRowCentered(Padding(10, 15), SizeFitContent, BorderRadius(3), style)
        {
            new IconPlay { Size = 30 },

            elementCollection
        };
    }
}