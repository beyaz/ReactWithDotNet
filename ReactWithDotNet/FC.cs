using System.Reflection;

namespace ReactWithDotNet;

/// <summary>
///     Functional component.
/// </summary>
public delegate Element FC(Scope cmp);

public interface Scope
{
    public Client Client { get; }

    public ReactContext Context { get; }
}

sealed class FunctionalComponent : Component, Scope
{
    internal object _target;
    
    internal Func<Task<FC>> renderFuncAsyncWithScope;

    internal FC renderFuncWithScope;

    public Type CompilerGeneratedType { get; set; }

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

            var response = methodInfo.Invoke(_target, invocationParameters);

            // recalculate scope because maybe fields have changed
            Scope = SerializationHelperForCompilerGeneratedClasss.Serialize(_target);

            return (Element)response;
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

            var response = methodInfo.Invoke(_target, invocationParameters);

            // recalculate scope because maybe fields have changed
            Scope = SerializationHelperForCompilerGeneratedClasss.Serialize(_target);

            return (Task<Element>)response;
        }

        throw DeveloperException("Invalid usage of useState.");
    }
}