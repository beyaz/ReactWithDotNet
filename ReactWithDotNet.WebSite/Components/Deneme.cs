namespace ReactWithDotNet.WebSite.Components;

class Deneme : PureComponent
{
    #region Designer Code [Do not edit manually]

    protected override DesignerCode Designer => new()
    {
        { [0], [Padding(22), Background("yellow"), WidthFitContent] },
        { [0, 0], [Size(200), Background("green")] }
    };

    #endregion

    protected override Element render()
    {
        return new div
        {
            new div()
        };
    }
}