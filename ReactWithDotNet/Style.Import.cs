using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Style
{
    [JsonIgnore]
    public bool IsEmpty => isEmpty(this);

    public string this[string key]
    {
        get
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Get(key.Replace("-", string.Empty));
        }
        set
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            setByName(this, key.Replace("-", string.Empty), value);
        }
    }

    public void Import(Style newStyle)
    {
        if (newStyle == null)
        {
            return;
        }

        transfer(newStyle, this);

        if (newStyle._hover is not null)
        {
            hover.Import(newStyle._hover);
        }

        if (newStyle._before is not null)
        {
            before.Import(newStyle._before);
        }

        if (newStyle._after is not null)
        {
            after.Import(newStyle._after);
        }

        if (newStyle._active is not null)
        {
            active.Import(newStyle._active);
        }

        if (newStyle._focus is not null)
        {
            focus.Import(newStyle._focus);
        }

        if (newStyle._mediaQueries is not null && newStyle._mediaQueries.Count > 0)
        {
            foreach (var mediaQuery in newStyle._mediaQueries)
            {
                MediaQueries.Add(new MediaQuery(mediaQuery.Query, mediaQuery.Style.Clone()));
            }
        }
    }
}