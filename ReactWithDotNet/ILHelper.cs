using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet;

public static class ILHelper
{
    public static string Deneme2<A,B,C>(string p0, int[] arr = null, int[,,] arr2 = null)
    {
        return "gg";
    }
    
    public static string Deneme(string p0)
    {
        return Deneme2<string, int, int>("g");
        
        //if (p0 == "a")
        //{
        //    try
        //    {
        //        return "a";
        //    }
        //    catch (Exception)
        //    {
        //        return "t";
        //    }
        //}


        //object x = p0;
        //if (x is string)
        //{
        //    Deneme2<string, int, int>("g");
        //}

        //return "b";
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
                    var body = methodDefinition.Body.Map();

                    return JsonSerializer.Serialize(body);
                }
            }
        }

        return null;
    }

   

    static IReadOnlyList<B> ToListOf<A, B>(this IEnumerable<A> enumerable, Func<A, B> convertFunc)
    {
        return enumerable?.Select(convertFunc).ToList();
    }
    
   
    
    
   

}

static class MonoCecilToJsonModelMapper
{
     static MethodBodyModel Map(this MethodBody body)
    {

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







        return new()
        {
            Instructions = opCodes,
            Operands     = operands,
            ExceptionHandlers = body.ExceptionHandlers.ToListOf(handler => new ExceptionHandler
            {
                HandlerStart = instructions.IndexOf(handler.HandlerStart),
                HandlerEnd   = instructions.IndexOf(handler.HandlerEnd)
            })
        };
    }


    
    
    static MethodReferenceModel Map(this MethodReference methodReference)
    {
        return new MethodReferenceModel
        {
            ReturnType = methodReference.ReturnType.Map(),
            Name       = methodReference.Name,
            Parameters = methodReference.Parameters.ToListOf(Map)
        };
    }

    static ParameterDefinitionModel Map(this ParameterDefinition parameterDefinition)
    {
        return new()
        {
            Index         = parameterDefinition.Index,
            ParameterType = parameterDefinition.ParameterType,
            Name          = parameterDefinition.Name
        };
    }
    
    
    
    

    
    
    static TypeReferenceModel Map(this TypeReference typeReference)
    {
        return new()
        {
            Name=typeReference.Name,
            Namespace = typeReference.Namespace,
            
            
        };
    }
    

    static ArrayTypeModel Map(this ArrayType arrayType)
    {
        return new()
        {
            ElementType = Map(arrayType.ElementType),
            Rank        = arrayType.Rank
        };
    }

    
    
    
    static GenericInstanceMethodModel Map(GenericInstanceMethod genericInstanceMethod)
    {
        return new()
        {
            ElementMethod    = genericInstanceMethod.ElementMethod.Map(),
            GenericArguments = genericInstanceMethod.GenericArguments.Select(Map).ToList()
        };
    }
    
}