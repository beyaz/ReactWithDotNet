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


    class BtState
    {
        public string PreviewText { get; set; }
    }
    class Btn : ReactComponent<BtState>
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
                    "Click Me"+state.PreviewText
                }
            };
        }

     
        
        void OnClickPreview()
        {
            state.PreviewText = "Preview";
        }
    }
}