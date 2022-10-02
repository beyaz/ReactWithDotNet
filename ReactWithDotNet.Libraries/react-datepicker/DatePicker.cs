namespace ReactWithDotNet.Libraries.react_datepicker;

/// <summary>
///     https://github.com/Hacker0x01/react-datepicker/blob/master/src/index.jsx
/// </summary>
public sealed class DatePicker : ThirdPartyReactComponent
{
    [React]
    public Action<DateTime> onChange { get; set; }

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ConvertDotnetSerializedStringDateToJsDate")]
    public DateTime? selected { get; set; }

    [React]
    public bool? inline { get; set; }

    [React]
    public bool? selectsStart { get; set; }

    [React]
    public bool? selectsEnd { get; set; }
}