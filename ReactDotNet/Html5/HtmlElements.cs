using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using static ReactDotNet.Mixin;

namespace ReactDotNet.Html5;

[Serializable]
public sealed class MarginThickness
{
    readonly Style style;

    public MarginThickness(Style style)
    {
        this.style = style;
    }

    public double? Left
    {
        set
        {
            if (value.HasValue)
            {
                style.marginLeft = px(value.Value);
            }
            else
            {
                style.marginLeft = null;
            }
        }
    }

    public double? Top
    {
        set
        {
            if (value.HasValue)
            {
                style.marginTop = px(value.Value);
            }
            else
            {
                style.marginTop = null;
            }
        }
    }

    public double? Right
    {
        set
        {
            if (value.HasValue)
            {
                style.marginRight = px(value.Value);
            }
            else
            {
                style.marginRight = null;
            }
        }
    }

    public double? Bottom
    {
        set
        {
            if (value.HasValue)
            {
                style.marginBottom = px(value.Value);
            }
            else
            {
                style.marginBottom = null;
            }
        }
    }
}

[Serializable]
public sealed class PaddingThickness
{
    readonly Style style;

    public PaddingThickness(Style style)
    {
        this.style = style;
    }

    public double? Left
    {
        set
        {
            if (value.HasValue)
            {
                style.paddingLeft = px(value.Value);
            }
            else
            {
                style.paddingLeft = null;
            }
        }
    }

    public double? Top
    {
        set
        {
            if (value.HasValue)
            {
                style.paddingTop = px(value.Value);
            }
            else
            {
                style.paddingTop = null;
            }
        }
    }

    public double? Right
    {
        set
        {
            if (value.HasValue)
            {
                style.paddingRight = px(value.Value);
            }
            else
            {
                style.paddingRight = null;
            }
        }
    }

    public double? Bottom
    {
        set
        {
            if (value.HasValue)
            {
                style.paddingBottom = px(value.Value);
            }
            else
            {
                style.paddingBottom = null;
            }
        }
    }
}



public class button : HtmlElement
{
    public button()
    {
    }

    public button(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}





public class label : HtmlElement
{
    public label()
    {
    }

    public label(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string htmlFor { get; set; }
}

public class input : HtmlElement
{
    public input()
    {
    }

    public input(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string type { get; set; }

    [React]
    public string name { get; set; }

    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public bool @checked { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }
}

public class i : HtmlElement
{
    public i()
    {
    }

    public i(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}

public class div : HtmlElement
{

    [JsonPropertyName("$type")]
    public override string Type => nameof(div);

    public div()
    {
    }

    public div(string innerText)
    {
        this.innerText = innerText;
    }

    public div(IEnumerable<Element> children)
    {
        this.children.AddRange(children);
    }

    public div(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}

public class p : HtmlElement
{
    public p()
    {
    }

    public p(string innerText)
    {
        this.innerText = innerText;
    }

    public p(IEnumerable<Element> children)
    {
        this.children.AddRange(children);
    }

    public p(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}
public class pre : HtmlElement
{
    public pre()
    {
    }

    public pre(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}

public class h5 : HtmlElement
{
    public h5()
    {
    }

    public h5(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}

public class h4 : HtmlElement
{
    public h4()
    {
    }

    public h4(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    public h4(string innerText)
    {
        this.innerText = innerText;
    }
}

public class h3 : HtmlElement
{
    public h3()
    {
    }

    public h3(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    public h3(string innerText)
    {
        this.innerText = innerText;
    }
}

public class h2 : HtmlElement
{
  

    public h2()
    {
    }

    public h2(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    public h2(string innerText)
    {
        this.innerText = innerText;
    }
}

public class h1 : HtmlElement
{
    public h1()
    {
    }

    public h1(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    public h1(string innerText)
    {
        this.innerText = innerText;
    }
}

public class a : HtmlElement
{
    public a()
    {
    }

    public a(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string href { get; set; }
}

public class li : HtmlElement
{
    public li()
    {
    }

    public li(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}
public class img : HtmlElement
{
    public img()
    {
    }

    public img(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string src { get; set; }

    [React]
    public string alt { get; set; }

    [React]
    public new int width { get; set; }

    [React]
    public new int height { get; set; }
}

public class HPanel : div
{
    public HPanel(params ElementModifier[] modifiers) : base(modifiers)
    {
        InitializeStyle(style);
    }
    
    static void InitializeStyle(Style style)
    {
        style.display       = Display.flex;
        style.flexDirection = FlexDirection.row;
        style.alignItems    = AlignItems.stretch;
        style.width         = "100%";

    }
    public HPanel()
    {
        InitializeStyle(style);
    }

    protected internal override void BeforeSerialize()
    {
        base.BeforeSerialize();

        if (children.Any(x => x.gravity.HasValue))
        {
            foreach (var child in children)
            {
                child.style.width = (GravityCalculator.CalculateGravity(child, children) * 100).AsPercent();
            }
        }
    }
}

public sealed class VPanel : div
{
    public VPanel()
    {
        style.display       = Display.flex;
        style.flexDirection = FlexDirection.column;
        style.alignItems    = AlignItems.stretch;
        style.height        = "100%";
    }

    public VPanel(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    protected internal override void BeforeSerialize()
    {
        base.BeforeSerialize();

        if (children.Any(x => x.gravity.HasValue))
        {
            foreach (var child in children)
            {
                child.style.height = (GravityCalculator.CalculateGravity(child, children) * 100).AsPercent();
            }
        }
    }
}

static class GravityCalculator
{
    public static double CalculateGravity(Element htmlElement, IReadOnlyList<Element> siblings)
    {
        var total = siblings.Sum(x => x.gravity ?? 1);

        var gravity = htmlElement.gravity ?? 1;

        return (double) gravity / total;
    }
}

public class svg : HtmlElement
{
    public svg(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string xmlns { get; set; } = "http://www.w3.org/2000/svg";

    [React]
    public string viewBox { get; set; }
}

public class path : HtmlElement
{
    public path(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string d { get; set; }

    [React]
    public string fill { get; set; }
}

public class rect : HtmlElement
{
    public rect(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public int y { get; set; }
}

public class nav : HtmlElement
{
    public nav()
    {
    }

    public nav(params ElementModifier[] modifiers) : base(modifiers)
    {
    }
}