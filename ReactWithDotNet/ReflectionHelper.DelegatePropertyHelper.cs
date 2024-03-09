using System.Diagnostics.Contracts;
using System.Reflection;
using System.Reflection.Emit;

namespace ReactWithDotNet;

static class DelegatePropertyHelper
{
    public static Delegate ReCalculatePropertyValue(ReactComponentBase reactComponent, PropertyInfo propertyInfo)
    {
        var methodInfoCreateDelagate = typeof(DelegatePropertyHelper).GetMethod(nameof(CreateDelegate), BindingFlags.Static| BindingFlags.NonPublic);
        if (methodInfoCreateDelagate is null)
        {
            throw new ArgumentNullException(nameof(methodInfoCreateDelagate));
        }
        
        var methodInfoCurry = typeof(DelegatePropertyHelper).GetMethod(nameof(Curry),BindingFlags.Static| BindingFlags.NonPublic);
        if (methodInfoCurry is null)
        {
            throw new ArgumentNullException(nameof(methodInfoCurry));
        }
        
        var argumentTypes = typeof(string);
        
        var genericArguments = propertyInfo.PropertyType.GetGenericArguments();
        if (genericArguments.Length == 2)
        {
            argumentTypes = genericArguments[0];
        }

        
        
        var createDelegateMethodInfo = methodInfoCreateDelagate.MakeGenericMethod(argumentTypes);

        var delegateFunc = createDelegateMethodInfo.Invoke(null, [reactComponent, propertyInfo]);

        
        var curryMethodInfo = methodInfoCurry.MakeGenericMethod(argumentTypes);

        return (Delegate) curryMethodInfo.Invoke(null, [reactComponent, delegateFunc]);
    }
    
    [Pure]
    static Func<A,Task> Curry<A>(ReactComponentBase reactComponent, Func<object,A, Task> a)
    {
        return  x => a(reactComponent, x);
    }
    
    static Func<object,T, Task> CreateDelegate<T>(ReactComponentBase reactComponent, PropertyInfo propertyInfo)
    {
        var methodInfo = typeof(DelegatePropertyHelper).GetMethod(nameof(DispatchDotNetCustomEvent),BindingFlags.Static| BindingFlags.NonPublic);
        if (methodInfo is null)
        {
            throw new NullReferenceException(nameof(methodInfo));
        }

        var componentType = reactComponent.GetType();

        // initialize dynamic method
        var name = $"{componentType.FullName}::DelegateFor::{propertyInfo.Name}";

        var returnType = typeof(Task);

        Type[] parameterTypes = [typeof(object) ,typeof(T)];
        
        var dynamicMethod = new DynamicMethod( name, returnType, parameterTypes);

        var ilGenerator = dynamicMethod.GetILGenerator();

        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldstr, propertyInfo.Name);
        ilGenerator.Emit(OpCodes.Ldarg_1);
        ilGenerator.Emit(OpCodes.Ldc_I4_1);
        ilGenerator.Emit(OpCodes.Newarr, typeof(object));
        ilGenerator.Emit(OpCodes.Dup);
        ilGenerator.Emit(OpCodes.Ldc_I4_0);
        ilGenerator.Emit(OpCodes.Ldarg_1);
        ilGenerator.Emit(OpCodes.Stelem_Ref);
        ilGenerator.Emit(OpCodes.Call, methodInfo);
        ilGenerator.Emit(OpCodes.Ret);

        return  (Func<object,T, Task>)dynamicMethod.CreateDelegate(typeof(Func<object,T, Task>));
    }

    static Task DispatchDotNetCustomEvent(ReactComponentBase reactComponent, string propertyName, object[] eventArguments)
    {
        var senderInfo = GetEventSenderInfo(reactComponent, propertyName);

        reactComponent.Client.DispatchDotNetCustomEvent(senderInfo, eventArguments);

        return Task.CompletedTask;
    }
}