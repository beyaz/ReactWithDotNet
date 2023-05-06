namespace ReactWithDotNet;

sealed class Space : HtmlElement
{
    public Space(double valueInPx)
    {
        spaceValue = $"{valueInPx}px";
    }

    public Space(string value)
    {
        spaceValue = value;
    }

    public string spaceValue { get; set; }
    public override string Type => nameof(div);

    internal override void BeforeSerialize(HtmlElement parent)
    {
        if (parent == null)
        {
            throw new Exception("Space element parent cannot be null.");
        }

        if (parent.style.display != "flex")
        {
            throw new Exception("Space element parent style.display should be 'flex'.");
        }

        if (parent.style.flexDirection == "column")
        {
            style.height = spaceValue;
            return;
        }

        style.width = spaceValue;
    }
}