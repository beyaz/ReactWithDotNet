using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReactDotNet.PrimeReact;

namespace ReactDotNet
{
    public static class JsonSerializationOptionHelper
    {
        public static string ToJson(object value)
        {
            var option = Modify(new JsonSerializerOptions());

            return JsonSerializer.Serialize(value, option);
        }

        #region Public Methods
        public static JsonSerializerOptions Modify(JsonSerializerOptions options)
        {
            options.WriteIndented    = true;
            options.IgnoreNullValues = true;



            options.PropertyNamingPolicy = Mixin.JsonNamingPolicy;
            options.Converters.Add(new UnionConverter<AlignContent>());
            options.Converters.Add(new UnionConverter<Display>());
            options.Converters.Add(new ActionConverter());
            options.Converters.Add(new BindingPathConverter());
            

            options.Converters.Add(new EnumToStringConverter<TooltipPositionType>()); 



            return options;
        }
        #endregion

        #region Methods
        static object GetValueFromUnion<B>(Union<string, B> union) where B : Enum
        {
            if (union.a != null)
            {
                return union.a;
            }

            var b             = union.b;
            var field         = b.GetType().GetField(b.ToString());
            var nameAttribute = (NameAttribute)field?.GetCustomAttributes(typeof(NameAttribute)).FirstOrDefault();
            if (nameAttribute != null)
            {
                return nameAttribute.value;
            }

            return b;
        }
        #endregion

        class BindingPathConverter:JsonConverter<Expression<Func<string>>>
        {
            public override Expression<Func<string>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Expression<Func<string>> propertyAccessor, JsonSerializerOptions options)
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

                    return string.Join(Separator, path.Select(options.PropertyNamingPolicy.ConvertName));
                }


                var memberExpression = propertyAccessor.Body as MemberExpression;
                if (memberExpression == null)
                {
                    throw new ArgumentException(propertyAccessor.ToString());
                }

                var bindingPath = NameofAllPath(memberExpression);

                writer.WriteStringValue(bindingPath);
            }
        }

        class EnumToStringConverter<T>: JsonConverter<T>
        {
            #region Public Methods
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
            #endregion
        }

        class ActionConverter : JsonConverter<Action>
        {
            #region Public Methods
            public override Action Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Action value, JsonSerializerOptions options)
            {
                var rawValue = value.Method.Name;

                var prefix = "$remote$";
                writer.WriteStringValue(prefix+ rawValue);
            }
            #endregion
        }

        class UnionConverter<B> : JsonConverter<Union<string, B>> where B : Enum
        {
            #region Public Methods
            public override Union<string, B> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Union<string, B> value, JsonSerializerOptions options)
            {
                var rawValue = GetValueFromUnion(value);

                if (rawValue is string str)
                {
                    writer.WriteStringValue(options.PropertyNamingPolicy.ConvertName(str));
                    return;
                }

                writer.WriteStringValue(options.PropertyNamingPolicy.ConvertName(rawValue.ToString()));
            }
            #endregion
        }
    }
}