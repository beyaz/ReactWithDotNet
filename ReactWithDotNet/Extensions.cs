using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReactWithDotNet
{
    static class Extensions
    {

        public static string AsBindingSourcePathInState<T>(this Expression<Func<T>> propertyAccessor)
        {
            string NameofAllPath(MemberExpression memberExpression)
            {
                var path = new List<string>();

                while (memberExpression != null)
                {
                    path.Add(memberExpression.Member.Name);

                    memberExpression = memberExpression.Expression as MemberExpression;
                }

                if (path.Count == 0)
                {
                    return null;
                }

                path.RemoveAt(path.Count - 1);

                path.Reverse();

                const string Separator = ".";

                return string.Join(Separator, path);
            }


            var memberExpression = propertyAccessor.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(propertyAccessor.ToString());
            }

            var bindingPath = NameofAllPath(memberExpression);

            return bindingPath;
        }

        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }

        /// <summary>
        ///     Removes value from end of str
        /// </summary>
        public static string RemoveFromEnd(this string data, string value)
        {
            return RemoveFromEnd(data, value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Removes from end.
        /// </summary>
        public static string RemoveFromEnd(this string data, string value, StringComparison comparison)
        {
            if (data.EndsWith(value, comparison))
            {
                return data.Substring(0, data.Length - value.Length);
            }

            return data;
        }

        /// <summary>
        ///     Removes value from start of str
        /// </summary>
        public static string RemoveFromStart(this string data, string value)
        {
            return RemoveFromStart(data, value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Removes value from start of str
        /// </summary>
        public static string RemoveFromStart(this string data, string value, StringComparison comparison)
        {
            if (data == null)
            {
                return null;
            }

            if (data.StartsWith(value, comparison))
            {
                return data.Substring(value.Length, data.Length - value.Length);
            }

            return data;
        }
    }
}