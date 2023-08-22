namespace ReactWithDotNet;

public sealed class ElementCollection : List<Element>
{
    internal ElementCollection()
    {
    }

    public void Add(ElementCollection elements)
    {
        if (elements is not null)
        {
            AddRange(elements);
        }
    }

    public new void Add(Element element)
    {
        base.Add(element);
    }

    public void Add(IEnumerable<Element> elements)
    {
        if (elements is not null)
        {
            AddRange(elements);
        }
    }

    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to list.
    /// </summary>
    public void Add(Func<Element> elementCreatorFunc)
    {
        base.Add(elementCreatorFunc?.Invoke());
    }
}