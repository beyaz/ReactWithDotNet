using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Style
{
    internal Style _hover;

    [JsonIgnore]
    public Style hover
    {
        get
        {
            if (_hover == null)
            {
                _hover = new Style();
            }
            return _hover;
        }
    }

    internal Style _before;

    [JsonIgnore]
    public Style before
    {
        get
        {
            if (_before == null)
            {
                _before = new Style();
            }
            return _before;
        }
    }


    internal Style _after;

    [JsonIgnore]
    public Style after
    {
        get
        {
            if (_after == null)
            {
                _after = new Style();
            }
            return _after;
        }
    }

    internal Style _active;

    [JsonIgnore]
    public Style active
    {
        get
        {
            if (_active == null)
            {
                _active = new Style();
            }
            return _active;
        }
    }

    internal Style _focus;

    [JsonIgnore]
    public Style focus
    {
        get
        {
            if (_focus == null)
            {
                _focus = new Style();
            }
            return _focus;
        }
    }

    internal List<MediaQuery> _mediaQueries;

    [JsonIgnore]
    public List<MediaQuery> MediaQueries
    {
        get
        {
            if (_mediaQueries == null)
            {
                _mediaQueries = new List<MediaQuery>();
            }
            return _mediaQueries;
        }
    }
}

/// <summary>
/// Example:
/// <br/>
/// new MediaQuery("only screen and (max-width: 600px)", new Style { width:"5px" }
/// </summary>
public sealed class MediaQuery
{
    internal readonly string query;
    internal readonly Style style;

    /// <summary>
    /// Example:
    /// <br/>
    /// new MediaQuery("only screen and (max-width: 600px)", new Style { width:"5px" }
    /// </summary>
    public MediaQuery(string query, Style style)
    {
        this.query = query;
        this.style = style;
    }
}