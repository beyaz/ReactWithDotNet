namespace ReactDotNet
{
    public abstract class ReactComponent<TState> :  Element where TState : new()
    {
        public TState state { get; set; }
        
        public Element RootElement => render();

        public string FullName => GetType().GetFullName();

        public abstract Element render();
    }
}