using System.Diagnostics.Contracts;
using System.Reflection;
using System.Reflection.Emit;

namespace ReactWithDotNet;

static class DelegatePropertyHelper
{
    public static Delegate ReCalculatePropertyValue(ReactComponentBase reactComponent, PropertyInfo propertyInfo)
    {
        var genericArguments = propertyInfo.PropertyType.GetGenericArguments();
        if (genericArguments.Length == 0 ||
            genericArguments.Length > 4 ||
            genericArguments[^1] != typeof(Task))
        {
            throw DeveloperException($"Custom Delegate should return Func<,,,Task> {propertyInfo.Name}");
        }

        var argumentTypes = genericArguments.SkipLast(1).ToArray();

        var methodInfoCreateDelagate = typeof(DelegatePropertyHelper).GetMethod(nameof(CreateDelegate), BindingFlags.Static | BindingFlags.NonPublic);
        if (methodInfoCreateDelagate is null)
        {
            throw new ArgumentNullException(nameof(methodInfoCreateDelagate));
        }

        var nameofCurryMethod = $"Curry{argumentTypes.Length}";

        var methodInfoCurry = typeof(DelegatePropertyHelper).GetMethod(nameofCurryMethod, BindingFlags.Static | BindingFlags.NonPublic);
        if (methodInfoCurry is null)
        {
            throw new ArgumentNullException(nameof(methodInfoCurry));
        }

        var delegateFunc = methodInfoCreateDelagate.Invoke(null, [reactComponent, propertyInfo]);

        if (argumentTypes.Length > 0)
        {
            methodInfoCurry = methodInfoCurry.MakeGenericMethod(argumentTypes);
        }

        return (Delegate)methodInfoCurry.Invoke(null, [reactComponent, delegateFunc]);
    }

    [Pure]
    internal static Func<Task> Curry0(ReactComponentBase reactComponent, Func<object, Task> func)
    {
        return () => func(reactComponent);
    }

    [Pure]
    internal static Func<A, Task> Curry1<A>(ReactComponentBase reactComponent, Func<object, A, Task> func)
    {
        return x => func(reactComponent, x);
    }

    [Pure]
    internal static Func<A, B, Task> Curry2<A, B>(ReactComponentBase reactComponent, Func<object, A, B, Task> func)
    {
        return (x, y) => func(reactComponent, x, y);
    }

    [Pure]
    internal static Func<A, B, C, Task> Curry3<A, B, C>(ReactComponentBase reactComponent, Func<object, A, B, C, Task> func)
    {
        return (x, y, z) => func(reactComponent, x, y, z);
    }

    static Delegate CreateDelegate(ReactComponentBase reactComponent, PropertyInfo propertyInfo)
    {
        var methodInfo = typeof(DelegatePropertyHelper).GetMethod(nameof(DispatchDotNetCustomEvent), BindingFlags.Static | BindingFlags.NonPublic);
        if (methodInfo is null)
        {
            throw new NullReferenceException(nameof(methodInfo));
        }

        var genericArguments = propertyInfo.PropertyType.GetGenericArguments();

        DynamicMethod dynamicMethod;
        {
            var componentType = reactComponent.GetType();

            var name = $"{componentType.FullName}::DelegateFor::{propertyInfo.Name}";

            var returnType = typeof(Task);

            var parameterTypes = new List<Type>
            {
                typeof(object)
            };

            parameterTypes.AddRange(genericArguments.SkipLast(1));

            dynamicMethod = new(name, returnType, parameterTypes.ToArray());
        }

        var il = dynamicMethod.GetILGenerator();

        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldstr, propertyInfo.Name);

        il.Emit(OpCodes.Ldc_I4, genericArguments.Length - 1);
        il.Emit(OpCodes.Newarr, typeof(object));

        for (var i = 0; i < genericArguments.Length - 1; i++)
        {
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldc_I4, i);
            il.Emit(OpCodes.Ldarg, i + 1);
            if (genericArguments[i].IsValueType)
            {
                il.Emit(OpCodes.Box, genericArguments[i]);
            }

            il.Emit(OpCodes.Stelem_I);
        }

        il.Emit(OpCodes.Call, methodInfo);
        il.Emit(OpCodes.Ret);

        var targetDelegateType = genericArguments.Length switch
        {
            1 => typeof(Func<,>).MakeGenericType([typeof(object), typeof(Task)]),
            2 => typeof(Func<,,>).MakeGenericType([typeof(object), genericArguments[0], typeof(Task)]),
            3 => typeof(Func<,,,>).MakeGenericType([typeof(object), genericArguments[0], genericArguments[1], typeof(Task)]),
            4 => typeof(Func<,,,,>).MakeGenericType([typeof(object), genericArguments[0], genericArguments[1], genericArguments[2], typeof(Task)]),
            _ => throw DeveloperException($"Custom Delegate should return Task {propertyInfo.Name}")
        };

        return dynamicMethod.CreateDelegate(targetDelegateType);
    }

    static Task DispatchDotNetCustomEvent(ReactComponentBase reactComponent, string propertyName, object[] eventArguments)
    {
        var senderInfo = GetEventSenderInfo(reactComponent, propertyName);

        reactComponent.Client.DispatchDotNetCustomEvent(senderInfo, eventArguments);

        return Task.CompletedTask;
    }
}