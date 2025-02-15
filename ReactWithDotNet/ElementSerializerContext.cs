namespace ReactWithDotNet;

sealed class ElementSerializerContext
{
    public readonly DynamicStyleContentForEmbedInClient DynamicStyles = new();

    public readonly Stack<FunctionalComponent> FunctionalComponentStack = new();

    public readonly ReactContext ReactContext;

    public readonly StateTree StateTree;

    public int ComponentUniqueIdentifierNextValue;

    public ElementSerializerContext(ReactContext reactContext, StateTree stateTree)
    {
        ReactContext = reactContext;

        StateTree = stateTree;
    }

    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }

    public bool IsCapturingPreview { get; set; }

    public Tracer Tracer { get; init; }
}