namespace ReactWithDotNet;
public sealed class div : HtmlElement
{
    public div() { }

    public div(string innerText) : base(innerText) {  }

    public div(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator div(string text) => new() { text = text };
}

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
public sealed class p : HtmlElement
{
    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p() { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public static implicit operator p(string text) => new() { text = text };
}

public sealed class h1 : HtmlElement
{
    public h1() { }

    public h1(string innerText) : base(innerText) {  }

    public h1(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator h1(string text) => new() { text = text };
}

public sealed class h2 : HtmlElement
{
    public h2() { }

    public h2(string innerText) : base(innerText) {  }

    public h2(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator h2(string text) => new() { text = text };
}

public sealed class h3 : HtmlElement
{
    public h3() { }

    public h3(string innerText) : base(innerText) {  }

    public h3(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator h3(string text) => new() { text = text };
}

public sealed class h4 : HtmlElement
{
    public h4() { }

    public h4(string innerText) : base(innerText) {  }

    public h4(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator h4(string text) => new() { text = text };
}

public sealed class h5 : HtmlElement
{
    public h5() { }

    public h5(string innerText) : base(innerText) {  }

    public h5(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator h5(string text) => new() { text = text };
}

public sealed class h6 : HtmlElement
{
    public h6() { }

    public h6(string innerText) : base(innerText) {  }

    public h6(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator h6(string text) => new() { text = text };
}

public sealed class header : HtmlElement
{
    public header() { }

    public header(string innerText) : base(innerText) {  }

    public header(params IModifier[] modifiers) : base(modifiers) { }

    public static implicit operator header(string text) => new() { text = text };
}

