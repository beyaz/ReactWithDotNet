using System;
using System.Collections.Generic;
using System.Text;

namespace ReactWithDotNet.Libraries.ReactSuite;


public class ElementBase : ThirdPartyReactComponent
{
   
}

public sealed class AutoComplete : ElementBase
{
    [React]
    public IEnumerable<string> data { get; set; }

    [React]
    public string value { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.Libraries.ReactSuite.AutoComplete::OnChange")]
    public Action<string> onChange { get; set; }
}