namespace ReactWithDotNet.Libraries.react_datepicker;

/// <summary>
///     https://github.com/Hacker0x01/react-datepicker/blob/master/src/index.jsx
/// </summary>
public sealed class DatePicker : ThirdPartyReactComponent
{
    [ReactProp]
    public Action<DateTime> onChange { get; set; }

    [ReactProp]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ConvertDotnetSerializedStringDateToJsDate")]
    public DateTime? selected { get; set; }

    [ReactProp]
    public bool? inline { get; set; }

    [ReactProp]
    public bool? selectsStart { get; set; }

    [ReactProp]
    public bool? selectsEnd { get; set; }
}