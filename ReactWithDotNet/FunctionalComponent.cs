using System.Reflection;

namespace ReactWithDotNet;

delegate Func<Element> FunctionalComponent(Scope cmp);

public interface Scope
{
    public Client Client { get; }

    public ReactContext Context { get; }
}

sealed class CompilerGeneratedClassComponent : Component, Scope
{
    internal object _target;
    internal Func<Element> renderFunc;

    internal Func<Task<Element>> renderFuncAsync;

    internal Func<Scope, Task<Element>> renderFuncAsyncWithScope;

    internal Func<Scope, Element> renderFuncWithScope;

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

        if (renderFunc is not null)
        {
            return renderFunc();
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

    protected override Task<Element> renderAsync()
    {
        if (renderFuncAsync is not null)
        {
            return renderFuncAsync();
        }

        if (renderFuncAsyncWithScope is not null)
        {
            return renderFuncAsyncWithScope(this);
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