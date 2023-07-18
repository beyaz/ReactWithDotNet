
using System.Collections;
namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public abstract partial  class Autocomplete : ElementBase
{
    //[ReactProp]
    //public dynamic options { get; set; }
    
    //[ReactProp]
    //public string value { get; set; }
    
    //[ReactProp]
    //[ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete::calculate_onChange_arguments")]
    //public Action<ChangeEvent,string> onChange { get; set; }
}


public interface IAutocompleteOption
{
    public string label { get; set; }
}

[ReactRealType(typeof(Autocomplete))]
public class Autocomplete<TOption> : Autocomplete where TOption : IAutocompleteOption, new()
{
    [ReactProp]
    public IEnumerable<TOption> options { get; set; }
    
    [ReactProp]
    public TOption value { get; set; }
    
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete::calculate_onChange_arguments")]
    public Action<ChangeEvent,TOption> onChange { get; set; }
}
