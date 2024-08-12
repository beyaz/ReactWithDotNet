namespace ReactWithDotNet.WebSite.Components;

class Deneme : PureComponent
{
    public string aaa { get; set; }

    #region Designer Code [Do not edit manually]

    protected override DesignerCode Designer => new()
    {
        { [0], [Padding(22), Background("yellow"), WidthFitContent] },
        { [0, 0], [Size(200), Background("green"), Hover(Background("blue"), BorderRadius(8))] }
    };

    #endregion Designer Code [Do not edit manually]

    protected override Element render()
    {
        return new div
        {
            new div()
        };
    }
}