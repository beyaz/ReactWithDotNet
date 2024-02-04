using System.Reflection;

namespace ReactWithDotNet;

/// <summary>
///     Functional component.
/// </summary>
public delegate Element FC(Scope cmp);

public interface Scope
{
    public Client Client { get; }

    public Func<Task> ComponentDidMount { set; }

    public Func<Task> Constructor { set; }

    public ReactContext Context { get; }
}

sealed class FunctionalComponent : Component, Scope
{
    internal object _target;

    internal Func<Task<FC>> renderFuncAsyncWithScope;

    internal FC renderFuncWithScope;

    public Type CompilerGeneratedType { get; set; }

    public Func<Task> ComponentDidMount { internal get; set; }

    public Func<Task> Constructor { internal get; set; }

    public bool IsRenderAsync { get; set; }

    public string RenderMethodNameWithToken { get; set; }

    /// <summary>
    ///     Scope means value of auto generated fields of CompilerGeneratedType instance.
    /// </summary>
    public IReadOnlyDictionary<string, object> Scope { get; set; }

    public void InitializeTarget()
    {
        _target ??= SerializationHelperForCompilerGeneratedClasss.Deserialize(CompilerGeneratedType, Scope);
    }

    internal void CalculateScopeFromTarget()
    {
        object target;

        if (renderFuncWithScope is not null)
        {
            target = renderFuncWithScope.Target;
        }
        else if (renderFuncAsyncWithScope is not null)
        {
            target = renderFuncAsyncWithScope.Target;
        }
        else
        {
            target = _target;
        }

        if (target is null)
        {
            throw DeveloperException("Invalid usage of useState. target not calculated.");
        }

        Scope = SerializationHelperForCompilerGeneratedClasss.Serialize(target);
    }

    protected override Element render()
    {
        if (IsRenderAsync)
        {
            return NoneOfRender.Value;
        }

        if (renderFuncWithScope is not null)
        {
            return renderFuncWithScope(this);
        }

        MethodInfo methodInfo = null;
        if (TryResolveMethodInfo(RenderMethodNameWithToken, ref methodInfo))
        {
            object[] invocationParameters = null;
            if (methodInfo.GetParameters().Length == 1)
            {
                invocationParameters = [this];
            }

            var renderResult = (Element)methodInfo.Invoke(_target, invocationParameters);

            // protect for calling twice
            Constructor = null;

            return renderResult;
        }

        throw DeveloperException("Invalid usage of useState.");
    }

    protected override async Task<Element> renderAsync()
    {
        if (renderFuncAsyncWithScope is not null)
        {
            var fc = await renderFuncAsyncWithScope();

            return fc?.Invoke(this);
        }

        MethodInfo methodInfo = null;
        if (TryResolveMethodInfo(RenderMethodNameWithToken, ref methodInfo))
        {
            object[] invocationParameters = null;
            if (methodInfo.GetParameters().Length == 1)
            {
                invocationParameters = [this];
            }

            var renderResult = (Task<Element>)methodInfo.Invoke(_target, invocationParameters);

            // protect for calling twice
            Constructor = null;

            return renderResult;
        }

        throw DeveloperException("Invalid usage of useState.");
    }
}