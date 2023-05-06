using System.Collections;

namespace ReactWithDotNet.Libraries.PrimeReact;

public class Column  : ElementBase
{
    [ReactProp]
    public string field { get; set; }

    [ReactProp]
    public string header { get; set; }
}

public class DataTable : ElementBase
{
    [ReactProp]
    public bool? scrollable  { get; set; }

    [ReactProp]
    public string scrollHeight { get; set; }

    [ReactProp]
    public IEnumerable value { get; set; }
    
}