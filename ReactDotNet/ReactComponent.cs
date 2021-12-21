namespace ReactDotNet
{
    public interface IReactComponent
    {
        ReactElement ToReactElement();
    }

    public abstract class ReactComponent<TState> :  Element, IReactComponent where TState : new()
    {
        public TState state { get; set; }
        
        public Element RootElement => render();

        public string FullName => GetType().GetFullName();

        public abstract Element render();
    }
}