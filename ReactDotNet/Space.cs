namespace ReactDotNet
{
    public class Space : Element
    {
        public double? Height
        {
            set
            {
                if (value.HasValue)
                {
                    style.Height = value.Value.AsPixel();
                }
                else
                {
                    style.Height = null;
                }
            }
        }

        public double? Width
        {
            set
            {
                if (value.HasValue)
                {
                    style.Width = value.Value.AsPixel();
                }
                else
                {
                    style.Width = null;
                }
            }
        }

        public override ReactElement ToReactElement()
        {
            return new ReactElement { Tag = "div", Props = this.CollectReactAttributedProperties() };
        }
    }
}