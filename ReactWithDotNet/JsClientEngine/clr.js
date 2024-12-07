/**
 * @typedef {Object} MetadataScopeModel
 * @property {string} Name
 */

/**
 * @typedef {Object} MemberReference
 * @property {string} Name
 * @property {number | null} DeclaringType
 * @property {number} IsDefinition
 */

/**
 * @typedef {MemberReference} TypeReferenceModel
 * @property {string} Namespace
 * @property {number} Scope
 * @property {number} IsValueType
 * @property {number} IsGenericParameter
 * @property {number} Position
 * @property {number | null} DeclaringMethod
 * @property {number} IsGenericInstance
 * @property {number} ElementType
 * @property {number[]} GenericArguments
 * 
 * @property {number} Rank
 * 
 */

/**
 * @typedef {TypeReferenceModel} TypeDefinitionModel
 * @property {number} BaseType
 * @property {number} Methods
 * @property {number} Fields
 * @property {number} Properties
 * @property {number} NestedTypes
 * @property {number} Events
 */


/**
 * @enum {number}
 */
const ExceptionHandlerType =
{
    Catch: 0,
    Filter: 1,
    Finally: 2,
    Fault: 4,
}

/**
 * @typedef {Object} ExceptionHandler
 * @property {number} TryStart
 * @property {number} TryEnd
 * @property {number} HandlerStart
 * @property {number} HandlerEnd
 * @property {number | null} CatchType
 * @property {ExceptionHandlerType} HandlerType
 */


/**
 * @typedef {Object} MethodBodyModel
 * @property {number[]} Instructions
 * @property {Object[]} Operands
 * @property {ExceptionHandler[]} ExceptionHandlers
 */


/**
 * @typedef {Object} ParameterDefinitionModel
 * @property {number} Index
 * @property {string} Name
 * @property {number} ParameterType
 */


/**
 * @typedef {MemberReference} MethodReferenceModel
 * @property {number} ReturnType
 * @property {ParameterDefinitionModel[]} Parameters
 */

/**
 * @typedef {Object} CustomAttributeModel
 * @property {number} ReturnType
 * @property {Object[]} Parameters
 */

/**
 * @typedef {Object} MethodDefinitionModel
 * @property {MethodBodyModel} Body
 * @property {CustomAttributeModel[]} CustomAttributes
 * @property {number} IsStatic
 * @property {number} IsConstructor
 */


/**
 * @typedef {Object} GenericInstanceMethodModel
 * @property {number} ElementMethod
 * @property {number[]} GenericArguments
 * @property {number} IsGenericInstance
 */

/**
 * @typedef {Object} Metadata
 * @property {MetadataScopeModel[]} MetadataScopes
 * @property {Array<TypeReferenceModel>} Types
 * @property {MemberReference[]} Fields
 * @property {Array<MethodDefinitionModel | MethodReferenceModel | GenericInstanceMethodModel>} Methods
 * @property {MemberReference[]} Properties
 * @property {MemberReference[]} Events
 */

/**
 * @type {Metadata}
 */
let GlobalMetadata;


/**
 * @param {Metadata} metadata
 * @returns {void}
 */
