using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet.Html5;

partial class Style
{
    #region margin
    [JsonIgnore]
    public string marginLeftRight
    {
        set
        {
            marginLeft  = value;
            marginRight = value;
        }
    }


    [JsonIgnore]
    public string marginLeftTop
    {
        set
        {
            marginLeft = value;
            marginTop  = value;
        }
    }

    [JsonIgnore]
    public string marginLeftBottom
    {
        set
        {
            marginLeft   = value;
            marginBottom = value;
        }
    }
    
    [JsonIgnore]
    public string marginTopBottom
    {
        set
        {
            marginTop    = value;
            marginBottom = value;
        }
    }

    [JsonIgnore]
    public string marginTopRight
    {
        set
        {
            marginTop    = value;
            marginRight = value;
        }
    }

    #endregion

    #region Padding
    [JsonIgnore]
    public string paddingLeftRight
    {
        set
        {
            paddingLeft  = value;
            paddingRight = value;
        }
    }


    [JsonIgnore]
    public string paddingLeftTop
    {
        set
        {
            paddingLeft = value;
            paddingTop  = value;
        }
    }


    [JsonIgnore]
    public string paddingLeftBottom
    {
        set
        {
            paddingLeft   = value;
            paddingBottom = value;
        }
    }

    [JsonIgnore]
    public string paddingTopBottom
    {
        set
        {
            paddingTop    = value;
            paddingBottom = value;
        }
    }

    [JsonIgnore]
    public string paddingTopRight
    {
        set
        {
            paddingTop   = value;
            paddingRight = value;
        }
    }


    public string ToCss()
    {
        var json = JsonSerializer.Serialize(this, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

        var map = JsonSerializer.Deserialize<Dictionary<string,string>>(json);

        if (map.Count == 0)
        {
            return null;
        }

        return string.Join(" ", map.Select(kvp => $"{getRealCssKey(kvp.Key)}: {kvp.Value};"));
        

        static string getRealCssKey(string key)
        {
            return string.Join("-", SplitByCamelCase(key).Select(x => x.ToLower()));
        }
        static string[] SplitByCamelCase(string stringtosplit)
        {
            string words = string.Empty;
            
            if (!string.IsNullOrEmpty(stringtosplit))
            {
                foreach (char ch in stringtosplit)
                {
                    if (char.IsLower(ch))
                    {
                        words += ch.ToString();
                    }
                    else
                    {
                        words += " " + ch.ToString();
                    }

                }
                return words.Split(" ".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
            }

            return Array.Empty<string>();
        }
    }
    #endregion
}