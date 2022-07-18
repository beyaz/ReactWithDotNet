using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;



[Serializable]
class UIDesignerModel
{
    #region Public Properties

    public IReadOnlyList<DotNetObjectPropertyValue> Properties { get; set; }

    public string SaveDirectoryPath { get; set; } = @"d:\\temp\\";

    public int ScreenWidth { get; set; } = 100;
    public string SelectedComponentTypeReference { get; set; }

    public string SelectedPropertyName { get; set; }
    public string SelectedPropertyValue { get; set; }

    public string ComponentsLocatedAssemblyName { get; set; } = "QuranAnalyzer.WebUI";
    #endregion
}

class ReactComponentInfo
{
    #region Public Properties
    public string Name { get; set; }
    public string Value { get; set; }
    #endregion
}

 class Pair
{
    #region Public Properties
    public string Key { get; set; }
    public string Value { get; set; }
    #endregion
}

[Serializable]
 sealed class DotNetObjectPropertyValue
{
    #region Public Properties
    public string Path { get; set; }
    public string Value { get; set; }
    #endregion
}

static class UIDesignerViewExtension
{
    #region Public Methods
    public static void TryUpdateFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> findFunction, Action<T> update)
    {
        var value = enumerable.FirstOrDefault(findFunction);
        if (value is not null)
        {
            update(value);
        }
    }

    public static Exception SaveValueToPropertyPath(object value, object instance, string propertyPath)
    {
        if (instance == null)
        {
            return new ArgumentNullException(nameof(instance));
        }

        while (true)
        {
            var parts = propertyPath.Split('.');

            var propertyInfo = instance.GetType().GetProperty(parts[0]);

            if (propertyInfo == null)
            {
                return new MissingMemberException(instance.GetType().FullName, parts[0]);
            }

            if (parts.Length == 1)
            {
                propertyInfo.SetValue(instance, value, null);
            }
            else
            {
                var innerInstance = propertyInfo.GetValue(instance);
                if (innerInstance == null)
                {
                    innerInstance = Activator.CreateInstance(propertyInfo.PropertyType);
                    if (innerInstance == null)
                    {
                        return new NullReferenceException(propertyInfo.Name);
                    }

                    propertyInfo.SetValue(instance, innerInstance);
                }

                instance     = innerInstance;
                propertyPath = string.Join(".", parts.Skip(1));

                continue;
            }

            break;
        }

        return null;
    }

    public static void OpenNullProperties(object instance)
    {
        if (instance == null)
        {
            return;
        }

        foreach (var propertyInfo in GetNestedProperties(instance.GetType()))
        {
            if (propertyInfo.GetValue(instance) == null)
            {
                var propertyValue = Activator.CreateInstance(propertyInfo.PropertyType);

                OpenNullProperties(propertyValue);

                propertyInfo.SetValue(instance, propertyValue);
            }
        }

        static IEnumerable<PropertyInfo> GetNestedProperties(Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    continue;
                }

                if (propertyInfo.PropertyType.IsAbstract)
                {
                    continue;
                }

                if (propertyInfo.PropertyType.IsValueType)
                {
                    continue;
                }

                yield return propertyInfo;
            }
        }
    }
    #endregion
}