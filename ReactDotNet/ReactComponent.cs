namespace ReactDotNet
{
    public abstract class ReactComponent : Element
    {
        public Element RootElement => render();

        public string fullName => GetType().GetFullName();

        public abstract Element render();
    }

    public abstract class ReactComponent<TState> : ReactComponent where TState : new()
    {
        public TState state { get; set; }
    }
}