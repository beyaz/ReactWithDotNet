namespace ReactWithDotNet.WebSite.Pages;

sealed class PageMileStones : PureComponent
{
    protected override Element render()
    {
        return new FlexColumn
        {
            new div
            {
                new h4{ "Core Concept" },
                new Progressbar{ Value = 95} + Size(300,50)
            }
        };

    }

    class Progressbar : PureComponent
    {
        public int Value { get; init; } = 10;
        
        protected override Element render()
        {
            return new div(SizeFull, BorderRadius(8), Border(1, solid, Gray300))
            {
                new div(Width(Value, 100), Background(Gray400), HeightFull)
                {

                }
            };
        }
    }
}