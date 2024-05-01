using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;

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
    
    static MethodDefinitionInfo GetMethodBody()
    {
        var assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(typeof(ILHelper).Assembly.Location);


        foreach (var moduleDefinition in assemblyDefinition.Modules)
        {
            var typeDefinition = moduleDefinition.GetType("ReactWithDotNet","ILHelper");
            foreach (var methodDefinition in typeDefinition.Methods)
            {
                if (methodDefinition.Name is "Deneme")
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
                            operands.Add(i, new []{methodReference.DeclaringType.Scope.Name ,methodReference.DeclaringType.FullName, methodReference.ToString()});
                        }
                        else if (operand is TypeReference typeReference)
                        {
                            operands.Add(i, new []{typeReference.Scope.Name, typeReference.ToString()});
                        }
                        else if (operand is Instruction[] operandAsInstructionList)
                        {
                            operands.Add(i, operandAsInstructionList.Select(x=>instructions.IndexOf(x)).ToArray());
                        }
                    }

                    var exceptionHandlers = body.ExceptionHandlers.Select(handler => new ExceptionHandlerInfo
                    {
                        HandlerStart = instructions.IndexOf(handler.HandlerStart),
                        HandlerEnd   = instructions.IndexOf(handler.HandlerEnd),
                        CatchType    = handler.CatchType.ToString()
                    }).ToList();
                    
                    return new MethodDefinitionInfo
                    {
                        FullName= methodDefinition.FullName,
                        OpCodes  = opCodes,
                        Operands = operands,
                        ExceptionHandlers = exceptionHandlers
                    };
                }
            }
        }

        return null;
    }
}