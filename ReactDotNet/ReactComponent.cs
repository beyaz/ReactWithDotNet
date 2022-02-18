namespace ReactDotNet
{
    public interface IReactStatelessComponent
    {
         Element render();
    }

    public abstract class ReactComponent : Element, IReactStatelessComponent
    {
        public abstract Element render();
    }



    public interface IReactStatefulComponent
    {
        Element RootElement { get; }
    }

    public abstract class ReactComponent<TState> : Element, IReactStatefulComponent where TState : new()
    {

        public Element RootElement => render();

        public string fullName => GetType().GetFullName();

        public abstract Element render();


        public TState state { get; set; }

        public string FullTypeNameOfState => typeof(TState).GetFullName();

        public int? UniqueIdOfState { get; set; }
    }
}