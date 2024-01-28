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
    
    public static implicit operator Element(Task<Func<Element>> task)
    {
        return new ElementAsTaskFunc(task);
    }
    
    public static implicit operator Element(Task<Func<Task<Element>>> task)
    {
        return new ElementAsTaskFuncDouble(task);
    }
    
    public static implicit operator Element(Func<Element> func)
    {
        return ToElement(func);
    }
    
    public static implicit operator Element(Func<Scope,Element> func)
    {
        return ToElement(func);
    }
    
    
    public static implicit operator Element(Func<Task<Element>> func)
    {
        return ToElement(func);
    }
    
    public static implicit operator Element(Func<Scope,Task<Element>> func)
    {
        return ToElement(func);
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
    
    public void Add(IEnumerable<Func<Element>> elements)
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
    
    public void Add(IEnumerable<Task<Func<Element>>> elements)
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

        if (func.Target is not null)
        {
            var targeType = func.Target.GetType();

            if (targeType.IsCompilerGenerated())
            {
                return new CompilerGeneratedClassComponent
                {
                    renderFunc = func,
                    
                    RenderMethodNameWithToken = func.Method.GetNameWithToken(),
                    
                    Scope = ReflectionHelper.FieldsToDictionaryOfCompilerGeneratedTypeInstance(func.Target),
                    
                    CompilerGeneratedType = targeType
                };
            }
        }

        return func.Invoke();
    }
    
    internal static Element ToElement(Func<Scope,Element> func)
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
                return new CompilerGeneratedClassComponent
                {
                    renderFuncWithScope = func,
                    
                    RenderMethodNameWithToken = func.Method.GetNameWithToken(),
                    
                    Scope = ReflectionHelper.FieldsToDictionaryOfCompilerGeneratedTypeInstance(func.Target),
                    
                    CompilerGeneratedType = targeType
                };
            }
        }
        
        return new CompilerGeneratedClassComponent
        {
            renderFuncWithScope = func
        };
    }
    
    internal static Element ToElement(Func<Task<Element>> func)
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
                return new CompilerGeneratedClassComponent
                {
                    IsRenderAsync = true,
                            
                    renderFuncAsync = func,
                    
                    RenderMethodNameWithToken = func.Method.GetNameWithToken(),
                    
                    Scope = ReflectionHelper.FieldsToDictionaryOfCompilerGeneratedTypeInstance(func.Target),
                    
                    CompilerGeneratedType = targeType
                };
            }
        }

        return new ElementAsTask(func());
    }
    
    internal static Element ToElement(Func<Scope,Task<Element>> func)
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
                return new CompilerGeneratedClassComponent
                {
                    IsRenderAsync = true,
                            
                    renderFuncAsyncWithScope = func,
                    
                    RenderMethodNameWithToken = func.Method.GetNameWithToken(),
                    
                    Scope = ReflectionHelper.FieldsToDictionaryOfCompilerGeneratedTypeInstance(func.Target),
                    
                    CompilerGeneratedType = targeType
                };
            }
        }

        return new CompilerGeneratedClassComponent
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
    public void Add(Task<Func<Element>> elementCreatorFunc)
    {
        if (elementCreatorFunc == null)
        {
            return;
        }
        
        Add(new ElementAsTaskFunc(elementCreatorFunc));
    }
    
    public void Add(Task<Func<Task<Element>>> elementCreatorFunc)
    {
        if (elementCreatorFunc == null)
        {
            return;
        }
        
        Add(new ElementAsTaskFuncDouble(elementCreatorFunc));
    }
    
    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to children.
    /// </summary>
    public void Add(Func<Task<Element>> elementCreatorFunc)
    {
        if (elementCreatorFunc == null)
        {
            return;
        }
        
        Add(elementCreatorFunc.Invoke());
    }
    
    public void Add<TElement>(Action<TElement> action) where TElement : Element
    {
        if (action == null)
        {
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

sealed class ElementAsTaskFunc : Element
{
    public readonly Task<Func<Element>> Value;

    public ElementAsTaskFunc(Task<Func<Element>> value)
    {
        Value = value;
    }
    
    internal List<IModifier> Modifiers;
}

sealed class ElementAsTaskFuncDouble : Element
{
    public readonly Task<Func<Task<Element>>> Value;

    public ElementAsTaskFuncDouble(Task<Func<Task<Element>>> value)
    {
        Value = value;
    }
    
    internal List<IModifier> Modifiers;
}



