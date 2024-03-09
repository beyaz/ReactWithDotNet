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
            throw DeveloperException($"Custom Delegate should return Task {propertyInfo.Name}");
        }
        
        var argumentTypes = genericArguments.SkipLast(1).ToArray();
        
        
        var methodInfoCreateDelagate = typeof(DelegatePropertyHelper).GetMethod(nameof(CreateDelegate), BindingFlags.Static| BindingFlags.NonPublic);
        if (methodInfoCreateDelagate is null)
        {
            throw new ArgumentNullException(nameof(methodInfoCreateDelagate));
        }

        var nameofCurryMethod = $"Curry{argumentTypes.Length}";
        
        var methodInfoCurry = typeof(DelegatePropertyHelper).GetMethod(nameofCurryMethod,BindingFlags.Static| BindingFlags.NonPublic);
        if (methodInfoCurry is null)
        {
            throw new ArgumentNullException(nameof(methodInfoCurry));
        }

        var delegateFunc = methodInfoCreateDelagate.Invoke(null, [reactComponent, propertyInfo]);

        
        var curryMethodInfo = methodInfoCurry.MakeGenericMethod(argumentTypes);

        return (Delegate) curryMethodInfo.Invoke(null, [reactComponent, delegateFunc]);
    }
    
    [Pure]
    static Func<Task> Curry0(ReactComponentBase reactComponent, Func<object, Task> func)
    {
        return  () => func(reactComponent);
    }
    
    [Pure]
    static Func<A,Task> Curry1<A>(ReactComponentBase reactComponent, Func<object,A, Task> func)
    {
        return  x => func(reactComponent, x);
    }
    
    [Pure]
    static Func<A,B,Task> Curry2<A,B>(ReactComponentBase reactComponent, Func<object,A,B, Task> func)
    {
        return  (x,y) => func(reactComponent, x,y);
    }
    
    [Pure]
    static Func<A,B,C,Task> Curry3<A,B,C>(ReactComponentBase reactComponent, Func<object,A,B,C, Task> func)
    {
        return  (x,y,z) => func(reactComponent, x,y,z);
    }
    
    
    static Delegate CreateDelegate(ReactComponentBase reactComponent, PropertyInfo propertyInfo)
    {
        var methodInfo = typeof(DelegatePropertyHelper).GetMethod(nameof(DispatchDotNetCustomEvent),BindingFlags.Static| BindingFlags.NonPublic);
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
        
        var ilGenerator = dynamicMethod.GetILGenerator();

        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldstr, propertyInfo.Name);
        
        ilGenerator.Emit(OpCodes.Ldc_I4,genericArguments.Length-1);
        ilGenerator.Emit(OpCodes.Newarr, typeof(object));
       
        
        for (var i = 0; i < genericArguments.Length-1; i++)
        {
            ilGenerator.Emit(OpCodes.Dup);
            ilGenerator.Emit(OpCodes.Ldc_I4,i);
            ilGenerator.Emit(OpCodes.Ldarg,i+1);
            ilGenerator.Emit(OpCodes.Stelem_I);
        }
        
        ilGenerator.Emit(OpCodes.Call, methodInfo);
        ilGenerator.Emit(OpCodes.Ret);


        Type targetDelegateType = null;
        if (genericArguments.Length == 2)
        {
            targetDelegateType = typeof(Func<,,>).MakeGenericType([typeof(object), genericArguments[0], typeof(Task)]);
        }
        else if (genericArguments.Length == 3)
        {
            targetDelegateType = typeof(Func<,,,>).MakeGenericType([typeof(object), genericArguments[0],genericArguments[1] ,typeof(Task)]);
        }
        
        return  dynamicMethod.CreateDelegate(targetDelegateType);
    }

    static void AAAA(ReactComponentBase reactComponent, string h)
    {
        DispatchDotNetCustomEvent(reactComponent, "abc",[h]);
    }
    
    static void AAAA2(ReactComponentBase reactComponent, string h, string j)
    {
        DispatchDotNetCustomEvent(reactComponent, "abc",[h,j]);
    }
    
    static object[] AAAA2y(int i)
    {
        object[] a = new object[666];

        return a;
    }
    
    
    static Task DispatchDotNetCustomEvent(ReactComponentBase reactComponent, string propertyName, object[] eventArguments)
    {
        var senderInfo = GetEventSenderInfo(reactComponent, propertyName);

        reactComponent.Client.DispatchDotNetCustomEvent(senderInfo, eventArguments);

        return Task.CompletedTask;
    }
}