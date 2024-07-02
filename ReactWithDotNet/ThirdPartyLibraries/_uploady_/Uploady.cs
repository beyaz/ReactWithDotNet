namespace ReactWithDotNet.ThirdPartyLibraries._uploady_;

public sealed class Uploady  : ThirdPartyReactComponent
{
    [ReactProp]
    public bool multiple { get; set; }

    [ReactProp]
    public bool grouped { get; set; }
    
    [ReactProp]
    public int? maxGroupSize  { get; set; }
    
    [ReactProp]
    public string method  { get; set; }
    
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic destination { get; } = new ExpandoObject();
}
public sealed class UploadButton  : ThirdPartyReactComponent
{
   
}
public sealed class UploadProgress  : ThirdPartyReactComponent
{
   
}


