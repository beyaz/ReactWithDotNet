namespace ReactWithDotNet;

sealed class ElementSerializerContext
{
    public readonly DynamicStyleContentForEmbedInClient DynamicStyles = new();
    
    public readonly StateTree StateTree;

    public readonly ReactContext ReactContext;

    public ElementSerializerContext(ReactContext reactContext, StateTree stateTree)
    {
        ReactContext = reactContext;
        
        StateTree = stateTree;
    }

    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }

    public int ComponentUniqueIdentifierNextValue { get; set; }
    public Stack<FunctionalComponent> FunctionalComponentStack { get; set; }
    public bool IsCapturingPreview { get; set; }

    

    

    public Tracer Tracer { get; init; }
}