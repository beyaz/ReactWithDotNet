using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace ReactDotNet
{
    public static class Mixin
    {
        public static  readonly JsonNamingPolicy JsonNamingPolicy = JsonNamingPolicy.CamelCase;

        public static string ToJson(object value)
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions().ModifyForReactDotNet());
        }

        public static JsonSerializerOptions ModifyForReactDotNet(this JsonSerializerOptions options)
        {
            return JsonSerializationOptionHelper.Modify(options);
        }

        public static string Bind(Expression<Func<string>> propertyAccessor)
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

                return string.Join(Separator, path.Select(JsonNamingPolicy.ConvertName));
            }


            var memberExpression = propertyAccessor.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(propertyAccessor.ToString());
            }

            var bindingPath = NameofAllPath(memberExpression);

            return bindingPath;
        }
    }
}