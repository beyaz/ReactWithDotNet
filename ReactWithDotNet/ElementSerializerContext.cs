namespace ReactWithDotNet;

sealed class ElementSerializerContext
{
    public readonly BeforeSerializeElementToClient BeforeSerializeElementToClient;
    
    public readonly DynamicStyleContentForEmbedInClient DynamicStyles = new();

    public readonly Stack<FunctionalComponent> FunctionalComponentStack = new();

    public readonly ReactContext ReactContext;

    public readonly StateTree StateTree;

    public int ComponentUniqueIdentifierNextValue;

    public ElementSerializerContext(ReactContext reactContext, StateTree stateTree, BeforeSerializeElementToClient beforeSerializeElementToClient)
    {
        ReactContext = reactContext;

        StateTree = stateTree;

        BeforeSerializeElementToClient = beforeSerializeElementToClient;
    }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }

    public bool IsCapturingPreview { get; set; }

    public Tracer Tracer { get; init; }
}