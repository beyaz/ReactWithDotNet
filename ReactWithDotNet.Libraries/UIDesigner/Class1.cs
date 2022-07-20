using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;



[Serializable]
class UIDesignerModel
{


    public string ReactWithDotnetComponentAsJson { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }

    public string SelectedFolderLastQuery { get; set; }
    public string SelectedFolder { get; set; }
    
    
    public string SelectedAssembly { get; set; }
    public string SelectedAssemblyLastQuery { get; set; }

    public IReadOnlyList<string> SelectedFolderSuggestions { get; set; } = new[] { @"d:\boa\server\bin\", @"d:\boa\client\bin\" };

    #region Public Properties

    


    public int ScreenWidth { get; set; } = 100;
    public string SelectedComponentTypeReference { get; set; }


    #endregion
}





static class UIDesignerViewExtension
{
    #region Public Methods
    

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