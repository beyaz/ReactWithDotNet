using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet;

public static class ILHelper
{
    //record MethodDefinitionInfo
    //{
    //    public IReadOnlyList<int> OpCodes { get; init; }
        
    //    public Dictionary<int,object> Operands { get; init; }
        
    //    public IReadOnlyList<ExceptionHandlerInfo> ExceptionHandlers { get; init; }
        
    //    public string FullName { get; init; }
    //}

    //record ExceptionHandlerInfo
    //{
    //    public int HandlerStart{ get; init; }
        
    //    public int HandlerEnd{ get; init; }
        
    //    public object CatchType { get; init; }
    //}

    static string Deneme2<T>(string p0)
    {
        return "g";
    }
    static string Deneme2<T,T5>(string p0)
    {
        return "g";
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

        Deneme2<long>("f");
        Deneme2<long,int>("f");
        
        object x = p0;
        if (x is string)
        {
            x.ToString();

            var list = new List<int>();

            list.ToArray();
        }
        return "b";
    }
    
    static object GetMethodBody()
    {
        var assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(typeof(ILHelper).Assembly.Location);


        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType("ReactWithDotNet","ILHelper");
            foreach (var methodDefinition in typeDefinition.Methods)
            {
                if (methodDefinition.Name is "Deneme")
                {
                    return methodDefinition.ToJson();
                }
            }
        }

        return null;


       
    }
    
    static object ToJson(this TypeReference typeReference)
    {
        if (typeReference is GenericInstanceType genericInstanceType)
        {
            
            return new
            {
                Scope            = typeReference.Scope.Name,
                FullName         = typeReference.ToString(),
                GenericArguments = genericInstanceType.GenericArguments.Select(ToJson),
                ElementType      = genericInstanceType.ElementType.ToJson()
            };
            
            
        }
        return new
        {
            Scope    = typeReference.Scope.Name,
            FullName = typeReference.ToString()
        };
    }
        
    static object ToJson(this MethodReference methodReference)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new
            {
                DeclaringType    = ToJson(methodReference.DeclaringType),
                FullName         = methodReference.ToString(),
                GenericArguments = genericInstanceMethod.GenericArguments.Select(ToJson),
                ElementMethod = genericInstanceMethod.ElementMethod.ToJson()
            };
        }
            
        return new
        {
            DeclaringType = ToJson(methodReference.DeclaringType),
            FullName      = methodReference.ToString(),
            GenericParamters = methodReference.GenericParameters.Select(ToJson)
        };
    }
    
    static object ToJson(this MethodDefinition methodDefinition)
    {
        var opCodes = new List<int>();
        var operands = new Dictionary<int,object>();

        var body = methodDefinition.Body;
                    
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
            }
            else if (operand is Instruction operandAsInstruction)
            {
                operands.Add(i, instructions.IndexOf(operandAsInstruction));
            }
            else if (operand is MethodReference methodReference)
            {
                operands.Add(i, ToJson(methodReference));
            }
            else if (operand is TypeReference typeReference)
            {
                operands.Add(i, ToJson(typeReference));
            }
            else if (operand is Instruction[] operandAsInstructionList)
            {
                operands.Add(i, operandAsInstructionList.Select(x=>instructions.IndexOf(x)).ToArray());
            }
        }

        var exceptionHandlers = body.ExceptionHandlers.Select(handler => new 
        {
            HandlerStart = instructions.IndexOf(handler.HandlerStart),
            HandlerEnd   = instructions.IndexOf(handler.HandlerEnd),
            CatchType    = ToJson(handler.CatchType)
        }).ToList();
                    
        return new 
        {
            FullName          = methodDefinition.FullName,
            OpCodes           = opCodes,
            Operands          = operands,
            ExceptionHandlers = exceptionHandlers
        };
    }


    static object ToJson(this GenericParameter genericParameter)
    {
        return new { Name = genericParameter.Name };
    }
}