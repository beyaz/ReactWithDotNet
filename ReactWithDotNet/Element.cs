using System.Collections;
using System.Text;

namespace ReactWithDotNet;

/// <summary>
///     The element
/// </summary>
public abstract class Element : IEnumerable<Element>, IEnumerable<IModifier>
{
    internal ElementCollection _children;

    /// <summary>
    ///     The children
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public ElementCollection children
    {
        get
        {
            _children ??= new ElementCollection();

            return _children;
        }
    }
    
    public bool HasChildren=>_children == null || _children.Count == 0;

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    internal string key { get; set; }

    public static Element operator +(Element element, StyleModifier styleModifier)
    {
        ModifyHelper.ProcessModifier(element, styleModifier);

        return element;
    }

    public static Element operator +(Element element, StyleModifier[] styleModifiers)
    {
        element.Apply(styleModifiers);

        return element;
    }

    public static Element operator +(Element element, Style style)
    {
        ModifyHelper.ProcessModifier(element, CreateStyleModifier(s => s?.Import(style)));

        return element;
    }

    public static Element operator +(Element element, IModifier modifier)
    {
        ModifyHelper.ProcessModifier(element, modifier);

        return element;
    }

    public static implicit operator Element(string text)
    {
        return new HtmlTextNode { text = text };
    }
    
    public static implicit operator Element(StringBuilder stringBuilder)
    {
        return new HtmlTextNode { stringBuilder = stringBuilder };
    }
    
    public static implicit operator Element(int number)
    {
        return new HtmlTextNode { text = number.ToString() };
    }
    public static implicit operator Element(int? number)
    {
        return new HtmlTextNode { text = number?.ToString() };
    }
    public static implicit operator Element(double? number)
    {
        return new HtmlTextNode { text = number?.ToString() };
    }
    
    public static implicit operator Element(Task<Element> task)
    {
        return new ElementAsTask(task);
    }
    
    public static implicit operator Element(FC func)
    {
        return ToElement(func);
    }
    
    public static implicit operator Element(Task<FC> func)
    {
        return new ElementAsTaskFC(func);
    }


    // TODO: Check usage maybe should remove
    public static implicit operator Element(Func<Task<FC>> func)
    {
        return ToElement(func);
    }


    public void Add(IEnumerable<FC> elements)
    {
        if (elements is null)
        {
            return;
        }
        
        foreach (var item in elements)
        {
            Add(item);
        }
    }
    
    /// <summary>
    ///     Adds the specified element.
    /// </summary>
    public void Add(Element element)
    {
        children.Add(element);
    }
    
    public void Add(params IModifier[] modifiers)
    {
        this.Apply(modifiers);
    }
    
    public void Add(StyleModifier[] modifiers)
    {
        this.Apply(modifiers);
    }

    public void Add(IEnumerable<Element> elements)
    {
        if (elements is not null)
        {
            children.AddRange(elements);
        }
    }
    
    public void Add(Func<IEnumerable<Element>> elements)
    {
        if (elements is not null)
        {
            Add(elements());
        }
    }

    public void Add(IModifier modifier)
    {
        ModifyHelper.ProcessModifier(this, modifier);
    }

    internal static Element ToElement(Func<Element> func)
    {
        if (func == null)
        {
            return null;
        }
        
        return func.Invoke();
    }
    
    internal static Element ToElement(FC func)
    {
        if (func == null)
        {
            return null;
        }

        if (func.Target is not null)
        {
            var targeType = func.Target.GetType();

            if (targeType.IsCompilerGenerated())
            {
                return new FunctionalComponent
                {
                    renderFuncWithScope = func,
                    
                    RenderMethodNameWithToken = func.Method.GetNameWithToken(),
                    
                    CompilerGeneratedType = targeType
                };
            }
        }
        
        return new FunctionalComponent
        {
            renderFuncWithScope = func
        };
    }
    
    internal static Element ToElement(Func<Task<FC>> func)
    {
        if (func == null)
        {
            return null;
        }

        if (func.Target is not null)
        {
            var targeType = func.Target.GetType();

            if (targeType.IsCompilerGenerated())
            {
                return new FunctionalComponent
                {
                    IsRenderAsync = true,
                            
                    renderFuncAsyncWithScope = func,
                    
                    RenderMethodNameWithToken = func.Method.GetNameWithToken(),
                    
                    CompilerGeneratedType = targeType
                };
            }
        }

        return new FunctionalComponent
        {
            IsRenderAsync = true,

            renderFuncAsyncWithScope = func
        };
    }
    
    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to children.
    /// </summary>
    public void Add(Func<Element> elementCreatorFunc)
    {
        Add(ToElement(elementCreatorFunc));
    }
    
    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to children.
    /// </summary>
    public void Add(Func<Task<Element>> elementCreatorFunc)
    {
        if (elementCreatorFunc == null)
        {
            Add((Element)null);
            return;
        }
        
        Add(elementCreatorFunc.Invoke());
    }
    
    public void Add<TElement>(Action<TElement> action) where TElement : Element
    {
        if (action == null)
        {
            Add((Element)null);
            return;
        }
        
        action.Invoke((TElement)this);
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    public IEnumerator<Element> GetEnumerator()
    {
        return children.GetEnumerator();
    }

    public string ToHtml()
    {
        return CalculateComponentHtmlText(new()
        {
            Component = new ToStringHandlerComponent(this)
        }).GetAwaiter().GetResult().ToString();
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return children.GetEnumerator();
    }

    IEnumerator<IModifier> IEnumerable<IModifier>.GetEnumerator()
    {
        return Enumerable.Empty<IModifier>().GetEnumerator();
    }

    class ToStringHandlerComponent : PureComponent
    {
        readonly Element _element;

        public ToStringHandlerComponent(Element element)
        {
            _element = element;
        }

        protected override Element render()
        {
            return _element;
        }
    }
}

sealed class FakeChild : Element
{
    public int Index { get; set; }
}

sealed class ElementAsTask : Element
{
    public readonly Task<Element> Value;

    public ElementAsTask(Task<Element> value)
    {
        Value = value;
    }
    
    internal List<IModifier> Modifiers;
}

sealed class ElementAsTaskFC : Element
{
    public readonly Task<FC> Value;

    public ElementAsTaskFC(Task<FC> value)
    {
        Value = value;
    }
    
    internal List<IModifier> Modifiers;
}


