using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Style
{
    internal Style _active;

    internal Style _after;

    internal Style _before;

    internal Style _focus;
    internal Style _hover;
    
    internal Style _focusVisible;

    internal List<MediaQuery> _mediaQueries;

    [JsonIgnore]
    public Style active
    {
        get { return _active ??= new Style(); }
    }

    [JsonIgnore]
    public Style after
    {
        get { return _after ??= new Style(); }
    }

    [JsonIgnore]
    public Style before
    {
        get { return _before ??= new Style(); }
    }

    [JsonIgnore]
    public Style focus
    {
        get { return _focus ??= new Style(); }
    }
    
    [JsonIgnore]
    public Style focusVisible
    {
        get { return _focusVisible ??= new Style(); }
    }

    [JsonIgnore]
    public Style hover
    {
        get { return _hover ??= new Style(); }
    }

    [JsonIgnore]
    public List<MediaQuery> MediaQueries
    {
        get { return _mediaQueries ??= []; }
    }
}

/// <summary>
///     Example:
///     <br />
///     new MediaQuery("only screen and (max-width: 600px)", new Style { width:"5px" }
/// </summary>
public sealed class MediaQuery
{
    internal readonly string Query;
    internal readonly Style Style;

    /// <summary>
    ///     Example:
    ///     <br />
    ///     new MediaQuery("only screen and (max-width: 600px)", new Style { width:"5px" }
    /// </summary>
    public MediaQuery(string query, Style style)
    {
        Query = query;
        Style = style;
    }
}