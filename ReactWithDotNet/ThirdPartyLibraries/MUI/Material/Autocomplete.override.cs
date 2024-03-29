namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public abstract partial class Autocomplete : ElementBase;

[ReactRealType(typeof(Autocomplete))]
public class Autocomplete<TOption> : Autocomplete where TOption : class
{
    [ReactProp]
    public Func<TOption, string> getOptionLabel { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete::calculate_onChange_arguments")]
    public Func<ChangeEvent, TOption,Task> onChange { get; set; }

    [ReactProp]
    public IEnumerable<TOption> options { get; set; }

    [ReactProp]
    public TOption value { get; set; }
    
    [ReactProp]
    public bool? freeSolo { get; set; }

    internal static (bool needToExport, object value)
        GetPropertyValueForSerializeToClient(object component, string propertyName)
    {
        var instance = (Autocomplete<TOption>)component;

        if (propertyName == nameof(getOptionLabel))
        {
            return (true, instance.options.Select(x => new { option = x, label = instance.getOptionLabel(x) }).ToList());
        }

        return default;
    }
}