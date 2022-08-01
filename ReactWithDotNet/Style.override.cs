using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

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


    [JsonIgnore]
    public string width_height
    {
        set
        {
            width  = value;
            height = value;
        }
    }
    
        
        
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

        var lines = File.ReadAllLines("D:\\A.txt");

        
        var sb = new StringBuilder();
        foreach (var name in lines)
        {
            sb.AppendLine("if(" + name + " != null )");
            sb.AppendLine("{");
            sb.AppendLine("sb.Append(" + '"' + getRealCssKey(name) + '"' + ");");
            sb.AppendLine("sb.Append(\": \"" + ");");
            sb.AppendLine("sb.Append(" + name+");");
            sb.AppendLine("sb.Append(\";\"" + ");");
            sb.AppendLine("}");
            sb.AppendLine("");
        }
        
        
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
                        words += " " + ch.ToString().ToLower(new CultureInfo("en-US"));
                    }

                }
                return words.Split(" ".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
            }

            return Array.Empty<string>();
        }
    }


    public string ToCss2()
    {
        var sb = new StringBuilder();

        
        
        return sb.ToString();
    }
    #endregion
}