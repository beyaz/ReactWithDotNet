using System.Collections;

namespace ReactWithDotNet.Libraries.PrimeReact;

public class Column  : ElementBase
{
    [React]
    public string field { get; set; }

    [React]
    public string header { get; set; }
}

public class DataTable : ElementBase
{
    [React]
    public bool? scrollable  { get; set; }

    [React]
    public string scrollHeight { get; set; }

    [React]
    public IEnumerable value { get; set; }
    
}