using System.Text.Json;
using System.Text.Json.Serialization;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet;

public static class ILHelper
{
    static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
        Converters =
        {
            new MethodBodyConverter(),
            new TypeReferenceConverter(),
            new MethodReferenceConverter(),
            new ParameterDefinitionConverter()
        }
    };

    public static string Deneme(string p0)
    {
        if (p0 == "a")
        {
            try
            {
                return "a";
            }
            catch (Exception)
            {
                return "t";
            }
        }

        object x = p0;
        if (x is string)
        {
        }

        return "b";
    }

    internal static object denemeeee()
    {
        var assemblyDefinition = AssemblyDefinition.ReadAssembly(typeof(ILHelper).Assembly.Location);

        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType("ReactWithDotNet", "ILHelper");
            foreach (var methodDefinition in typeDefinition.Methods)
            {
                if (methodDefinition.Name is "Deneme")
                {
                    var body = methodDefinition.Body;

                    return JsonSerializer.Serialize(body, JsonSerializerOptions);
                }
            }
        }

        return null;
    }

    static void Serialize(Utf8JsonWriter writer, MethodBody body, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        var opCodes = new List<int>();
        var operands = new Dictionary<int, object>();

        var instructions = body.Instructions;

        for (var i = 0; i < instructions.Count; i++)
        {
            var instruction = instructions[i];

            var code = instruction.OpCode.Code;

            var operand = instruction.Operand;

            opCodes.Add((int)code);

            if (code == Code.Ldstr)
            {
                operands.Add(i, (string)instruction.Operand);
                continue;
            }

            if (operand is Instruction operandAsInstruction)
            {
                operands.Add(i, instructions.IndexOf(operandAsInstruction));
                continue;
            }

            if (operand is MethodReference methodReference)
            {
                operands.Add(i, methodReference);
                continue;
            }

            if (operand is TypeReference typeReference)
            {
                operands.Add(i, typeReference);
                continue;
            }

            if (operand is Instruction[] operandAsInstructionList)
            {
                operands.Add(i, operandAsInstructionList.Select(instructions.IndexOf));
            }
        }

        writer.WritePropertyName(nameof(body.Instructions));

        JsonSerializer.Serialize(writer, opCodes, options);

        writer.WritePropertyName("Operands");

        JsonSerializer.Serialize(writer, operands, options);

        writer.WritePropertyName(nameof(body.ExceptionHandlers));

        JsonSerializer.Serialize(writer, body.ExceptionHandlers.Select(handler => new
        {
            HandlerStart = instructions.IndexOf(handler.HandlerStart),
            HandlerEnd   = instructions.IndexOf(handler.HandlerEnd)
        }), options);

        writer.WriteEndObject();
    }

    static void Serialize(Utf8JsonWriter writer, MethodReference methodReference, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName(nameof(MethodReference.Name));

        JsonSerializer.Serialize(writer, methodReference.Name, options);

        writer.WritePropertyName(nameof(MethodReference.ReturnType));

        JsonSerializer.Serialize(writer, methodReference.ReturnType, options);

        writer.WritePropertyName(nameof(MethodReference.ReturnType));

        JsonSerializer.Serialize(writer, methodReference.Parameters, options);

        writer.WriteEndObject();
    }

    static void Serialize(Utf8JsonWriter writer, ParameterDefinition parameterDefinition, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName(nameof(ParameterDefinition.Index));
        JsonSerializer.Serialize(writer, parameterDefinition.Index, options);

        writer.WritePropertyName(nameof(ParameterDefinition.Name));
        JsonSerializer.Serialize(writer, parameterDefinition.Name, options);

        writer.WritePropertyName(nameof(ParameterDefinition.ParameterType));
        JsonSerializer.Serialize(writer, parameterDefinition.ParameterType, options);

        writer.WriteEndObject();
    }

    static void Serialize(Utf8JsonWriter writer, TypeReference typeReference, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName(nameof(TypeReference.Name));

        JsonSerializer.Serialize(writer, typeReference.Name, options);

        writer.WritePropertyName(nameof(TypeReference.Namespace));

        JsonSerializer.Serialize(writer, typeReference.Namespace, options);

        writer.WriteEndObject();
    }

    sealed class MethodBodyConverter : JsonConverter<MethodBody>
    {
        public override MethodBody Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, MethodBody body, JsonSerializerOptions options)
        {
            Serialize(writer, body, options);
        }
    }

    sealed class MethodReferenceConverter : JsonConverter<MethodReference>
    {
        public override MethodReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, MethodReference methodReference, JsonSerializerOptions options)
        {
            Serialize(writer, methodReference, options);
        }
    }

    sealed class ParameterDefinitionConverter : JsonConverter<ParameterDefinition>
    {
        public override ParameterDefinition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ParameterDefinition parameterDefinition, JsonSerializerOptions options)
        {
            Serialize(writer, parameterDefinition, options);
        }
    }

    sealed class TypeReferenceConverter : JsonConverter<TypeReference>
    {
        public override TypeReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TypeReference typeReference, JsonSerializerOptions options)
        {
            Serialize(writer, typeReference, options);
        }
    }
}