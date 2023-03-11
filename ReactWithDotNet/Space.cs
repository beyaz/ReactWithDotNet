namespace ReactWithDotNet;

class Space : HtmlElement
{
    public override string Type => nameof(div);

    public string spaceValue { get; set; }
    
    public Space(double valueInPx)
    {
        spaceValue = $"{valueInPx}px";
    }

    public Space(string value)
    {
        spaceValue = value;
    }

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
