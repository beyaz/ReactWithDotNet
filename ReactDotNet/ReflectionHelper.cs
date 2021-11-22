using System;
using System.Linq;
using System.Reflection;


namespace ReactDotNet
{
    static class ReflectionHelper
    {
        public static void InvokeInstanceMethod(object instance, string methodName, object argument0)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            var methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            var method = methods.FirstOrDefault(m => m.Name == methodName.Trim() &&  m.GetParameters().Length == 1);
            if (method == null)
            {
                throw new MissingMethodException(instance.GetType().FullName + ":" + methodName);
            }

            method.Invoke(instance, argument0);
        }

        public static Type FindComponentTypeByName(string componentName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            assemblies.Reverse();

            foreach (var assembly in assemblies)
            {
                var type = assembly.GetTypes().FirstOrDefault(x => x.Name.Equals(componentName, StringComparison.OrdinalIgnoreCase));
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}