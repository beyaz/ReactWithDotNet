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

}