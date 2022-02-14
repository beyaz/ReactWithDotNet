namespace ReactDotNet
{
    public abstract class ReactComponent : Element
    {
        public Element RootElement => render();

        public string FullName => GetType().GetFullName();

        public abstract Element render();
    }

    public abstract class ReactComponent<TState> : ReactComponent where TState : new()
    {
        public TState state { get; set; }
    }
}