using ReactWithDotNet.Libraries.PrimeReact;

namespace ReactWithDotNet.Libraries.UIDesigner.Components;

sealed class ValueInfo
{
    public string Label { get; set; }
    public string Value { get; set; }
}

class ValueInfoStringEditor : ReactComponent
{
    public ValueInfo Model { get; set; } = new();
    
    protected override Element render()
    {
        return new div(ClassName("field"))
        {
            new span(ClassName("p-float-label"))
            {
                new InputTextarea{id="textarea", valueBind = ()=>Model.Value, rows = 1},
                new label{htmlFor = "textarea", text = Model.Label}
            }
        };
    }
}


class ValueInfoListEditor : ReactComponent
{
    public IReadOnlyCollection<ValueInfo> ValueInfoList { get; set; } = new List<ValueInfo>();

    protected override Element render()
    {
        return new FlexColumn
        {
            new ValueInfoStringEditor
            {
                Model = new ValueInfo{Label = "Abc", Value = "tt"}
            }
        };
    }
}