function ImportMetadata(metadata)
{
    if (!GlobalMetadata)
    {
        GlobalMetadata = metadata;

        return;
    }

    const getGlobalScopeIndex  = CreateFunction_GetGlobalScopeIndex(metadata);
    const getGlobalTypeIndex   = CreateFunction_GetGlobalTypeIndex(metadata);
    const getGlobalMethodIndex = CreateFunction_GetGlobalMethodIndex(metadata);

    /**
     * @param {TypeReferenceModel | TypeDefinitionModel } type
     */
    function recalculateIndexesOfType(type)
    {
        if (type.Scope != null)
        {
            type.Scope = getGlobalScopeIndex(type.Scope);
        }
        
        if (type.BaseType != null)
        {
            type.BaseType = getGlobalTypeIndex(type.BaseType);    
        }

        if (type.ElementType != null)
        {
            type.ElementType = getGlobalTypeIndex(type.ElementType);
        }
        
        
        
    }

    function recalculateIndexesOfMethod(method)
    {
        if (method.ReturnType != null)
        {
            method.ReturnType = getGlobalTypeIndex(method.ReturnType);
        }

        if (method.DeclaringType != null)
        {
            method.DeclaringType = getGlobalTypeIndex(method.DeclaringType);
        }

        {
            const customAttributes = method.CustomAttributes;
            
            const len = customAttributes.length;
            
            for (let i = 0; i < len; i++)
            {
                const customAttribute = customAttributes[i];
                
                customAttribute.Constructor = getGlobalMethodIndex(customAttribute.Constructor);
            }
        }   
        
        const body = method.Body;
        if (body &&  body.Instructions)
        {
            const instructions = body.Instructions;
            const operands = body.Operands;
            
            const len = instructions.length;
            for(let i = 0; i < len; i++)
            {
                const instruction = instructions[i];
                if (instruction === 39)
                {
                    operands[i] = getGlobalMethodIndex( /** @type {number} */ operands[i] );
                }
            }
        }
        
    }
    
    function importTypeDefinitions()
    {
        const globalTypes = GlobalMetadata.Types;
        
        const types = metadata.Types;
        
        const length = types.length;

        for (let i = 0; i < length; i++)
        {
            const type = types[i];
            
            if (!type.IsDefinition)
            {
                continue;
            }
            
            const globalTypeIndex = getGlobalTypeIndex(i);
            if ( globalTypes[globalTypeIndex] === type )
            {
                continue;
            }

            globalTypes[globalTypeIndex] = type;

            recalculateIndexesOfType(type);
        }
    }

    function importMethodDefinitions()
    {
        const globalMethods = GlobalMetadata.Methods;

        const methods = metadata.Methods;

        const length = methods.length;

        for (let i = 0; i < length; i++)
        {
            const method = methods[i];

            if (!method.IsDefinition)
            {
                continue;
            }

            const globalMethodIndex = getGlobalMethodIndex(i);
            if ( globalMethods[globalMethodIndex] === method )
            {
                continue;
            }

            globalMethods[globalMethodIndex] = method;

            recalculateIndexesOfMethod(method);
        }
    }

    /**
     * @param {Metadata} metadata
     * @returns {(index: number) => number}
     */
    function CreateFunction_GetGlobalScopeIndex(metadata)
    {
        const globalScopes = GlobalMetadata.MetadataScopes;

        const cache = {};

        /**
         * @param {MetadataScopeModel} scopeA
         * @param {MetadataScopeModel} scopeB
         */
        const isSameScope = function(scopeA, scopeB)
        {
            return scopeA.Name !== scopeB.Name;
        }

        return function(index)
        {
            const globalIndex = cache[index];
            if ( globalIndex !== undefined )
            {
                return globalIndex;
            }

            const searchValue = metadata.MetadataScopes[index];

            const length = globalScopes.length;

            for (let i = 0; i < length; i++)
            {
                if (isSameScope(globalScopes[i], searchValue))
                {
                    return cache[index] = i;
                }
            }

            cache[index] = globalScopes.length;

            globalScopes.push(searchValue);

            return cache[index];
        }
    }

    /**
     * @param {Metadata} metadata
     * @returns {(index: number) => number}
     */
    function CreateFunction_GetGlobalTypeIndex(metadata)
    {
        const globalTypes = GlobalMetadata.Types;

        const cache = {};

        /**
         * @param {TypeReferenceModel} globalType
         * @param {TypeReferenceModel} type
         */
        const isSameType = function(globalType, type)
        {            
            if ( globalType.Name !== type.Name )
            {
                return false;
            }

            if ( globalType.Namespace !== type.Namespace )
            {
                return false;
            }

            if (globalType.IsGenericInstance)
            {
                if (!type.IsGenericInstance)
                {
                    return false;
                }

                if (globalType.GenericArguments.length  !== type.GenericArguments.length)
                {
                    return false;
                }

                if (globalType.ElementType !== getGlobalTypeIndex(type.ElementType))
                {
                    return false;
                }

                // is GenericArguments Full Same
                let isGenericArgumentsFullSame = 1;
                {
                    const length = type.GenericArguments.length;
                    for ( let i = 0; i < length; i++ )
                    {
                        if (globalType.GenericArguments[i] !== getGlobalTypeIndex(type.GenericArguments[i]))
                        {
                            isGenericArgumentsFullSame = 0;
                            break;
                        }
                    }
                }

                return isGenericArgumentsFullSame === 1;
            }

            return true;
        }

        const getGlobalTypeIndex = function(index)
        {
            const globalIndex = cache[index];
            if ( globalIndex !== undefined )
            {
                return globalIndex;
            }

            const searchValue = metadata.Types[index];

            const length = globalTypes.length;

            for (let i = 0; i < length; i++)
            {
                if (isSameType(globalTypes[i], searchValue))
                {
                    return cache[index] = i;
                }
            }

            cache[index] = globalTypes.length;

            globalTypes.push(searchValue);

            recalculateIndexesOfType(searchValue);

            return cache[index];
        }

        return getGlobalTypeIndex;
    }

    /**
     * @param {Metadata} metadata
     * @returns {(index: number) => number}
     */
    function CreateFunction_GetGlobalMethodIndex(metadata)
    {
        const globalMethods = GlobalMetadata.Methods;

        const cache = {};

        /**
         * @param {MethodReferenceModel} globalMethod
         * @param {MethodReferenceModel} method
         */
        const isSameMethod = function(globalMethod, method)
        {
            if ( globalMethod.Name && globalMethod.Name !== method.Name )
            {
                return false;
            }

            if ( globalMethod.DeclaringType != null && method.DeclaringType == null )
            {
                return false;
            }

            if ( method.DeclaringType != null && globalMethod.DeclaringType == null )
            {
                return false;
            }

            if ( globalMethod.DeclaringType != null && globalMethod.DeclaringType !== getGlobalTypeIndex(method.DeclaringType))
            {
                return false;
            }

            // Is method parameters full match
            {
                const parametersA = globalMethod.Parameters;
                const parametersB = method.Parameters;

                let lengthA = parametersA.length;
                let lengthB = parametersB.length;

                if (lengthA !== lengthB)
                {
                    return false;
                }

                for (let i = 0; i < lengthA; i++)
                {
                    if (parametersA[i].ParameterType !== getGlobalTypeIndex(parametersB[i].ParameterType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        return function(index)
        {
            const globalIndex = cache[index];
            if ( globalIndex !== undefined )
            {
                return globalIndex;
            }

            const searchValue = metadata.Methods[index];

            const length = globalMethods.length;

            for (let i = 0; i < length; i++)
            {
                if (isSameMethod(globalMethods[i], searchValue))
                {
                    return cache[index] = i;
                }
            }

            cache[index] = globalMethods.length;

            globalMethods.push(searchValue);

            recalculateIndexesOfMethod(searchValue);

            return cache[index];
        }
    }


    importTypeDefinitions();
    importMethodDefinitions();
}

function GetDeclaringTypeOfMethod(method)
{
    return GetType(method.Metadata, method.DeclaringType);
}

function GetElementType(type)
{
    return GetType(type.Metadata, type.ElementType);
}

function GetDeclaringTypeIndexOfMethod(method)
{
    return GetGlobalTypeIndex(method.Metadata, method.DeclaringType);
}

function GetGlobalTypeIndex(metadata, index)
{
    if (metadata === GlobalMetadata)
    {
        return index;
    }
    
    const type = metadata.Types[index];    

    const globalTypes = GlobalMetadata.Types;
    
    const indexMap = metadata.Types_GlobalIndexMap = metadata.Types_GlobalIndexMap || {};

    const globalIndex = indexMap[index];

    if ( globalIndex === undefined )
    {
        const length = globalTypes.length;

        for ( let i = 0; i < length; i++ )
        {
            const globalType = globalTypes[i];
            
            if (type.IsGenericInstance)
            {
                if (!globalType.IsGenericInstance)
                {
                    continue;
                }

                if (GetGlobalTypeIndex(type.Metadata, type.ElementType) !== GetGlobalTypeIndex(globalType.Metadata, globalType.ElementType))
                {
                    continue;
                }
                
                if (type.GenericArguments.length  !== globalType.GenericArguments.length)
                {
                    continue;
                }

                // is GenericArguments Full Same
                {
                    let isGenericArgumentsFullSame = 1;
                    {
                        const length = type.GenericArguments.length;
                        for ( let i = 0; i < length; i++ )
                        {
                            if (GetGlobalTypeIndex(type.Metadata, type.GenericArguments[i]) !== GetGlobalTypeIndex(globalType.Metadata, globalType.GenericArguments[i]))
                            {
                                isGenericArgumentsFullSame = 0;
                                break;
                            }
                        }
                    }
                    
                    if (isGenericArgumentsFullSame === 0)
                    {
                        continue;
                    }

                    // found
                    indexMap[index] = i;

                    return i;
                }
                
                
            }

            if ( globalType.Name !== type.Name )
            {
                continue;
            }

            if ( globalType.Namespace !== type.Namespace )
            {
                continue;
            }

            // found
            indexMap[index] = i;

            return i;
        }

        indexMap[index] = globalTypes.length;

        globalTypes.push(type);

        return globalTypes.length - 1;
    }

    return globalIndex;
}

function GetType(metadata, index)
{
    return GlobalMetadata.Types[GetGlobalTypeIndex(metadata, index)];    
}

function GetGlobalMethodIndex(metadata, index)
{
    if (metadata === GlobalMetadata)
    {
        return index;
    }
    
    const method = metadata.Methods[index];
    
    const indexMap = metadata.Methods_GlobalIndexMap = metadata.Methods_GlobalIndexMap || {};

    let globalIndex = indexMap[index];

    if ( globalIndex === undefined )
    {
        const globalMethods = GlobalMetadata.Methods;
        
        const length = globalMethods.length;

        for ( let i = 0; i < length; i++ )
        {
            const globalMethod = globalMethods[i];

            if ( globalMethod.Name !== method.Name )
            {
                continue;
            }

            if ( GetDeclaringTypeOfMethod(globalMethod) !== GetDeclaringTypeOfMethod(method) )
            {
                continue;
            }
            
            if ( !IsTwoMethodParametersFullMatch(globalMethod, method) )
            {
                continue;
            }

            indexMap[index] = i;

            return i;
        }

        globalIndex = indexMap[index] = globalMethods.length;

        globalMethods.push(method);

        return globalIndex;
    }

    return globalIndex;
}

function GetMethod(metadata, index)
{
    return GlobalMetadata.Methods[GetGlobalMethodIndex(metadata, index)];
}

function GetMetadataOfThread(thread)
{
    return thread.LastFrame.Method.Metadata;
}


const InterpreterBridge_NewArr = 0;
const InterpreterBridge_NullReferenceException = 1;
const InterpreterBridge_ArgumentNullException = 2;
const InterpreterBridge_DivideByZeroException = 3;
const InterpreterBridge_IndexOutOfRangeException = 4;
const InterpreterBridge_MissingMethodException = 5;


function AssertNotNull(value)
{
    if (value == null)
    {
        throw 'AssertNotNull';
    }
}


const InterpreterBridge_Jump = 219;

let InterpreterBridge_Jump_MethodDefinition;

function TryInitialize_InterpreterBridge(metadataTable) 
{
    for (let i = 0; i < metadataTable.Methods.length; i++)
    {
        if (metadataTable.Methods[i].IsDefinition === true)
        {
            if (metadataTable.Types[metadataTable.Methods[i].DeclaringType].Name === 'InterpreterBridge' &&
                metadataTable.Types[metadataTable.Methods[i].DeclaringType].Namespace === 'ReactWithDotNet')
            {
                if (metadataTable.Methods[i].Name === "Jump")
                {
                    InterpreterBridge_Jump_MethodDefinition = metadataTable.Methods[i];
                }
            }
        }
    }    
}

function NotImplementedOpCode()
{
    throw 'NotImplementedOpCode';
}
function Interpret(thread)
{
    const AllTypes = GlobalMetadata.Types;
    const AllMethods = GlobalMetadata.Methods;
    
    let currentStackFrame = thread.LastFrame;
    
    let instructions = currentStackFrame.Method.Body.Instructions;
    let operands     = currentStackFrame.Method.Body.Operands;

    let evaluationStack = currentStackFrame.EvaluationStack;
    let localVariables  = currentStackFrame.LocalVariables;
    
    let methodArguments       = currentStackFrame.MethodArguments;
    let methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

    let nextInstruction = instructions[currentStackFrame.Line];

    while(true)
    {
        try
        {
            switch (nextInstruction)
            {
                case 0: // Nop: No operation
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 1: // Break: Inform debugger of a break point
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 2: // Ldarg_0: Load argument 0 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 0]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 3: // Ldarg_1: Load argument 1 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 1]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 4: // Ldarg_2: Load argument 2 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 2]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 5: // Ldarg_3: Load argument 3 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 3]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 6: // Ldloc_0: Load local variable 0 onto the stack
                {
                    evaluationStack.push(localVariables[0]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 7: // Ldloc_1: Load local variable 1 onto the stack
                {
                    evaluationStack.push(localVariables[1]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 8: // Ldloc_2: Load local variable 2 onto the stack
                {
                    evaluationStack.push(localVariables[2]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 9: // Ldloc_3: Load local variable 3 onto the stack
                {
                    evaluationStack.push(localVariables[3]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 10: // Stloc_0: Store value from the stack in local variable 0
                {
                    localVariables[0] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 11: // Stloc_1: Store value from the stack in local variable 1
                {
                    localVariables[1] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 12: // Stloc_2: Store value from the stack in local variable 2
                {
                    localVariables[2] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 13: // Stloc_3: Store value from the stack in local variable 3
                {
                    localVariables[3] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 14: // Ldarg_S: Load argument at a specified index (short form)
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + operands[currentStackFrame.Line]]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 15: // Ldarga_S: Load address of argument at a specified index (short form)
                {
                    evaluationStack.push({
                        $isAddress: 1,
                        $object: methodArguments,
                        $key: methodArgumentsOffset + operands[currentStackFrame.Line]
                    });
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 16: // Starg_S: Store value from the stack into argument at specified index
                {
                    methodArguments[methodArgumentsOffset + operands[currentStackFrame.Line]] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 17: // Ldloc_S: Load local variable at a specified index (short form)
                {
                    evaluationStack.push(localVariables[operands[currentStackFrame.Line]]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 18: // Ldloca_S: Load address of local variable at a specified index (short)
                {
                    evaluationStack.push({
                        $isAddress: 1,
                        $object: localVariables,
                        $key: operands[currentStackFrame.Line]
                    });
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 19: // Stloc_S: Store value from the stack into local variable at specified index
                {
                    localVariables[operands[currentStackFrame.Line]] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 20: // Ldnull: Push a null reference onto the stack
                {
                    evaluationStack.push(null);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 21: // Ldc_I4_M1: Load integer constant -1 onto the stack
                {
                    evaluationStack.push(-1);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 22: // Ldc_I4_0: Load integer constant 0 onto the stack
                {
                    evaluationStack.push(0);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 23: // Ldc_I4_1: Load integer constant 1 onto the stack
                {
                    evaluationStack.push(1);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 24: // Ldc_I4_2: Load integer constant 2 onto the stack
                {
                    evaluationStack.push(2);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 25: // Ldc_I4_3: Load integer constant 3 onto the stack
                {
                    evaluationStack.push(3);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 26: // Ldc_I4_4: Load integer constant 4 onto the stack
                {
                    evaluationStack.push(4);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 27: // Ldc_I4_5: Load integer constant 5 onto the stack
                {
                    evaluationStack.push(5);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 28: // Ldc_I4_6: Load integer constant 6 onto the stack
                {
                    evaluationStack.push(6);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 29: // Ldc_I4_7: Load integer constant 7 onto the stack
                {
                    evaluationStack.push(7);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 30: // Ldc_I4_8: Load integer constant 8 onto the stack
                {
                    evaluationStack.push(8);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 31: // Ldc_I4_S: Load 4-byte integer constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 32: // Ldc_I4: Load 8-byte integer constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 33: // Ldc_I8: Load 4-byte floating-point constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 34: // Ldc_R4: Load 8-byte floating-point constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 35: // Ldc_R8: Load 8-byte floating-point constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 36: // Dup: Duplicate the value on top of the stack
                {
                    evaluationStack.push(evaluationStack[evaluationStack.length - 1]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 37: // Pop: Remove the value from the top of the stack
                {
                    evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 38: // Jmp
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 39: // Call
                {
                    let method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];

                    if (method.IsGenericInstance)
                    {
                        let elementMethod = GetMethod(method.Metadata, method.ElementMethod);

                        method.Body       = elementMethod.Body;
                        method.Parameters = elementMethod.Parameters;
                        method.IsStatic   = elementMethod.IsStatic;
                    }
                    
                    // call instance bool value type [System.Runtime]System.Nullable`1<int32>::get_HasValue()
                    if (!method.Body)
                    {
                        let declaringType = GlobalMetadata.Types[method.DeclaringType];
                        if (declaringType.IsGenericInstance)
                        {
                            let realMethod = null;
                            {
                                let elementType = GetElementType(declaringType);

                                let methods = elementType.Methods;
                                let length = methods.length;
                                for ( let i = 0; i < length; i++ )
                                {
                                    let methodDefinitionModel =  GetMethod(elementType.Metadata, methods[i]);
                                    if (methodDefinitionModel.Name === method.Name)
                                    {
                                        realMethod = methodDefinitionModel;
                                        break;
                                    }
                                }                                
                            }
                            
                            if (realMethod)
                            {
                                method.Body         = realMethod.Body;
                                method.Parameters   = realMethod.Parameters;
                                method.IsStatic     = realMethod.IsStatic;
                                method.IsDefinition = realMethod.IsDefinition;

                                if (method.Name === '.ctor')
                                {
                                    let elementType = GetElementType(declaringType);
                                    if (elementType.IsValueType)
                                    {
                                        let instanceIndex = evaluationStack.length - realMethod.Parameters.length - 1;

                                        let address = evaluationStack[instanceIndex];

                                        address.$object[address.$key] = {
                                            $typeIndex: method.DeclaringType
                                        };
                                    }
                                }
                            }
                        }
                    }

                    if (!method.IsDefinition)
                    {
                        // Load method at runtime
                        evaluationStack.push(method);
                        nextInstruction = 222;
                        break;
                    }

                    if (!method.Body)
                    {
                        NotImplementedOpCode();
                    }
                    // arrange arguments
                    methodArguments = evaluationStack;
                    methodArgumentsOffset = evaluationStack.length - method.Parameters.length;
                    if (method.IsStatic === false)
                    {
                        // 0: this
                        methodArgumentsOffset--;
                    }

                    // arrange opcodes
                    instructions = method.Body.Instructions;
                    operands = method.Body.Operands;
                    
                    // maybe external method
                    if (instructions.length === 0)
                    {
                        const declaringType = AllTypes[method.DeclaringType];
                        
                        let isDeclaringTypeExternal = false;
                        {
                            let customAttributes = declaringType.CustomAttributes;

                            let length = customAttributes.length;

                            for (let i= 0; i < length; i++)
                            {
                                let attribute = customAttributes[i];

                                let constructor = AllMethods[attribute.Constructor]
                                
                                let declaringTypeOfConstructor = AllTypes[constructor.DeclaringType];
                                if (declaringTypeOfConstructor.Name === 'ExternalAttribute' &&
                                    declaringTypeOfConstructor.Namespace === 'ReactWithDotNet')
                                {
                                    isDeclaringTypeExternal = true;
                                    break;
                                }
                            }
                        }
                        
                        if (isDeclaringTypeExternal === true)
                        {
                            let instance = null;
                            
                            let fnArguments = [];
                            
                            // move arguments to fnArguments
                            {
                                let length = method.Parameters.length;
                                while (length-- > 0)
                                {
                                    fnArguments.push(evaluationStack.pop());
                                }

                                fnArguments.reverse();
                            }

                            let isVoid = false;
                            {
                                let returnType = AllTypes[method.ReturnType];
                                
                                isVoid = returnType.Namespace === 'System' && returnType.Name === 'Void';
                            }

                            if (method.IsStatic === false)
                            {
                                instance = evaluationStack.pop();
                            }
                            
                            let fn = null;
                            
                            if (method.IsStatic === true)
                            {
                                let externalType = window[declaringType.Name];

                                fn = externalType[method.Name];                                
                            }
                            
                            if (fn == null)
                            {
                                evaluationStack.push(method);
                                evaluationStack.push(InterpreterBridge_MissingMethodException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            let fnResult = fn.apply(null, fnArguments);

                            if (isVoid === false)
                            {
                                evaluationStack.push(fnResult);
                            }
                            
                            // arrange fast access variables
                            instructions = currentStackFrame.Method.Body.Instructions;
                            operands = currentStackFrame.Method.Body.Operands;

                            // arrange fast access variables
                            methodArguments = currentStackFrame.MethodArguments;
                            methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

                            nextInstruction = instructions[++currentStackFrame.Line];
                            break;
                        }                                          
                    }

                    // arrange calculation stacks
                    evaluationStack = [];
                    localVariables = [];

                    // connect frame
                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: method,
                        Line: 0,

                        MethodArguments: methodArguments,
                        MethodArgumentsOffset: methodArgumentsOffset,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables
                    };

                    nextInstruction = instructions[0];                    
                    break;
                }                  

                case 40: // Calli
                {
                    NotImplementedOpCode(); break;
                }

                case 41: // Ret
                {
                    // exit thread
                    if (currentStackFrame.Prev === null)
                    {
                        if (evaluationStack.length > 0)
                        {
                            const returnValue = evaluationStack.pop();

                            thread.OnSuccess(returnValue);
                        }
                        
                        return;
                    }

                    let previousStackFrame = currentStackFrame;

                    thread.LastFrame = currentStackFrame = currentStackFrame.Prev;

                    // arrange fast access variables
                    instructions = currentStackFrame.Method.Body.Instructions;
                    operands = currentStackFrame.Method.Body.Operands;

                    // arrange fast access variables
                    evaluationStack = currentStackFrame.EvaluationStack;
                    localVariables = currentStackFrame.LocalVariables;
                    methodArguments = currentStackFrame.MethodArguments;
                    methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

                    // remove parameters
                    let length = previousStackFrame.Method.Parameters.length;
                    while (length-- > 0)
                    {
                        evaluationStack.pop();
                    }

                    // remove instance
                    if (previousStackFrame.Method.IsStatic === false)
                    {
                        evaluationStack.pop();
                    }

                    // check has any return value
                    if (previousStackFrame.EvaluationStack.length === 1)
                    {
                        evaluationStack.push(previousStackFrame.EvaluationStack.pop());
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 42: // Br_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 55;
                    break;
                }
                
                case 43: // Brfalse_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 56;
                    break;
                }
                
                case 44: // Brtrue_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 57;
                    break;
                }
                
                case 45: // Beq_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 58;
                    break;
                }
                
                case 46: // Bge_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 59;
                    break;
                }

                case 47: // Bgt_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 60;
                    break;
                }                    

                case 48: // Ble_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 61;
                    break;
                }
                
                case 49: // Blt_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 62;
                    break;
                }
                
                case 50: // Bne_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 63;
                    break;
                }
                case 51: // Bge_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 64;
                    break;
                }
                case 52: // Bgt_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 65;
                    break;
                }
                case 53: // Ble_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 66;
                    break;
                }
                case 54: // Blt_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 67;
                    break;
                }
                    
                case 55: // Br: Unconditionally transfers control to a target instruction.
                {
                    currentStackFrame.Line = operands[currentStackFrame.Line];
                    nextInstruction = instructions[currentStackFrame.Line];
                    break;
                }    
                
                case 56: // Brfalse: Transfers control to a target instruction if value is false, a null reference, or zero.
                {
                    let value = evaluationStack.pop();
                    if (value)
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }
                    else
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }

                    break;
                }
                
                case 57: // Brtrue: Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
                {
                    let value = evaluationStack.pop();
                    if (value)
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }

                    break;
                }
                
                case 58: // Beq: Transfers control to a target instruction if two values are equal.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 === value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 59: // Bge: Transfers control to a target instruction if the first value is greater than or equal to the second value.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 >= value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                    
                case 60: // Bgt: Transfers control to a target instruction if the first value is greater than the second value.
                {   
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 > value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 61: // Ble: Transfers control to a target instruction if the first value is less than or equal to the second value.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 <= value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 62: // Blt: Transfers control to a target instruction if the first value is less than the second value.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 < value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 63: // Bne_Un: Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        // use >>> for support UInt32
                        // NaN >>> 0 is 0 and NaN === NaN is false
                        // Infinity >>> 0 and Infinity === Infinity is true
                        
                        if (value0 === value1 || value0 >>> 0 === value1 >>> 0)
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }
                        else
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 64: // Bge_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 65: // Bgt_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 66: // Ble_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 67: // Blt_Un
                {
                    NotImplementedOpCode(); break;
                }
                
                case 68: // Switch: Implements a jump table.
                {
                    let jumpTable = operands[currentStackFrame.Line];
                    
                    let caseIndex = evaluationStack.pop();
                    
                    if ( caseIndex > 0 && jumpTable.length > caseIndex)
                    {
                        currentStackFrame.Line = jumpTable[caseIndex];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }
                    
                    break;
                }
                    
                case 69: // Ldind_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 70: // Ldind_U1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 71: // Ldind_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 72: // Ldind_U2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 73: // Ldind_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 74: // Ldind_U4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 75: // Ldind_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 76: // Ldind_I
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 77: // Ldind_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 78: // Ldind_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 79: // Ldind_Ref: Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.
                {
                    let address = evaluationStack.pop();
                    
                    evaluationStack.push(address.$object[address.$key]);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 80: // Stind_Ref: Stores  object reference value at a supplied address.
                {
                    let value   = evaluationStack.pop();
                    let address = evaluationStack.pop();

                    address.$object[address.$key] = value;

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                    
                case 81: // Stind_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 82: // Stind_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 83: // Stind_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 84: // 'Stind_I8'
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 85: // Stind_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 86: // Stind_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                
                case 87: // Add
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 + value1);
                        
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 88: // Sub
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 - value1);
                        
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 89: // Mul
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 * value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 90: // Div
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value1 === 0)
                        {
                            evaluationStack.push(value0 + ' cannot divide by zero');
                            evaluationStack.push(InterpreterBridge_DivideByZeroException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        
                        evaluationStack.push(value0 / value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 91: // Div_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 92: // Rem
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 % value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 93: // Rem_Un
                {
                    NotImplementedOpCode(); break;
                }
                
                case 94: // And
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 & value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }

                case 95: // Or
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 | value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }

                case 96: // Xor
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 ^ value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 97: // Shl
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 << value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 98: // Shr
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 >> value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 99: // Shr_Un
                {
                    NotImplementedOpCode(); break;
                }
                
                case 100: // Neg
                {
                    let value = evaluationStack.pop();

                    if (typeof value === 'number')
                    {
                        evaluationStack.push(value * -1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 101: // Not
                {
                    let value = evaluationStack.pop();

                    if (typeof value === 'number')
                    {
                        evaluationStack.push(~value);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 102: // Conv_I1
                {
                    let value = evaluationStack.pop();
    
                    if (typeof value === 'number')
                    {
                        if (value > /*Int32.MaxValue*/2147483647 || value < /*Int32.MinValue*/ -2147483648)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = /*Number_To_Int8*/~~(value << 24) >> 24;
                        }
                        
                        evaluationStack.push(value);
                        
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 103: // Conv_I2
                {
                    let value = evaluationStack.pop();

                    if (typeof value === 'number')
                    {
                        if (value > /*Int32.MaxValue*/2147483647 || value < /*Int32.MinValue*/ -2147483648)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = /*Number_To_Int16*/~~(value << 16) >> 16;
                        }

                        evaluationStack.push(value);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }

                case 104: // Conv_I4
                {
                    let value = evaluationStack.pop();
                    
                    if (typeof value === 'number')
                    {
                        if (value >= 0)
                        {
                            if (value > /*Int32.MaxValue*/2147483647)
                            {
                                value = /*Int32.MinValue*/ -2147483648;
                            }
                            else
                            {
                                value = value << 0;
                            }
                        }
                        else
                        {
                            if (value < /*Int32.MinValue*/ -2147483648)
                            {
                                value = /*Int32.MinValue*/ -2147483648;
                            }
                            else
                            {
                                value = Math.ceil(value);
                            }
                        }

                        evaluationStack.push(value);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }

                    NotImplementedOpCode(); break;                   
                }
                
                case 105: // Conv_I8
                {
                    let value = evaluationStack.pop();

                    evaluationStack.push(Long.fromNumber(value));

                    nextInstruction = instructions[++currentStackFrame.Line];                    
                    break;
                }
                
                case 106: // Conv_R4
                {
                    NotImplementedOpCode(); break;
                }
                case 107: // Conv_R8
                {
                    NotImplementedOpCode(); break;
                }
                case 108: // Conv_U4
                {
                    NotImplementedOpCode(); break;
                }
                case 109: // Conv_U8
                {
                    NotImplementedOpCode(); break;
                }
                case 110: // Callvirt
                {
                    let method = GetMethod(GetMetadataOfThread(thread), operands[currentStackFrame.Line]);
                
                    let methodParameterCount = method.Parameters.length;

                    // arrange arguments
                    methodArguments      = evaluationStack;
                    methodArgumentsOffset = evaluationStack.length - methodParameterCount - 1;

                    let instance = methodArguments[methodArgumentsOffset];

                    if (instance == null)
                    {
                        // NullReferenceException
                        evaluationStack.push(method.Name);
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }
                    
                    AssertNotNull(instance.$typeIndex);

                    // find target method

                    let targetMethod = null;
                    {
                        let type = GlobalMetadata.Types[instance.$typeIndex];
                        while (type)
                        {
                            let typeMethods = type.Methods;
                            let typeMethodsLength = typeMethods.length;

                            for (let i = 0; i < typeMethodsLength; i++)
                            {
                                targetMethod = GetMethod(type.Metadata, typeMethods[i]);

                                if (targetMethod.Name !== method.Name)
                                {
                                    continue;
                                }

                                if (IsTwoMethodParametersFullMatch(method, targetMethod) === false)
                                {
                                    continue;
                                }

                                method = targetMethod;
                                break;
                            }

                            if (method === targetMethod)
                            {
                                break;
                            }
                            
                            if (type.BaseType >= 0)
                            {
                                type = GetType(type.Metadata, type.BaseType);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (method !== targetMethod)
                    {
                        evaluationStack.push(method);
                        evaluationStack.push(InterpreterBridge_MissingMethodException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    // arrange opcodes
                    instructions = method.Body.Instructions;
                    operands     = method.Body.Operands;
                                
                    // arrange calculation stacks
                    evaluationStack = [];
                    localVariables  = [];
                              
                    // connect frame
                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: method,
                        Line: 0,

                        MethodArguments: methodArguments,
                        MethodArgumentsOffset: methodArgumentsOffset,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables                    
                    };
                    nextInstruction = instructions[0];
                    break;
                }   
                case 111: // Cpobj
                {
                    NotImplementedOpCode(); break;
                }
                case 112: // Ldobj
                {
                    NotImplementedOpCode(); break;
                }
                case 113: // Ldstr: Load string
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 114: // Newobj
                {
                    let method = AllMethods[ operands[currentStackFrame.Line] ];

                    let declaringType = AllTypes[method.DeclaringType];

                    if (declaringType.IsGenericInstance)
                    {
                        let elementType = GetElementType(declaringType);

                        for (let i = 0; i < elementType.Methods.length; i++)
                        {
                            let methodTemp = GetMethod(elementType.Metadata, elementType.Methods[i]);

                            if (methodTemp.IsDefinition)
                            {
                                if (methodTemp.Name === method.Name)
                                {
                                    method.Body       = methodTemp.Body;
                                    method.Parameters = methodTemp.Parameters;
                                    method.IsStatic   = methodTemp.IsStatic;
                                }
                            }
                        }
                    }

                    if (!method.IsDefinition)
                    {
                        // Load method at runtime
                        evaluationStack.push(declaringType);
                        nextInstruction = 223;
                        break;
                    }
                    
                    let newObj = {};
                    newObj.$typeIndex = method.DeclaringType;
                    
                    let tempArray = [];

                    for (let i = 0; i < method.Parameters.length; i++)
                    {
                        tempArray.push(evaluationStack.pop());
                    }

                    tempArray.push(newObj);
                    tempArray.push(newObj);

                    while(tempArray.length > 0)
                    {
                        evaluationStack.push(tempArray.pop());
                    }

                    instructions = method.Body.Instructions;
                    operands     = method.Body.Operands;

                    evaluationStack = [];
                    methodArguments = currentStackFrame.EvaluationStack;
                    methodArgumentsOffset = methodArguments.length - method.Parameters.length;
                    methodArgumentsOffset--;
                    localVariables  = [];

                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: method,
                        Line: 0,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables,
                        MethodArguments: methodArguments,
                        MethodArgumentsOffset: methodArgumentsOffset
                    };

                    nextInstruction = instructions[0];

                    break;
                }
                    
                case 115: // Castclass
                {
                    NotImplementedOpCode(); break;
                }
                case 116: // Isinst
                {
                    let value = evaluationStack.pop();
                    if ( value == null)
                    {
                        evaluationStack.push(null);
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }

                    if (value.$isBox)
                    {
                        if (operands[currentStackFrame.Line] === value.$typeIndex)
                        {
                            evaluationStack.push(value.rawValue);
                            nextInstruction = instructions[++currentStackFrame.Line];
                            break;
                        }
                    }
                    
                    NotImplementedOpCode(); break;
                }
                case 117: // Conv_R_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 118: // Unbox
                {
                    NotImplementedOpCode(); break;
                }
                case 119: // Throw: Throws the exception object currently on the evaluation stack.
                {
                    ++currentStackFrame.Line;

                    nextInstruction = 221;
                    
                    break;
                }

                case 120: // Ldfld
                {
                    let fieldReference = GetMetadataOfThread(thread).Fields[operands[currentStackFrame.Line]];
                                
                    let instance = evaluationStack.pop();

                    if (instance == null)
                    {
                        // NullReferenceException
                        evaluationStack.push(fieldReference.Name);
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    if (instance.$isAddress)
                    {
                        instance = instance.$object[instance.$key];
                    }

                    let value = instance[fieldReference.Name];

                    if (typeof value === 'boolean')
                    {
                        value = value ? 1 : 0;
                    }

                    evaluationStack.push(value);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                case 121: // Ldflda
                {
                    let fieldReference = GetMetadataOfThread(thread).Fields[operands[currentStackFrame.Line]];
                    
                    let instance = evaluationStack.pop();

                    evaluationStack.push({
                        $isAddress: 1,
                        $object: instance,
                        $key: fieldReference.Name
                    });
                    
                    break;
                }
                case 122: // Stfld
                {    
                    let fieldReference = GetMetadataOfThread(thread).Fields[operands[currentStackFrame.Line]];
                                    
                    let value    = evaluationStack.pop();
                    let instance = evaluationStack.pop();

                    if (instance == null)
                    {
                        // NullReferenceException
                        evaluationStack.push(fieldReference.Name);
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }
                    
                    if (instance.$isAddress)
                    {
                        instance = instance.$object[instance.$key];
                    }

                    instance[fieldReference.Name] = value;

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                case 123: // Ldsfld
                {
                    NotImplementedOpCode(); break;
                }
                case 124: // Ldsflda
                {
                    NotImplementedOpCode(); break;
                }
                case 125: // Stsfld
                {
                    NotImplementedOpCode(); break;
                }
                case 126: // Stobj
                {
                    NotImplementedOpCode(); break;
                }
                case 127: // Conv_Ovf_I1_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 128: // Conv_Ovf_I2_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 129: // Conv_Ovf_I4_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 130: // Conv_Ovf_I8_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 131: // Conv_Ovf_U1_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 132: // Conv_Ovf_U2_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 133: // Conv_Ovf_U4_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 134: // Conv_Ovf_U8_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 135: // Conv_Ovf_I_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 136: // Conv_Ovf_U_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 137: // Box
                {
                    let value = evaluationStack.pop();
                    
                    evaluationStack.push({
                        $isBox: 1,
                        $typeIndex: operands[currentStackFrame.Line],
                        rawValue: value                        
                    });

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                case 138: // Newarr
                {
                    evaluationStack.push(InterpreterBridge_NewArr);
                    nextInstruction = InterpreterBridge_Jump;
                    break;
                }
                case 139: // Ldlen
                {
                    let array = evaluationStack.pop();

                    evaluationStack.push(array.length);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                    
                case 140: // Ldelema
                {
                    NotImplementedOpCode(); break;
                }
                case 141: // Ldelem_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 142: // Ldelem_U1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 143: // Ldelem_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 144: // Ldelem_U2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 145: // Ldelem_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 146: // Ldelem_U4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 147: // Ldelem_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 148: // Ldelem_I
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 149: // Ldelem_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 150: // Ldelem_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 151: // Ldelem_Ref
                {
                    let index = evaluationStack.pop();
                    let array = evaluationStack.pop();

                    if ( array == null)
                    {
                        evaluationStack.push('@array is null when load index');
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }
                    
                    if (array.length <= index || index < 0)
                    {
                        evaluationStack.push('@array.length:' + array.length + ' @index: ' + index);
                        evaluationStack.push(InterpreterBridge_IndexOutOfRangeException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    evaluationStack.push(array[index]);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                    
                case 152: // Stelem_I
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 153: // Stelem_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 154: // Stelem_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 155: // Stelem_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 156: // Stelem_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 157: // Stelem_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 158: // Stelem_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 159: // Stelem_Ref
                {
                    let value = evaluationStack.pop();
                    let index = evaluationStack.pop();
                    let array = evaluationStack.pop();

                    if ( array == null)
                    {
                        evaluationStack.push('@array is null when set index');
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    if (array.length <= index || index < 0)
                    {
                        evaluationStack.push('@array.length:' + array.length + ' @index: ' + index);
                        evaluationStack.push(InterpreterBridge_IndexOutOfRangeException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    array[index] = value;

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 160: // Ldelem_Any
                {
                    NotImplementedOpCode(); break;
                }

                case 161: // Stelem_Any
                {
                    NotImplementedOpCode(); break;
                }

                case 162: // Unbox_Any
                {
                    let box = evaluationStack.pop();
                    if ( box == null)
                    {
                        NotImplementedOpCode(); break;
                    }

                    if (box.$isBox)
                    {
                        if (operands[currentStackFrame.Line] === box.$typeIndex)
                        {
                            evaluationStack.push(box.rawValue);
                            nextInstruction = instructions[++currentStackFrame.Line];
                            break;
                        }
                    }
                    
                    NotImplementedOpCode(); break;
                }

                case 163: // Conv_Ovf_I1
                {
                    NotImplementedOpCode(); break;
                }

                case 164: // Conv_Ovf_U1
                {
                    NotImplementedOpCode(); break;
                }

                case 165: // Conv_Ovf_I2
                {
                    NotImplementedOpCode(); break;
                }

                case 166: // Conv_Ovf_U2
                {
                    NotImplementedOpCode(); break;
                }

                case 167: // Conv_Ovf_I4
                {
                    NotImplementedOpCode(); break;
                }

                case 168: // Conv_Ovf_U4
                {
                    NotImplementedOpCode(); break;
                }

                case 169: // Conv_Ovf_I8
                {
                    NotImplementedOpCode(); break;
                }

                case 170: // Conv_Ovf_U8
                {
                    NotImplementedOpCode(); break;
                }

                case 171: // Refanyval
                {
                    NotImplementedOpCode(); break;
                }

                case 172: // Ckfinite
                {
                    NotImplementedOpCode(); break;
                }

                case 173: // Mkrefany
                {
                    NotImplementedOpCode(); break;
                }

                case 174: // Ldtoken
                {
                    NotImplementedOpCode(); break;
                }

                case 175: // Conv_U2
                {
                    NotImplementedOpCode(); break;
                }

                case 176: // Conv_U1
                {
                    NotImplementedOpCode(); break;
                }

                case 177: // Conv_I
                {
                    NotImplementedOpCode(); break;
                }

                case 178: // Conv_Ovf_I
                {
                    NotImplementedOpCode(); break;
                }

                case 179: // Conv_Ovf_U
                {
                    NotImplementedOpCode(); break;
                }

                case 180: // Add_Ovf
                {
                    NotImplementedOpCode(); break;
                }

                case 181: // Add_Ovf_Un
                {
                    NotImplementedOpCode(); break;
                }

                case 182: // Mul_Ovf
                {
                    NotImplementedOpCode(); break;
                }

                case 183: // Mul_Ovf_Un
                {
                    NotImplementedOpCode(); break;
                }

                case 184: // Sub_Ovf
                {
                    NotImplementedOpCode(); break;
                }

                case 185: // Sub_Ovf_Un
                {
                    NotImplementedOpCode(); break;
                }

                case 186: // Endfinally
                {
                    if( thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock !== null )
                    {
                        let exception = thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock;
                        
                        thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock = null;

                        // handle exception
                        evaluationStack.push(exception);
                        nextInstruction = 221;
                        break;
                    }
                    
                    let line = currentStackFrame.Line;
                    
                    if (thread.LeaveJumpIndex !== null)
                    {
                        line = thread.LeaveJumpIndex;
                        thread.LeaveJumpIndex = null;
                    }
                    else
                    {
                        line++;
                    }

                    // branch
                    currentStackFrame.Line = line;
                    nextInstruction = instructions[line];
                    
                    break;
                }

                case 187: // Leave: Exits a protected region of code, unconditionally transferring control to a specific target instruction.
                {   
                    const Catch = 0;
                    const Finally = 2;

                    /**
                     * @type {ExceptionHandler[]}
                     */
                    let exceptionHandlers = currentStackFrame.Method.Body.ExceptionHandlers;
                    let exceptionHandlersLength = exceptionHandlers.length;
                    
                    let line = currentStackFrame.Line;
                    
                    let finallyFound = false;

                    for (let i = 0; i < exceptionHandlersLength; i++)
                    {
                        let exceptionHandler = exceptionHandlers[i];
                        
                        if (exceptionHandler.HandlerType === Catch)
                        {
                            // is successfully passed protected region code
                            if( line + 1 === exceptionHandler.HandlerStart  )
                            {
                                break;
                            }
                        }
                        else if (exceptionHandler.HandlerType === Finally)
                        {
                            if( line <= exceptionHandler.HandlerEnd)
                            {
                                if( line + 1 >= exceptionHandler.HandlerStart)
                                {
                                    thread.LeaveJumpIndex = operands[line];
                                    finallyFound = true;
                                    line = exceptionHandler.HandlerStart;
                                    break;
                                }
                            }
                        }
                    }

                    if( finallyFound === true )
                    {
                        currentStackFrame.Line = line;
                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        // nextInstruction = instructions[++currentStackFrame.Line];

                        // branch
                        currentStackFrame.Line = operands[line];
                        nextInstruction = instructions[currentStackFrame.Line];
                    }

                    break;
                }

                case 188: // Leave_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 187;
                    break;                   
                }  

                case 189: // Stind_I
                {
                    NotImplementedOpCode(); break;
                }

                case 190: // Conv_U
                {
                    NotImplementedOpCode(); break;
                }

                case 191: // Arglist
                {
                    NotImplementedOpCode(); break;
                }

                case 192: // Ceq: Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    let result = null;
                    
                    if (typeof value0 === 'number' && typeof value1 === 'number' ||
                        typeof value0 === 'string' && typeof value1 === 'string')
                    {
                        result = value0 === value1 ? 1 : 0;
                    }
                    else if (Long.isLong(value0) && Long.isLong(value1))
                    {
                        result = value0.compare( value1 ) === 0 ? 1 : 0;
                    }
                    else if ( value0 === undefined )
                    {
                        result = value1 == null ? 1 : 0;
                    }
                    else if ( value1 === undefined )
                    {
                        result = value0 == null ? 1 : 0;
                    }
                    
                    if (result !== null)
                    {
                        evaluationStack.push(result);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    
                    NotImplementedOpCode(); break;
                }

                case 193: // Cgt: If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value1 < value0 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value1.lessThan(value0) ? 1 : 0);
                    }
                    
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 194: // Cgt_Un
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value1 < value0 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value1.lessThan(value0) ? 1 : 0);
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 195: // Clt
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 < value1 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value0.lessThan(value1) ? 1 : 0);
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 196: // Clt_Un
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 < value1 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value0.lessThan(value1) ? 1 : 0);
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 197: // Ldftn
                {
                    NotImplementedOpCode(); break;
                }

                case 198: // Ldvirtftn
                {
                    NotImplementedOpCode(); break;
                }

                case 199: // Ldarg
                {
                    NotImplementedOpCode(); break;
                }

                case 200: // Ldarga
                {
                    NotImplementedOpCode(); break;
                }

                case 201: // Starg
                {
                    NotImplementedOpCode(); break;
                }

                case 202: // Ldloc
                {
                    NotImplementedOpCode(); break;
                }

                case 203: // Ldloca
                {
                    NotImplementedOpCode(); break;
                }

                case 204: // Stloc
                {
                    NotImplementedOpCode(); break;
                }

                case 205: // Localloc
                {
                    NotImplementedOpCode(); break;
                }

                case 206: // Endfilter
                {
                    NotImplementedOpCode(); break;
                }

                case 207: // Unaligned
                {
                    NotImplementedOpCode(); break;
                }

                case 208: // Volatile
                {
                    NotImplementedOpCode(); break;
                }

                case 209: // Tail
                {
                    NotImplementedOpCode(); break;
                }

                case 210: // Initobj: Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
                {
                    //The address is popped from the stack; the value type object at the specified address is initialized as type
                    
                    let typeIndex = operands[currentStackFrame.Line];
                    
                    let typeReference = GetType(GetMetadataOfThread(thread), typeIndex);

                    let address = evaluationStack.pop();
                    
                    if (!typeReference.IsValueType)
                    {                
                        address.$object[address.$key] = null;
                    }
                    else
                    {
                        address.$object[address.$key] = {
                            $typeIndex: typeIndex
                        };
                    }                    

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 211: // Constrained
                {
                    NotImplementedOpCode(); break;
                }

                case 212: // Cpblk
                {
                    NotImplementedOpCode(); break;
                }

                case 213: // Initblk
                {
                    NotImplementedOpCode(); break;
                }

                case 214: // No
                {
                    NotImplementedOpCode(); break;
                }

                case 215: // Rethrow
                {
                    NotImplementedOpCode(); break;
                }

                case 216: // Sizeof
                {
                    NotImplementedOpCode(); break;
                }

                case 217: // Refanytype
                {
                    NotImplementedOpCode(); break;
                }

                case 218: // Readonly
                {
                    NotImplementedOpCode(); break;
                }

                case 219: // Jump
                {
                    instructions = InterpreterBridge_Jump_MethodDefinition.Body.Instructions;
                    operands     = InterpreterBridge_Jump_MethodDefinition.Body.Operands;

                    evaluationStack = [];
                    localVariables  = [];

                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: InterpreterBridge_Jump_MethodDefinition,
                        Line: 0,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables
                    };

                    nextInstruction = instructions[currentStackFrame.Line];

                    break;
                }                    

                case 220: // CallJsNative
                {
                    switch(operands[currentStackFrame.Line])
                    {
                        // set
                        case 0:
                        {
                            let value    = evaluationStack.pop();
                            let key      = evaluationStack.pop();
                            let instance = evaluationStack.pop();

                            instance[key] = value;

                            break;
                        }

                        // get
                        case 1:
                        {
                            let key      = evaluationStack.pop();
                            let instance = evaluationStack.pop();

                            evaluationStack.push( instance[key] );

                            break;
                        }

                        // CreateNewPlainObject
                        case 2:
                        {
                            evaluationStack.push({});
                            break;
                        }

                        // CreateNewArray
                        case 3:
                        {
                            evaluationStack.push([]);
                            break;
                        }

                        // Sum
                        case 4:
                        {
                            let value1 = evaluationStack.pop();
                            let value0 = evaluationStack.pop();

                            evaluationStack.push( value0 + value1 );

                            break;
                        }

                        case 5: // PreviousStackFrame
                        {
                            evaluationStack.push(currentStackFrame.Prev);
                            break;
                        }

                        // instance.Call('functionName')
                        case 6:
                        {
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]());

                            break;
                        }


                        // instance.Call('functionName', parameter1)
                        case 7:
                        {
                            let parameter1   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]( parameter1 ));

                            break;
                        }


                        // instance.Call('functionName', parameter1, parameter2)
                        case 8:
                        {
                            let parameter2   = evaluationStack.pop();
                            let parameter1   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]( parameter1, parameter2));

                            break;
                        }


                        // instance.Call('functionName', parameter1, parameter2, parameter3)
                        case 9:
                        {
                            let parameter3   = evaluationStack.pop();
                            let parameter2   = evaluationStack.pop();
                            let parameter1   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]( parameter1, parameter2, parameter3));

                            break;
                        }


                        // instance.Call('functionName', parameters)
                        case 10:
                        {
                            let parameters   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName].apply(instance, parameters));

                            break;
                        }


                        case 11: // GlobalMetadata
                        {
                            evaluationStack.push(GlobalMetadata);
                            break;
                        }

                        case 12: // array.push
                        {
                            let item = evaluationStack.pop();
                            let arrayInstance = evaluationStack.pop();

                            if ( arrayInstance == null)
                            {
                                evaluationStack.push('array is null. when call push');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            arrayInstance.push(item);
                            break;
                        }

                        case 13: // array.pop
                        {
                            let arrayInstance = evaluationStack.pop();

                            if ( arrayInstance == null)
                            {
                                evaluationStack.push('array is null. when call push');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            evaluationStack.push(arrayInstance.pop());
                            break;
                        }
                        case 14: // Dump
                        {
                            console.log(evaluationStack.pop());
                            break;
                        }

                        case 15: // CurrentStackFrame
                        {
                            evaluationStack.push(currentStackFrame);
                            break;
                        }
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 221: // handle exception
                {
                    let exception = evaluationStack.pop();

                    if(typeof exception === 'string')
                    {
                        exception = new Error(exception);
                    }

                    let handlerFound = 0;
                    while( currentStackFrame && !handlerFound )
                    {
                        let exceptionHandlers = currentStackFrame.Method.Body.ExceptionHandlers;
                        let exceptionHandlersLength = exceptionHandlers.length;

                        for (let i = 0; i < exceptionHandlersLength; i++)
                        {
                            let exceptionHandler = exceptionHandlers[i];

                            if ( exceptionHandler.CatchType >= 0 )
                            {
                                const catchType = GetType(currentStackFrame.Method.Metadata, exceptionHandler.CatchType);
                                
                                let catchTypeIsDotNetRootException = catchType.Namespace === 'System' && catchType.Name === 'Exception';
                                
                                if ( GlobalMetadata.Types[exception.$typeIndex] ===  catchType || catchTypeIsDotNetRootException )
                                {
                                    if (exceptionHandler.TryStart <= currentStackFrame.Line &&
                                        exceptionHandler.TryEnd >= currentStackFrame.Line)
                                    {
                                        handlerFound = 1;
                                        
                                        evaluationStack.push(exception);

                                        currentStackFrame.Line = exceptionHandler.HandlerStart;
                                        nextInstruction = instructions[currentStackFrame.Line];
                                        break;
                                    }
                                }
                            }
                        }

                        const Finally = 2;
                        
                        let finallyFound = 0;
                        
                        // try to find finally block
                        if (!handlerFound)
                        {
                            for (let i = 0; i < exceptionHandlersLength; i++)
                            {
                                let exceptionHandler = exceptionHandlers[i];

                                if ( exceptionHandler.HandlerType === Finally )
                                {
                                    if (exceptionHandler.HandlerStart <= currentStackFrame.Line &&
                                        exceptionHandler.HandlerEnd >= currentStackFrame.Line)
                                    {
                                        finallyFound = 1;
                                        currentStackFrame.Line = exceptionHandler.HandlerStart;
                                        nextInstruction = instructions[currentStackFrame.Line];
                                        break;
                                    }
                                }
                            }
                            
                            if( finallyFound )
                            {
                                thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock = exception;
                            }
                        }

                        if ( !handlerFound )
                        {
                            if(!exception._stackTrace)
                            {
                                exception._stackTrace =[];
                            }
                            exception._stackTrace.push(currentStackFrame.Method);

                            if( currentStackFrame.Prev === null)
                            {
                                // exit thread
                                thread.OnFail(exception);
                                return;
                            }

                            // go previous stack frame
                            {
                                // arrange stack frame
                                let previousStackFrame = currentStackFrame;

                                thread.LastFrame = currentStackFrame = currentStackFrame.Prev;

                                // arrange fast access variables
                                instructions = currentStackFrame.Method.Body.Instructions;
                                operands     = currentStackFrame.Method.Body.Operands;

                                // arrange fast access variables
                                evaluationStack      = currentStackFrame.EvaluationStack;
                                localVariables       = currentStackFrame.LocalVariables;
                                methodArguments      = currentStackFrame.MethodArguments;
                                methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

                                // remove parameters
                                length = previousStackFrame.Method.Parameters.length;
                                while(length-- > 0)
                                {
                                    evaluationStack.pop();
                                }

                                // remove instance
                                if (previousStackFrame.Method.IsStatic === false)
                                {
                                    evaluationStack.pop();
                                }
                            }
                        }
                    }
                    
                    if ( handlerFound )
                    {
                        break;
                    }

                    // exit thread
                    thread.OnFail(exception);
                    return;
                }

                case 222: // Load method at runtime
                {
                    let methodReference = evaluationStack.pop();
                    
                    let declaringType = AllTypes[methodReference.DeclaringType];

                    let declaringTypeAsJson = SerializeTypeReference(declaringType);

                    let query = Object.assign({},declaringTypeAsJson);
                    query.MethodName = methodReference.Name;
                    function onSuccess(response)
                    {
                        if (response.Success === 0)
                        {
                            throw response.ErrorMessage;
                        }

                        ImportMetadata(response.Metadata);
                        
                        thread.IsSuspended = 0;
                        
                        Interpret(thread);
                    }
                    function onFail(exception)
                    {
                        throw exception;
                    }

                    let request =
                    {
                        IsInitialRequest: false,
                        RequestedTypes:
                        [
                            query
                        ]
                    };
                    
                    GetMetadata(request, onSuccess, onFail);
                    
                    thread.IsSuspended = 1;
                    
                    return;
                }

                case 223: // Load type at runtime
                {
                    const typeReference = evaluationStack.pop();

                    const request =
                    {
                        IsInitialRequest: false,
                        RequestedTypes:
                        [
                            SerializeTypeReference(typeReference)
                        ]
                    };

                    const onSuccess = response =>
                    {
                        if (response.Success === 0)
                        {
                            throw response.ErrorMessage;
                        }

                        ImportMetadata(response.Metadata);

                        thread.IsSuspended = 0;

                        Interpret(thread);
                    };
                    
                    const onFail = (e)=>
                    {
                        throw e;
                    };
                    
                    GetMetadata(request, onSuccess, onFail);

                    thread.IsSuspended = 1;

                    return;
                }
                
                case 224: // System.String System.String::Concat(System.String,System.String)
                {
                    let b    = evaluationStack.pop() || '';
                    let a      = evaluationStack.pop() || '';
                    
                    evaluationStack.push(a + b);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 225: // System.String System.String::Concat(System.String,System.String,System.String)
                {
                    let c    = evaluationStack.pop() || '';
                    let b    = evaluationStack.pop() || '';
                    let a      = evaluationStack.pop() || '';

                    evaluationStack.push(a + b + c);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 226: // System.String System.String::Concat(System.String,System.String,System.String,System.String)
                {
                    let d    = evaluationStack.pop() || '';
                    let c    = evaluationStack.pop() || '';
                    let b    = evaluationStack.pop() || '';
                    let a      = evaluationStack.pop() || '';

                    evaluationStack.push(a + b + c + d);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
            }
        }
        catch (exception)
        {
            // exit thread
            if (nextInstruction === 221)
            {
                throw exception;
            }

            // handle exception
            evaluationStack.push(exception);
            nextInstruction = 221;
        }
    }
}

function SerializeTypeReference(typeReference)
{
    if (!typeReference)
    {
        return null;
    }

    let declaringTypeAsJson = null;
    
    const assemblyName = GlobalMetadata.MetadataScopes[typeReference.Scope].Name;

    const declaringType = typeReference.DeclaringType;
    if (declaringType != null)
    {
        declaringTypeAsJson = SerializeTypeReference(GlobalMetadata.Types[declaringType]);
    }

    return {
        AssemblyName: assemblyName,
        NamespaceName: typeReference.Namespace,
        TypeName: typeReference.Name,
        DeclaringType: declaringTypeAsJson
    }
}

/**
 * @param request
 * @param {(response: MetadataResponse) => void} onSuccess
 * @param {(e: Error) => void} onFail
 */
function GetMetadata(request, onSuccess, onFail)
{
    const url = "/GetMetadata";
    
    let options =
    {
        method: "POST",
        headers:
        {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        body: JSON.stringify(request)
    };
    
    window.fetch(url, options).then(response => response.json()).then(json => onSuccess(json)).catch(onFail ?? console.log);
}

function CallManagedStaticMethod(methodDefinition, args, success, fail) 
{
    fail = fail ?? console.log;
    
    const thread =
    {
        LastFrame: 
        {
            Method: methodDefinition,
            EvaluationStack:[],
            LocalVariables:[],
            MethodArguments: args,
            MethodArgumentsOffset: 0,
            Line: 0,
            Prev: null
        },

        LeaveJumpIndex: null,
        ExceptionObjectThatMustThrownWhenExitFinallyBlock: null,
        IsSuspended: 0,
        OnSuccess: function(value)
        {
            if (success)
            {
                success(value);   
            }
        },
        OnFail: function(exception)
        {
            fail(exception);
        }
    };

    try 
    {
        Interpret(thread);
    }
    catch (e) 
    {
        fail(e);
    }
}

/**
 * @typedef {Object} MetadataResponse
 * @property {string | null} ErrorMessage
 * @property {Metadata | null} Metadata
 * @property {number} Success
 */

setTimeout(function () 
{   
    let request =
    {
        IsInitialRequest: true,
        RequestedTypes:
        [
            {
                AssemblyName: "ReactWithDotNet.WebSite.dll",
                NamespaceName: "ReactWithDotNet.WebSite",
                TypeName: "Test45"
            }
        ]
    };

    function onSuccess(response)
    {
        if (response.Success === 0)
        {
            throw response.ErrorMessage;
        }

        let metadataTable = response.Metadata;

        TryInitialize_InterpreterBridge(metadataTable);

        ImportMetadata(metadataTable);

        for (let i = 0; i < metadataTable.Types.length; i++)
        {
            const table = metadataTable.Types[i];

            if (!table.IsDefinition)
            {
                continue;
            }

            for (let j = 0; j < table.Methods.length; j++)
            {

                const method = metadataTable.Methods[table.Methods[j]];

                if (method.IsDefinition)
                {
                    if (method.Name === "Abc5")
                    {
                        CallManagedStaticMethod(method,[],console.log,console.log);
                    }
                }
            }
        }

    }

    function onFail(error)
    {
        throw error;
    }

    GetMetadata(request, onSuccess, onFail);    
    
}, 2000);


const CLR =
{

};
window.CLR = CLR;

function IsTwoMethodParametersFullMatch(methodA, methodB)
{
    const parametersA = methodA.Parameters;
    const parametersB = methodB.Parameters;
    
    let lengthA = parametersA.length;
    let lengthB = parametersB.length;

    if (lengthA !== lengthB)
    {
        return false;
    }

    for (let i = 0; i < lengthA; i++)
    {
        if (GetType(methodA.Metadata, parametersA[i].ParameterType) !== GetType(methodB.Metadata, parametersB[i].ParameterType))
        {
            return false;
        }
    }

    return true;
}


