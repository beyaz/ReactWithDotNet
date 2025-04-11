namespace ReactWithDotNet.ThirdPartyLibraries.HeroUI;

public sealed class Popover : ElementBase
{
    [ReactProp]
    public string placement { get; set; }
    
    [ReactProp]
    public bool? showArrow { get; set; }
    
    [ReactProp]
    public bool? isOpen { get; set; }
    
    [ReactProp]
    public double? offset { get; set; }
    
    [ReactProp]
    public double? crossOffset { get; set; }
}

public sealed class PopoverTrigger : ElementBase
{

}

public sealed class PopoverContent : ElementBase
{
  
}

public sealed class Checkbox : ElementBase
{
    [ReactProp]
    public string value { get; set; }
    
    [ReactProp]
    public string name { get; set; }
    
    [ReactProp]
    public string size { get; set; }
    
    [ReactProp]
    public bool? isSelected { get; set; }
    
    [ReactProp]
    public bool? isReadOnly { get; set; }
    
    [ReactProp]
    public bool? isDisabled { get; set; }
}