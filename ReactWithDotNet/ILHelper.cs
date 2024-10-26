using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using static ReactWithDotNet.JsonMap;

namespace ReactWithDotNet;

public static class ILHelper
{
    record MethodDefinitionInfo
    {
        public IReadOnlyList<int> OpCodes { get; init; }
        
        public Dictionary<int,object> Operands { get; init; }
        
        public IReadOnlyList<ExceptionHandlerInfo> ExceptionHandlers { get; init; }
        
        public string FullName { get; init; }
    }

    record ExceptionHandlerInfo
    {
        public int HandlerStart{ get; init; }
        
        public int HandlerEnd{ get; init; }
        
        public string CatchType { get; init; }
    }
    

    static string Deneme(string p0)
    {
        if (p0 == "a")
        {
            try
            {
                return "a";
            }
            catch (Exception exception)
            {

                return "t";
            } 
        }

        object x = p0;
        if (x is string)
        {
            x.ToString();
        }
        return "b";
    }
    
    sealed class MethodBodyConverter : JsonConverter<MethodBody>
    {
        public override MethodBody Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, MethodBody body, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            
            var opCodes = new List<int>();
            var operands = new Dictionary<int,object>();

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
                    operands.Add(i, new []{methodReference.DeclaringType.Scope.Name ,methodReference.DeclaringType.FullName, methodReference.ToString()});
                    continue;
                }
                
                if (operand is TypeReference typeReference)
                {
                    operands.Add(i, typeReference);
                    continue;
                }
                
                if (operand is Instruction[] operandAsInstructionList)
                {
                    operands.Add(i, operandAsInstructionList.Select(x=>instructions.IndexOf(x)).ToArray());
                    continue;
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
                HandlerEnd   = instructions.IndexOf(handler.HandlerEnd),
                
            }), options);
            

            writer.WriteEndObject();
        }
    }
    
    sealed class TypeReferenceConverter: JsonConverter<TypeReference>
    {
        public override TypeReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TypeReference typeReference, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            
            writer.WritePropertyName(nameof(TypeReference.Name));
            
            JsonSerializer.Serialize(writer, typeReference.Name, options);
            
            writer.WritePropertyName(nameof(TypeReference.Namespace));
            
            JsonSerializer.Serialize(writer, typeReference.Namespace, options);
            
            writer.WriteEndObject();
        }
    }
    
    internal static object GetMethodBody()
    {
        var assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(typeof(ILHelper).Assembly.Location);

        
            
        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType("ReactWithDotNet","ILHelper");
            foreach (var methodDefinition in typeDefinition.Methods)
            {
                if (methodDefinition.Name is "Deneme")
                {
                    var body = methodDefinition.Body;
                    

                    return JsonSerializer.Serialize(body, new JsonSerializerOptions
                    {
                        Converters = { new MethodBodyConverter(), new TypeReferenceConverter() }
                    });
                    

                   
                }
            }
        }

        return null;
    }
}