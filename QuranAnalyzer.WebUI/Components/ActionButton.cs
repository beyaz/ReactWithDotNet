namespace QuranAnalyzer.WebUI.Components
{
    public class ActionButton : ReactComponent
    {
        public string Label { get; set; }
        
        protected override Element render()
        {
            return new FlexRowCentered
            {
                children = { Label },
                onClick  = ActionButtonOnClick,
                style =
                {
                    Color(BluePrimary),
                    Border($"1px solid {BluePrimary}"),
                    Background("transparent"),
                    BorderRadius(5),
                    Padding(10, 30),
                    CursorPointer
                }
            };
        }

        [ReactCustomEvent]
        public Action OnClick { get; set; }
        
        
        void ActionButtonOnClick(MouseEvent _)
        {
            DispatchEvent(()=>OnClick);
        }
    }
}
