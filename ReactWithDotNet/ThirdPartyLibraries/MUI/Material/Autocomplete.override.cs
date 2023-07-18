namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public abstract partial class Autocomplete : ElementBase;

public interface IAutocompleteOption
{
    public string label { get; set; }
}

[ReactRealType(typeof(Autocomplete))]
public class Autocomplete<TOption> : Autocomplete where TOption : IAutocompleteOption, new()
{
    [ReactProp]
    public Func<TOption, string> getOptionLabel { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete::calculate_onChange_arguments")]
    public Action<ChangeEvent, TOption> onChange { get; set; }

    [ReactProp]
    public IEnumerable<TOption> options { get; set; }

    [ReactProp]
    public TOption value { get; set; }

    internal static (bool isProcessed, object processedVersionOfPropertyValue) 
        GetPropertyValueForSerializeToClient(Autocomplete<TOption> instance, string propertyName)
    {
        if (propertyName == nameof(getOptionLabel))
        {
            return (true, instance.options.Select(instance.getOptionLabel).ToList());
        }

        return default;
    }
}