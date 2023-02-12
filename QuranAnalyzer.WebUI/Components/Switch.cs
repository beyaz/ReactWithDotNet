namespace QuranAnalyzer.WebUI.Components;

class SwitchWithLabel : ReactPureComponent
{
    public bool IsDisabled { get; set; }

    public string Label { get; set; }

    public double? LabelMaxWidth { get; set; }

    public bool Value { get; set; }

    public Action<ChangeEvent> ValueChange { get; set; }

    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter, Gap(5), FlexWrap, AlignContentFlexStart)
        {
            new Switch
            {
                IsChecked = Value, ValueChange = ValueChange, IsDisabled = IsDisabled
            },
            new label { Text(Label), When(LabelMaxWidth.HasValue, MaxWidth(LabelMaxWidth.GetValueOrDefault())) }
        };
    }
}

public class Switch : ReactPureComponent
{
    public bool IsChecked { get; set; }

    public bool? IsDisabled { get; set; }

    public Action<ChangeEvent> ValueChange { get; set; }

    protected override Element render()
    {
        return new ReactWithDotNet.Libraries.mui.material.Switch
        {
            @checked = IsChecked,
            onChange = ValueChange,
            disabled = IsDisabled,
            value    = (!IsChecked).ToString()
        };
    }
}