using ReactWithDotNet.ThirdPartyLibraries.React_Player;

namespace ReactWithDotNet.WebSite.Showcases;

public class OnClickPreviewDemo : ReactComponent
{
    public int Count  { get; set; }
    
    protected override Element render()
    {
        return new div
        {
            style =
            {
                width  = "640px",
                height = "360px"
            },
            children =
            {
                $"Count: {Count}",
                new Btn
                {
                    OnClick = OnClick
                }
            }
        };
    }

    void OnClick(MouseEvent obj)
    {
        Count++;
    }

    class Btn : ReactComponent
    {
        public Action<MouseEvent> OnClick;
        
        protected override Element render()
        {
            return new div
            {
                onClickPreview = OnClickPreview,
                onClick = OnClick,
                style =
                {
                    Padding(10),
                    Border(Solid(1, "green")),

                },
                children =
                {
                    "Click Me"+PreviewText
                }
            };
        }

        public string PreviewText { get; set; }
        
        void OnClickPreview()
        {
            PreviewText = "Preview";
        }
    }
}