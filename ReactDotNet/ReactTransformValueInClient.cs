using System;
using System.Text.Json.Serialization;

namespace ReactDotNet.Html5;

[Serializable]
public  class ReactTransformValueInClient
{
    [JsonPropertyName("$JsTransformFunctionLocation")]
    public string JsTransformFunctionLocation { get; set; }
    public object RawValue { get; set; }
}

[Serializable]
public sealed class ReactTransformValueInClient_Regex : ReactTransformValueInClient
{
    public static implicit operator ReactTransformValueInClient_Regex(string regex)
    {
        return new ReactTransformValueInClient_Regex { RawValue = regex, JsTransformFunctionLocation = "RegExp.prototype.constructor" };
    }
}