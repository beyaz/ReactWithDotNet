using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace ReactDotNet
{
    public static class Mixin
    {
        public static readonly JsonNamingPolicy JsonNamingPolicy =  JsonNamingPolicy.CamelCase;

        public static string ToJson(object value)
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions().ModifyForReactDotNet());
        }

        public static JsonSerializerOptions ModifyForReactDotNet(this JsonSerializerOptions options)
        {
            return JsonSerializationOptionHelper.Modify(options);
        }
    }
}