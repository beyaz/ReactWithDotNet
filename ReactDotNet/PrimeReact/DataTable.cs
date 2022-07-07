using System.Collections;
using System.Linq;
using ReactDotNet.Html5;

namespace ReactDotNet.PrimeReact;

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